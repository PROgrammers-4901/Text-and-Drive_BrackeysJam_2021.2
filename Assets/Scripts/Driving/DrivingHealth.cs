using UnityEngine;
using UnityEngine.Audio;

public class DrivingHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [Header("Sounds")]
    [SerializeField] private AudioMixer audioMixer;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("KillObject"))
        {
            playerHealth--;
            
            if (playerHealth == 2)
            {
                audioMixer.SetFloat("easyVolume", -80f);
                audioMixer.SetFloat("mediumVolume", -10f);
                //easyVolume = -80f;
                //mediumVolume = -10f;
            }
            else if (playerHealth == 1)
            {
                audioMixer.SetFloat("mediumVolume", -80f);
                audioMixer.SetFloat("hardVolume", 0f);
                //mediumVolume = -80f;
                //hardVolume = 0f;
            }
            

            if (playerHealth <= 0)
            {
                SoundManager.Instance.PlaySound("GameOverCrash", 1.3f);
                GameManager.Instance.GameOver();
                audioMixer.SetFloat("hardVolume", -80f);
                audioMixer.SetFloat("easyVolume", 0f);
                //hardVolume = -80f;
                //easyVolume = 0f;
                return;
            }
            
            SoundManager.Instance.PlaySound("CarCrash");
        }
    }
}
