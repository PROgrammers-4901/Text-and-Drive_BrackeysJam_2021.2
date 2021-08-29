using System;
using System.Collections.Generic;
using Microgames;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Phone
{
    public class PhoneController : Singleton<PhoneController>
    {
        [SerializeField] private GameObject phoneScreenContainer;
        [SerializeField] private GameObject notificationContainer;
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private int playerLives = 4;

        private List<GameObject> _notificationInstances = new List<GameObject>();
        private GameObject _microgameInstance;
        private float _idleTime;

        private bool firstMicrogame = true;

        private void Awake()
        {
            if (phoneScreenContainer == null)
                throw new Exception("Phone Screen Container Not Set");
        }

        private void Update()
        {
            if (firstMicrogame && GameManager.Instance.GameStarted)
            {
                Invoke(nameof(FetchMicrogame), GameManager.Instance.GetMicrogameInterval());
                firstMicrogame = false;
            }
            
            if (_notificationInstances.Count > 0)
                _idleTime += Time.deltaTime * _notificationInstances.Count;
            else
                _idleTime = Mathf.Clamp(_idleTime - Time.deltaTime, 0, float.MaxValue);
            
            if(_notificationInstances.Count > 15 && _idleTime > 60f)
                GameManager.Instance.GameOver();
        }

        void SpawnNotification(MicrogameScriptableObject microgame)
        {
            // TODO: Spawn Sound
            SoundManager.Instance.PlaySound(microgame.NotificationSounds[Random.Range(0, microgame.NotificationSounds.Count)].name);

            if (!phoneScreenContainer.activeSelf)
                phoneScreenContainer.SetActive(true);

            // Spawn Notification
            GameObject notification = Instantiate(notificationPrefab, notificationContainer.transform);
            NotificationDisplay nd = notification.GetComponent<NotificationDisplay>();
            nd.InitializeNotification(microgame);
            nd.SpawnMicrogame.AddListener(HandleSpawnButton);
            _notificationInstances.Add(notification);

            // Start Timer
        }

        public void TogglePhoneScreen()
        {
            phoneScreenContainer.SetActive(!phoneScreenContainer.activeSelf);
        }


        // Update is called once per frame
        void FetchMicrogame()
        {
            MicrogameScriptableObject newGame = GameManager.Instance.GetRandomMicrogame();

            SpawnNotification(newGame);
            Invoke(nameof(FetchMicrogame), GameManager.Instance.GetMicrogameInterval());
        }

        public void HandleMicrogameComplete(Microgame microgame)
        {
            Destroy(_microgameInstance);

            // TODO: Phone Fail State
            GameManager.Instance.PhoneScore++;
        }

        void HandleSpawnButton(NotificationDisplay nd)
        {
            _microgameInstance = Instantiate(nd.microgame, phoneScreenContainer.transform);
            _notificationInstances.Remove(nd.gameObject);
            
            Microgame microgame = _microgameInstance.GetComponent<Microgame>();
            microgame.MicrogameCompleted.AddListener(HandleMicrogameComplete);
            
            Destroy(nd.gameObject);
        }
    }
}