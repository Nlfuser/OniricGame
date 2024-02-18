using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private List<string> dialogueText;
    [SerializeField] private GameObject content;
    private TypewriterByCharacter _typewriter;
    private int _currentLine;

    private void Awake()
    {
        _typewriter = transform.GetChild(0).GetComponentInChildren<TypewriterByCharacter>();
    }

    public void StartDialogue()
    {
        content.SetActive(true);
        _typewriter.GetComponent<TMP_Text>().text = dialogueText[_currentLine++ - 1];
        _typewriter.StartShowingText(true);
    }
    public void StartDialogue(int index)
    {
        content.SetActive(true);
        _typewriter.GetComponent<TMP_Text>().text = dialogueText[index];
        _typewriter.StartShowingText(true);
    }
}
