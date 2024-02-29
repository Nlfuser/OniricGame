using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float newScale = 1.25f;
    [SerializeField] private Ease ease = Ease.OutExpo;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        ((RectTransform) transform).DOScale(newScale, 0.5f).SetUpdate(true).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ((RectTransform) transform).DOScale(1f, 0.5f).SetUpdate(true).SetEase(ease);
    }
}
