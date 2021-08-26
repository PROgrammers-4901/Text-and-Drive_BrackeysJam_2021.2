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
        [SerializeField] private GameObject _notificationPrefab;


        private List<MicrogameScriptableObject> loadedMicroGames = new List<MicrogameScriptableObject>();
        private List<GameObject> notificationInstances = new List<GameObject>();
        private GameObject microgameInstance;

        private float elapsedTime;

        private void Awake()
        {
            if (phoneScreenContainer == null)
                throw new Exception("Phone Screen Container Not Set");
        }

        private void Start()
        {
            Invoke(nameof(FetchMicrogame), GameManager.Instance.GetMicrogameInterval());
        }

        void SpawnNotification(MicrogameScriptableObject microgame)
        {
            // Spawn Sound
            Debug.Log("DING DING");

            if (!phoneScreenContainer.activeSelf)
                phoneScreenContainer.SetActive(true);

            // Spawn Notification
            GameObject notification = Instantiate(_notificationPrefab, notificationContainer.transform);
            NotificationDisplay nd = notification.GetComponent<NotificationDisplay>();
            nd.InitializeNotification(microgame);
            nd.SpawnMicrogame.AddListener(HandleSpawnButton);
            notificationInstances.Add(notification);

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

            loadedMicroGames.Add(newGame);

            SpawnNotification(newGame);
            Invoke(nameof(FetchMicrogame), GameManager.Instance.GetMicrogameInterval());
        }

        public void HandleMicrogameComplete(Microgame microgame)
        {
            Destroy(microgameInstance);

            Debug.Log("MICROGAME CONTROLLER EVENT HANDLER");
        }

        void HandleSpawnButton(NotificationDisplay nd)
        {
            Debug.Log("TEST");
            
            microgameInstance = Instantiate(nd.microgame, phoneScreenContainer.transform);

            Microgame microgame = microgameInstance.GetComponent<Microgame>();
            microgame.MicrogameCompleted.AddListener(HandleMicrogameComplete);
            
            Destroy(nd.gameObject);
        }
    }
}