using System;
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

    [Header("Animator")]
    [SerializeField] private Animator carAnimator;
    private Animator phoneAnimator;
    [SerializeField] private GameObject airBag;

    private void Start()
    {
        phoneAnimator = GameObject.Find("PhoneContainer").GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("KillObject"))
        {
            playerHealth--;
            
            carAnimator.Play("CarDamage");
            phoneAnimator?.Play("CarDamagePhone");
            
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
                airBag.SetActive(true);
                return;
            }
            
            SoundManager.Instance.PlaySound("CarCrash");
        }
    }
}
