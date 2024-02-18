using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingImage : MonoBehaviour
{
    [SerializeField] private List<Sprite> garbageSprites;

    private void Update()
    {
        foreach (var sprite in garbageSprites)
        {
            if (GetComponent<Image>().sprite == sprite)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
                break;
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
            }
        }
    }
}
