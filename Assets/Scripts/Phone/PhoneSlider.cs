using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneSlider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [Header("References")] 
    [SerializeField] private RectTransform phoneFrame;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_allowDrag) return;
        
        _startingPosition = phoneFrame.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_allowDrag) return;

        _allowDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerPressRaycast.gameObject;
        if (clicked != phoneFrame.gameObject)
            return;
        
        Vector3 point = clicked.transform.worldToLocalMatrix.MultiplyPoint(eventData.pressPosition);
        Texture2D pic = clicked.transform.gameObject.GetComponent<Image>().sprite.texture;
        
        Vector2 uv = new Vector2((point.x/1228f) +.5f , (point.y/1228f) +.5f);
        
        float alpha = pic.GetPixel((int) (uv.x * pic.width), (int) (uv.y * pic.height)).a;

        _allowDrag = alpha >= .5f;
    }
}
