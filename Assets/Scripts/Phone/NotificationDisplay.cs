using Microgames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Phone
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField] private Image appLogo;
        [SerializeField] private TMPro.TMP_Text appName;
        [SerializeField] private TMPro.TMP_Text notificationText;
        public GameObject microgame;

        public SpawnMicrogameEvent SpawnMicrogame;
        public void InitializeNotification(MicrogameScriptableObject microgameSO)
        {
            appLogo.sprite = microgameSO.appLogo;
            appName.text = microgameSO.appName;
            notificationText.text = microgameSO.notificationText;
            microgame = microgameSO.microgame.gameObject;
        }

        public void SpawnMicrogameOnClick() =>
            SpawnMicrogame.Invoke(this);

    }
    
    [System.Serializable]
    public class SpawnMicrogameEvent : UnityEvent<NotificationDisplay>
    {
        
    }
}
