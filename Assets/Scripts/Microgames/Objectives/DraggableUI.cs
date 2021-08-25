using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Microgames.Objectives
{
    public class DraggableUI : ObjectiveBase, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
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
        private bool _allowDrag = false;

        private void Awake()
        {
            _image = draggable.GetComponentInChildren<Image>();
            _imageColor = _image.color;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(_allowDrag)
                draggable.anchoredPosition += eventData.delta / (canvas.renderMode != RenderMode.WorldSpace ? canvas.scaleFactor : 1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_allowDrag) return;
        
            _startingPosition = draggable.anchoredPosition;

            _imageColor.a = .8f;
            _image.color = _imageColor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_allowDrag) return;
        
            _imageColor.a = 1f;
            _image.color = _imageColor;
        
            if(destination)
                if (Vector2.Distance(draggable.anchoredPosition, destination.anchoredPosition) < distanceThreshold)
                    CompleteObjective();
        
            if (snapToOrigin)
                draggable.anchoredPosition = _startingPosition;
        
            _allowDrag = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameObject clicked = eventData.pointerPressRaycast.gameObject;
        
            Debug.Log(clicked);
        
            if (clicked == draggable.gameObject)
                _allowDrag = true;
        }
    }
}
