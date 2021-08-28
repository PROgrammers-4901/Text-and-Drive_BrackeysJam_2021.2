using UnityEngine;

public class DrivingHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [Header("Sounds")]
    [SerializeField] private AudioClip CarImpact;
    [SerializeField] private AudioClip GameOver;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("KillObject"))
        {
            playerHealth--;
            //StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
            /*
            if (playerHealth == 2)
            {
                StartCoroutine(AudioManager.FadeAudioSource.StartFade(easy, medium, 1, .75));
            }
            else if (playerHealth == 1)
            {
                StartCoroutine(AudioManager.FadeAudioSource.StartFade(medium, hard, 1, 1));
            }
            */

            if (playerHealth <= 0)
            {
                SoundManager.Instance.PlaySound("GameOverCrash", 1.3f);
                GameManager.Instance.GameOver();
                return;
            }
            
            SoundManager.Instance.PlaySound("CarCrash");
        }
    }
}
