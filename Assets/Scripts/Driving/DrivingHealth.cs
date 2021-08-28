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
            
            if(playerHealth <= 0)
            {
                SoundManager.Instance.PlaySound(GameOver, 1.3f);
                GameManager.Instance.GameOver();
                return;
            }
            
            SoundManager.Instance.PlaySound(CarImpact);
        }
    }
}
