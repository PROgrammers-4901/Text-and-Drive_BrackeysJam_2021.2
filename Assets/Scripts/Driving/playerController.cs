using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float strafeSpeed = 1;
    
    [SerializeField] private Dial steeringWheel;
    [SerializeField] private Vector2 xDistance = new Vector2(-60f, 60f);
    [SerializeField] private float brakeCooldown = 5f;

    private bool braking = false;
    private bool brakingAllowed = false;

    private void Start()
    {
        moveSpeed = GameManager.Instance.PlayerSpeed;
        Invoke(nameof(AllowBraking), brakeCooldown * 3f);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Brake") && !braking && brakingAllowed)
        {
            Debug.Log("BRAKE");
            
            braking = true;
            brakingAllowed = false;
            moveSpeed /= 5;
            Invoke(nameof(StopBraking), 1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 startPosition = this.transform.position;
        
        float wheelRotation = steeringWheel.gameObject.transform.rotation.z * Mathf.Rad2Deg;
        Vector3 positionDelta = new Vector3(
            (-wheelRotation / steeringWheel.GetMaxAngle * strafeSpeed),
            0,
            moveSpeed
        );
        
        this.transform.rotation = Quaternion.Euler(0,-wheelRotation, 0 );
        this.transform.position = new Vector3(
            Mathf.Clamp((startPosition.x + positionDelta.x), xDistance.x, xDistance.y),
            startPosition.y + positionDelta.y,
            startPosition.z + positionDelta.z
        );
    }

    public void SetSpeed(float speed) => moveSpeed = speed;

    public void StopBraking()
    {
        moveSpeed = GameManager.Instance.PlayerSpeed;
        braking = false;
        Invoke(nameof(AllowBraking), brakeCooldown);
    }

    public void AllowBraking()
    {
        Debug.Log("Brake Allowed");
        brakingAllowed = true;
    }
}
