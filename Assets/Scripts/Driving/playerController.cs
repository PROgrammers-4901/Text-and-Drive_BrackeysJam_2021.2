using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Dial steeringWheel;
    [SerializeField] private AudioClip brakingSound;
    
    
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float strafeSpeed = 1;
    [SerializeField] private float brakeCooldownInSeconds = 5f;
    [Range(0f,1f)]
    [SerializeField] private float acceleration;
    [Range(0f,1f)]
    [SerializeField] private float turnSpeed;
    [SerializeField] private Vector2 xDistance = new Vector2(-60f, 60f);
    
    [Header("Health")] [SerializeField]
    private int playerHealth;

    private bool braking = false;
    private bool brakingAllowed = false;
    private AudioSource brakeSound;
    private bool fadeOut;

    private void Start()
    {
        GameManager.Instance.PlayerObject = this.gameObject;
        Invoke(nameof(AllowBraking), brakeCooldownInSeconds * 3f);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Brake") && !braking && brakingAllowed)
        {
            brakeSound = SoundManager.Instance.PlaySound("Car Brake");
            brakeSound.volume = 1f;
            
            fadeOut = false;
            braking = true;
            brakingAllowed = false;
            Invoke(nameof(StopBraking), 1f);
        }

        if (Input.GetButtonUp("Brake") && braking)
            fadeOut = true;
        
        if (fadeOut && brakeSound)
            brakeSound.volume -= Time.deltaTime*2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 startPosition = this.transform.position;
        
        float wheelRotation = steeringWheel.gameObject.transform.rotation.z * Mathf.Rad2Deg;
        Vector3 positionDelta = new Vector3(
            (-wheelRotation / steeringWheel.GetMaxAngle * moveSpeed),
            0,
            moveSpeed
        );

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, -wheelRotation, 0), turnSpeed);
        this.transform.position = new Vector3(
            Mathf.Clamp((startPosition.x + positionDelta.x), xDistance.x, xDistance.y),
            startPosition.y + positionDelta.y,
            startPosition.z + positionDelta.z
        );
        
        if (braking)
            SetSpeed(GameManager.Instance.GetScaledSpeed()/5f);
        else
            SetSpeed(GameManager.Instance.GetScaledSpeed());
    }

    public void SetSpeed(float speed) => moveSpeed = Mathf.Lerp(moveSpeed, speed, acceleration);

    public void StopBraking()
    {
        braking = false;
        Invoke(nameof(AllowBraking), brakeCooldownInSeconds);
    }

    public void AllowBraking()
    {
        Debug.Log("Brake Allowed");
        brakingAllowed = true;
    }

    
}
