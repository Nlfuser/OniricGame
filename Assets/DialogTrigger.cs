using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private ItemSO flowerItem;
    [SerializeField] private ItemSO flowerItem2;
    [SerializeField] private float textUpMargin = 1f;
    [SerializeField] private float textUpDuration = 2;
    [SerializeField] private Ease textUpEase;
    [SerializeField] private GameObject bubbleLadObject;
    [SerializeField] private GameObject bubbleGalObject;
    [SerializeField] private CanvasGroup galCanvasGroup;
    [SerializeField] private CanvasGroup ladCanvasGroup;
    
    private DialogueBubble _bubbleLad;
    private DialogueBubble _bubbleGal;
    private int _textIndex = 0;
    private int _levIndex = -1;
    private bool _hasFlowerDialoguePlayed;
    private bool _galBubbleUp;
    private float _itemHintTimer = 15f;
    
    private void Start()
    {
        _bubbleGal = bubbleGalObject.GetComponent<DialogueBubble>();
        _bubbleLad = bubbleLadObject.GetComponent<DialogueBubble>();
        InventoryUI.instance.OnPlace += OnItemPlaced;
    }

    private void OnItemPlaced(ItemSO item)
    {
        _itemHintTimer = 15f;
        if(_hasFlowerDialoguePlayed)
            return;
        if (item == flowerItem || item == flowerItem2)
        {
            _textIndex = 2;
            ChatOneShot();
            _hasFlowerDialoguePlayed = true;
        }
    }

    private void Update()
    {
        _itemHintTimer -= Time.deltaTime;
        if (_itemHintTimer <= 0f)
        {
            _textIndex = 1;
            ChatOneShot();
            _itemHintTimer = Mathf.Infinity;
        }
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

        bubbleLadObject.SetActive(false);
        bubbleGalObject.SetActive(false);
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
                ShowText(bubbleGalObject, _bubbleGal, galCanvasGroup, 0);
                yield return new WaitForSeconds(2f);
                ChatLerpUp(bubbleGalObject);
                _galBubbleUp = true;
                ShowText(bubbleLadObject, _bubbleLad, ladCanvasGroup, 0);
                yield return new WaitForSeconds(2f);
                HideText(bubbleLadObject, ladCanvasGroup);
                HideText(bubbleGalObject, galCanvasGroup);
                ChatLerpDown(bubbleGalObject);
                _galBubbleUp = false;
                yield return new WaitForSeconds(1f);
                break;
            case 1:
                ShowText(bubbleGalObject, _bubbleGal, galCanvasGroup, 1);
                yield return new WaitForSeconds(2f);
                ShowText(bubbleGalObject, _bubbleGal, galCanvasGroup, 2);
                yield return new WaitForSeconds(2f);
                ChatLerpUp(bubbleGalObject);
                _galBubbleUp = true;
                ShowText(bubbleLadObject, _bubbleLad, ladCanvasGroup, 1);
                yield return new WaitForSeconds(3f);
                HideText(bubbleLadObject, ladCanvasGroup);
                HideText(bubbleGalObject, galCanvasGroup);
                ChatLerpDown(bubbleGalObject);
                _galBubbleUp = false;
                yield return new WaitForSeconds(1f);
                break;
            case 2:
                ShowText(bubbleGalObject, _bubbleGal, galCanvasGroup, 3);
                yield return new WaitForSeconds(3);
                ChatLerpUp(bubbleGalObject);
                ShowText(bubbleLadObject, _bubbleLad, ladCanvasGroup, 2);
                yield return new WaitForSeconds(3);
                ChatLerpUp(bubbleLadObject);
                ChatLerpDown(bubbleGalObject);
                ShowText(bubbleGalObject, _bubbleGal, galCanvasGroup, 4);
                yield return new WaitForSeconds(3);
                HideText(bubbleLadObject, ladCanvasGroup);
                HideText(bubbleGalObject, galCanvasGroup);
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
    
    private void ShowText(GameObject chatObject, DialogueBubble chatBubble, CanvasGroup chatCanvasGroup, int textNext)
    {
        chatObject.SetActive(true);
        chatCanvasGroup.DOFade(1f, 0.25f);
        chatObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.25f);
        chatBubble.StartDialogue(textNext);
    }
    
    private void HideText(GameObject chatObject, CanvasGroup chatCanvasGroup)
    {
        chatCanvasGroup.DOFade(0f, 0.25f);
        chatObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.25f);
    }
    
    internal void IncreaseChatIndex()
    {
        ++_textIndex;
    }
}
