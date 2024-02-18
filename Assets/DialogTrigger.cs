using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private int textIndex = 0;
    [SerializeField]
    private int levIndex = -1;
    public float textUpMargin = 1f;
    public float textUpDuration = 2;
    public GameObject bubbleLadObject;
    public GameObject bubbleGalObject;
    public DialogueBubble bubbleLad;
    public DialogueBubble bubbleGal;
    public TextMeshProUGUI dialogLad;
    public TextMeshProUGUI dialogGal;    
    private void Start()
    {
        bubbleGal = bubbleGalObject.GetComponent<DialogueBubble>();
        bubbleLad = bubbleLadObject.GetComponent<DialogueBubble>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChatOneShot();
    }
    public void ChatOneShot()
    {
        if(textIndex != levIndex) StartCoroutine(ChatAnimate(textIndex));
        levIndex = textIndex;
    }
    IEnumerator ChatAnimate(int interaction)
    {
        switch (interaction)
        {
            case 0:
                ChatOne(bubbleGalObject, bubbleGal, 0);
                yield return new WaitForSeconds(2);
                StartCoroutine(ChatLerpUp(bubbleGalObject));
                ChatOne(bubbleLadObject, bubbleLad, 0);
                yield return new WaitForSeconds(1);
                break;
            case 1:
                StartCoroutine(ChatLerpUp(bubbleLadObject));
                StartCoroutine(ChatLerpDown(bubbleGalObject));
                ChatOne(bubbleGalObject, bubbleGal, 1);
                yield return new WaitForSeconds(3);
                ChatOne(bubbleGalObject, bubbleGal, 2);
                yield return new WaitForSeconds(2);
                StartCoroutine(ChatLerpUp(bubbleGalObject));
                StartCoroutine(ChatLerpDown(bubbleLadObject));
                ChatOne(bubbleLadObject, bubbleLad, 1);
                yield return new WaitForSeconds(1);
                break;
            case 2:
                StartCoroutine(ChatLerpUp(bubbleLadObject));
                StartCoroutine(ChatLerpDown(bubbleGalObject));
                ChatOne(bubbleGalObject, bubbleGal, 3);
                yield return new WaitForSeconds(3);
                StartCoroutine(ChatLerpUp(bubbleGalObject));
                StartCoroutine(ChatLerpDown(bubbleLadObject));
                ChatOne(bubbleLadObject, bubbleLad, 2);
                yield return new WaitForSeconds(4);
                StartCoroutine(ChatLerpUp(bubbleLadObject));
                StartCoroutine(ChatLerpDown(bubbleGalObject));
                ChatOne(bubbleGalObject, bubbleGal, 4);
                yield return new WaitForSeconds(1);
                break;
            default:
                yield return new WaitForSeconds(1);
                break;
        }
        
    }
    IEnumerator ChatLerpUp(GameObject chatObject)
    {
        float t = 0;
        Vector3 targetPos = chatObject.transform.position + new Vector3(0, textUpMargin, 0);
        while (t < 1f)
        {
            t += Time.deltaTime / textUpDuration;
            chatObject.transform.position = Vector3.Lerp(chatObject.transform.position, targetPos, t);
            yield return null;
        }
    }
    IEnumerator ChatLerpDown(GameObject chatObject)
    {
        float t = 0;
        Vector3 targetPos = chatObject.transform.position + new Vector3(0, -textUpMargin, 0);
        while (t < 1f)
        {
            t += Time.deltaTime / textUpDuration;
            chatObject.transform.position = Vector3.Lerp(chatObject.transform.position, targetPos, t);
            yield return null;
        }
    }
    void ChatOne(GameObject chatObject, DialogueBubble chatBubble, int textNext)
    {
        chatObject.SetActive(true);
        chatBubble.StartDialogue(textNext);
    }
    internal void IncreaseChatIndex()
    {
        ++textIndex;
    }
}
