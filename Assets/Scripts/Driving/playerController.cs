using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Dial steeringWheel;

    private BoxCollider _bc;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float strafeSpeed = 1;
    [SerializeField] private float brakeCooldownInSeconds = 5f;
    [Range(0f,1f)]
    [SerializeField] private float acceleration;
    [Range(0f,1f)]
    [SerializeField] private float turnSpeed;
    
    [SerializeField] private Vector2 xDistance = new Vector2(-60f, 60f);

    private bool braking = false;
    private bool brakingAllowed = false;

    private void Awake()
    {
        _bc = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        SetSpeed(GameManager.Instance.PlayerSpeed);
        Invoke(nameof(AllowBraking), brakeCooldownInSeconds * 3f);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Brake") && !braking && brakingAllowed)
        {
            braking = true;
            brakingAllowed = false;
            Invoke(nameof(StopBraking), 1f);
            
            GameManager.Instance.ResumeGame();
        }
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
            SetSpeed(GameManager.Instance.PlayerSpeed/5f);
        else
            SetSpeed(GameManager.Instance.PlayerSpeed);
    }

    public void SetSpeed(float speed) => moveSpeed = Mathf.Lerp(moveSpeed, speed, acceleration);

    public void StopBraking()
    {
        moveSpeed = GameManager.Instance.PlayerSpeed;
        braking = false;
        Invoke(nameof(AllowBraking), brakeCooldownInSeconds);
    }

    public void AllowBraking()
    {
        Debug.Log("Brake Allowed");
        brakingAllowed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillObject"))
        {
            Debug.Log("GAME OVER");
            GameManager.Instance.PauseGame();
        }
    }
}
