using System;
using Febucci.UI;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject content;
    [SerializeField] private Vector2 offset;
    private TypewriterByCharacter _typewriter;

    private void Awake()
    {
        _typewriter = transform.GetChild(0).GetComponentInChildren<TypewriterByCharacter>();
    }

    private void Update()
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, player.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPoint, UnityEngine.Camera.main, out var canvasPos);
        content.GetComponent<RectTransform>().anchoredPosition = canvasPos + offset;
    }

    public void StartDialogue()
    {
        content.SetActive(true);
        _typewriter.StartShowingText(true);
    }
}
