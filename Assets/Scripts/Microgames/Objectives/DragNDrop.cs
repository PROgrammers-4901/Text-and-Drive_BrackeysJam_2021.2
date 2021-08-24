using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Rendering;

public class DragNDrop : ObjectiveBase
{
    [Header("References")]
    public DraggableUI draggableObject;
    public GameObject destination;
    public Camera mainCamera;

    [Header("Settings")] 
    [SerializeField]
    private bool _snapToOrigin = false;

    [SerializeField] 
    private bool useThreshold = false;
    [SerializeField]
    private float _distanceToDestinationThreshold = 1f;
    
    private bool _isMouseDown;
    private Vector3 _draggableStartPosition;
    private Vector3 _mousePosition;
    

    private float _zDistFromCamera = 0f;

    private void Start()
    {
        _zDistFromCamera = Mathf.Abs(draggableObject.transform.position.z - mainCamera.transform.position.z);
        _distanceToDestinationThreshold *=
            Vector3.Magnitude(draggableObject.transform.position - destination.transform.position);

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(draggableObject.transform.position, destination.transform.position));
        _mousePosition = Input.mousePosition;
        _mousePosition.z = _zDistFromCamera;
        _mousePosition = mainCamera.ScreenToWorldPoint(_mousePosition);

        // if (Input.GetButtonDown("Click"))
        //     _draggableStartPosition = draggableObject.transform.position;
        //
        // if (Input.GetButton("Click"))
        //     draggableObject.transform.position = new Vector3(_mousePosition.x,
        //         _mousePosition.y, draggableObject.transform.position.z);
        //
        // if (Input.GetButtonUp("Click") && _snapToOrigin)
        //     draggableObject.transform.position = _draggableStartPosition;

        if (useThreshold)
        {
            Debug.Log("USE COLLIDER NOT IMPLEMENTED YET");
        }

        if (Vector3.Magnitude(draggableObject.transform.position - destination.transform.position) <
            _distanceToDestinationThreshold)
            CompleteObjective();
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == destination && !useThreshold)
            CompleteObjective();
    }
}
