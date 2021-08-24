using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : Objective, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField] private RectTransform draggableRectTransform;
    [SerializeField] private RectTransform destination;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image image;
    
    [Header("Settings")]
    [SerializeField] private bool snapToOrigin;
    
    private Color _imageColor;
    private Vector2 startingPosition = Vector2.zero;

    private void Awake()
    {
        _imageColor = image.color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggableRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log(Vector2.Distance(draggableRectTransform.anchoredPosition, destination.anchoredPosition));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startingPosition = draggableRectTransform.anchoredPosition;
        
        _imageColor.a = .8f;
        image.color = _imageColor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (snapToOrigin)
            draggableRectTransform.anchoredPosition = startingPosition;
        
        _imageColor.a = 1f;
        image.color = _imageColor;
    }
}
