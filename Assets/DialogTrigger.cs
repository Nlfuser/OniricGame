using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private float textUpMargin = 1f;
    [SerializeField] private float textUpDuration = 2;
    [SerializeField] private Ease textUpEase;
    [SerializeField] private GameObject bubbleLadObject;
    [SerializeField] private GameObject bubbleGalObject;
    
    private DialogueBubble _bubbleLad;
    private DialogueBubble _bubbleGal;
    private TextMeshProUGUI _dialogLad;
    private TextMeshProUGUI _dialogGal;   
    private int _textIndex = 0;
    private int _levIndex = -1;
    private bool _galBubbleUp;
    
    private void Start()
    {
        _bubbleGal = bubbleGalObject.GetComponent<DialogueBubble>();
        _bubbleLad = bubbleLadObject.GetComponent<DialogueBubble>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChatOneShot();
    }
    
    public void ChatOneShot()
    {
        if (_galBubbleUp)
        {
            ChatLerpDown(bubbleGalObject);
            _galBubbleUp = false;
        }

        HideText(bubbleLadObject);
        HideText(bubbleGalObject);
        StopAllCoroutines();
        if(_textIndex != _levIndex)
            StartCoroutine(ChatAnimate(_textIndex));
        _levIndex = _textIndex;
    }
    
    private IEnumerator ChatAnimate(int interaction)
    {
        switch (interaction)
        {
            case 0:
                ShowText(bubbleGalObject, _bubbleGal, 0);
                yield return new WaitForSeconds(2f);
                ChatLerpUp(bubbleGalObject);
                _galBubbleUp = true;
                ShowText(bubbleLadObject, _bubbleLad, 0);
                yield return new WaitForSeconds(2f);
                HideText(bubbleLadObject);
                HideText(bubbleGalObject);
                ChatLerpDown(bubbleGalObject);
                _galBubbleUp = false;
                yield return new WaitForSeconds(1f);
                break;
            case 1:
                ShowText(bubbleGalObject, _bubbleGal, 1);
                yield return new WaitForSeconds(2f);
                ShowText(bubbleGalObject, _bubbleGal, 2);
                yield return new WaitForSeconds(2f);
                ChatLerpUp(bubbleGalObject);
                _galBubbleUp = true;
                ShowText(bubbleLadObject, _bubbleLad, 1);
                yield return new WaitForSeconds(3f);
                HideText(bubbleLadObject);
                HideText(bubbleGalObject);
                ChatLerpDown(bubbleGalObject);
                _galBubbleUp = false;
                yield return new WaitForSeconds(1f);
                break;
            case 2:
                ShowText(bubbleGalObject, _bubbleGal, 3);
                yield return new WaitForSeconds(3);
                ChatLerpUp(bubbleGalObject);
                ShowText(bubbleLadObject, _bubbleLad, 2);
                yield return new WaitForSeconds(3);
                ChatLerpUp(bubbleLadObject);
                ChatLerpDown(bubbleGalObject);
                ShowText(bubbleGalObject, _bubbleGal, 4);
                yield return new WaitForSeconds(3);
                HideText(bubbleLadObject);
                HideText(bubbleGalObject);
                ChatLerpDown(bubbleLadObject);
                yield return new WaitForSeconds(1);
                break;
            default:
                yield return new WaitForSeconds(1);
                break;
        }
        
    }
    private void ChatLerpUp(GameObject chatObject)
    {
        chatObject.transform.DOMoveY(chatObject.transform.position.y + textUpMargin, textUpDuration).SetEase(textUpEase);
    }
    
    private void ChatLerpDown(GameObject chatObject)
    {
        chatObject.transform.DOMoveY(chatObject.transform.position.y - textUpMargin, textUpDuration).SetEase(textUpEase);
    }
    
    private void ShowText(GameObject chatObject, DialogueBubble chatBubble, int textNext)
    {
        chatObject.SetActive(true);
        chatBubble.StartDialogue(textNext);
    }
    
    private void HideText(GameObject chatObject)
    {
        chatObject.SetActive(false);
    }
    
    internal void IncreaseChatIndex()
    {
        ++_textIndex;
    }
}
