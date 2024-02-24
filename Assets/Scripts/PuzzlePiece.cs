using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float dist;
    [SerializeField] private GameObject solution;
    [SerializeField] private GameObject canvas;
    private Vector2 _startDragPosition;
    private Vector2 _pointerOffset;
    private bool _canDrag = true;

    private void Awake()
    {
        _canDrag = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, UnityEngine.Camera.main, out _startDragPosition);
        _pointerOffset = (Vector2)transform.localPosition - _startDragPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_canDrag)
            return;
        Vector2 currentDragPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, UnityEngine.Camera.main, out currentDragPosition);
        transform.localPosition = currentDragPosition + _pointerOffset;
        transform.SetAsLastSibling();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        if (Vector2.Distance(transform.position, solution.transform.position) < dist)
        {
            transform.position = solution.transform.position;
            _canDrag = false;
            GetComponent<Image>().raycastTarget = false;
            GameManager.instance.AddPuzzlePiece();
        }
    }
}
