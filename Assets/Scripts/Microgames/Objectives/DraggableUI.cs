using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : ObjectiveBase, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private RectTransform draggable;
    [SerializeField] private RectTransform destination;
    [SerializeField] private Canvas canvas;
    
    [Header("Settings")]
    [SerializeField] private bool snapToOrigin;
    [SerializeField] private float distanceThreshold = 10f;
    
    private Color _imageColor;
    private Image _image;
    private Vector2 _startingPosition = Vector2.zero;

    private void Awake()
    {
        _image = draggable.GetComponentInChildren<Image>();
        _imageColor = _image.color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggable.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startingPosition = draggable.anchoredPosition;
        
        _imageColor.a = .8f;
        _image.color = _imageColor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _imageColor.a = 1f;
        _image.color = _imageColor;
        
        if(Vector2.Distance(draggable.anchoredPosition, destination.anchoredPosition) < distanceThreshold)
            CompleteObjective();
        
        if (snapToOrigin)
            draggable.anchoredPosition = _startingPosition;
    }
}
