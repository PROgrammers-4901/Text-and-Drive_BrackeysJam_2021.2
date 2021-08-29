using UnityEngine;
using UnityEngine.Audio;

public class DrivingHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [Header("Sounds")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("ScreenDamage")] 
    [SerializeField] private GameObject damage1;
    [SerializeField] private GameObject damage2;

    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("KillObject"))
        {
            playerHealth--;
            
            if (playerHealth == 2)
            {
                damage1.SetActive(true);
                audioMixer.SetFloat("easyVolume", -80f);
                audioMixer.SetFloat("mediumVolume", -10f);
            }
            else if (playerHealth == 1)
            {
                damage2.SetActive(true);
                audioMixer.SetFloat("mediumVolume", -80f);
                audioMixer.SetFloat("hardVolume", 0f);
            }
            

            if (playerHealth <= 0)
            {
                SoundManager.Instance.PlaySound("GameOverCrash", 1.3f);
                GameManager.Instance.GameOver();
                audioMixer.SetFloat("hardVolume", -80f);
                audioMixer.SetFloat("easyVolume", 0f);
                return;
            }
            
            SoundManager.Instance.PlaySound("CarCrash");
        }
    }
}
