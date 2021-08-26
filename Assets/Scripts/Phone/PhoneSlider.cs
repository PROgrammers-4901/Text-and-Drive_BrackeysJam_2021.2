using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneSlider : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [Header("References")] 
    [SerializeField] private RectTransform phoneFrame;
    [SerializeField] private List<RectTransform> extraHandles = new List<RectTransform>();
    [SerializeField] private Canvas canvas;
    
    [Header("Settings")] 
    [SerializeField] private bool allowVerticalDrag;
    [SerializeField] private bool allowHorizontalDrag;
    [SerializeField] private float maxTravelDistance = 1000f;
    [SerializeField] private Vector2 minScreenPosition;
    [SerializeField] private Vector2 maxScreenPosition;
    
    

    private bool _allowDrag = false;
    private Vector2 _startingPosition;

    private void Start()
    {
        _startingPosition = phoneFrame.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_allowDrag)
        {
            Vector2 oldPosition = phoneFrame.anchoredPosition;
            Vector2 delta = eventData.delta / (canvas.renderMode != RenderMode.WorldSpace ? canvas.scaleFactor : 1);
            Vector2 newPosition = new Vector2(
                Mathf.Clamp(allowHorizontalDrag ? (delta.x + oldPosition.x) : oldPosition.x,minScreenPosition.x, maxScreenPosition.x),
                Mathf.Clamp(allowVerticalDrag ? (delta.y + oldPosition.y) : oldPosition.y, minScreenPosition.y, maxScreenPosition.y)
            );

            if(Vector2.Distance(newPosition,_startingPosition) < maxTravelDistance)
                phoneFrame.anchoredPosition = newPosition;
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
        
        if(extraHandles.Contains(clicked.GetComponent<RectTransform>()))
        {
            _allowDrag = true;
            return;
        }
        
        if (clicked != phoneFrame.gameObject)
            return;
        
        _allowDrag = (common.Raycast.CheckIfTransparencyHit(eventData.pointerPressRaycast, eventData.pressPosition) != null);
    }
}
