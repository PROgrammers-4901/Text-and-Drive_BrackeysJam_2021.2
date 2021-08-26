using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Dial : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [Header("References")] 
    [SerializeField] private RectTransform steeringWheel;
    [SerializeField] private Canvas canvas;
    
    [Header("Settings")] 
    [SerializeField] private float maxRotationInDegrees = 45f;

    [SerializeField] private float mouseSpeedMultiplier = 8;
    [SerializeField] private float smoothSpeed = .04f;
    
    [SerializeField] private bool returnToZero = true;
    [SerializeField] private float returnSpeed = .03f;
    
    
    
    private bool _allowDrag = false;
    private float _mouseDelta = 0;
    private float currentAngle = 0f;
    public float GetMaxAngle => maxRotationInDegrees;

    private Vector2 _uvClick;

    private void LateUpdate()
    {
        Quaternion quaternion = Quaternion.Euler(0, 0, -_mouseDelta);
        this.transform.rotation.ToAngleAxis(out currentAngle, out _);
        quaternion.ToAngleAxis(out var desiredAngle, out _);
        
        if(currentAngle < maxRotationInDegrees || desiredAngle < maxRotationInDegrees)
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, smoothSpeed);

        if (!_allowDrag && returnToZero)
            _mouseDelta = Mathf.Lerp(_mouseDelta, Random.Range(-5f, 5f), returnSpeed);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_allowDrag)
        {
            if(_uvClick.y > -.2f)
                _mouseDelta += Input.GetAxis("Mouse X") * mouseSpeedMultiplier;
            else
                _mouseDelta -= Input.GetAxis("Mouse X") * mouseSpeedMultiplier;
            
            if(_uvClick.x > 0)
                _mouseDelta -= Input.GetAxis("Mouse Y") * mouseSpeedMultiplier;
            else
                _mouseDelta += Input.GetAxis("Mouse Y") * mouseSpeedMultiplier;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_allowDrag) return;

        _allowDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerPressRaycast.gameObject;
        if (clicked != steeringWheel.gameObject)
            return;
        
        Vector3 point = steeringWheel.gameObject.transform.worldToLocalMatrix.MultiplyPoint(eventData.position);
        _uvClick = new Vector2((point.x/steeringWheel.sizeDelta.x), (point.y/steeringWheel.sizeDelta.y));
        
        Debug.Log(_uvClick);
        
        
        _allowDrag = (common.Raycast.CheckIfTransparencyHit(eventData.pointerPressRaycast, eventData.pressPosition) != null);
    }
}
