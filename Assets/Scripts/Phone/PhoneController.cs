using System;
using System.Collections.Generic;
using Microgames;
using UnityEngine;

namespace Phone
{
    public class PhoneController : Singleton<PhoneController>
    {
        [SerializeField] private GameObject phoneScreenContainer;
        [SerializeField] private GameObject notificationContainer;
        [SerializeField] private GameObject notificationPrefab;

        private List<GameObject> _notificationInstances = new List<GameObject>();
        private GameObject _microgameInstance;
        private float _idleTime;

        private void Awake()
        {
            if (phoneScreenContainer == null)
                throw new Exception("Phone Screen Container Not Set");
        }

        private void Start()
        {
            Invoke(nameof(FetchMicrogame), GameManager.Instance.GetMicrogameInterval());
        }

        private void Update()
        {
            _idleTime += Time.deltaTime * _notificationInstances.Count;
        }

        void SpawnNotification(MicrogameScriptableObject microgame)
        {
            // Spawn Sound
            Debug.Log("BRzzt BRzzt");

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