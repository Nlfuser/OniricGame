using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    public bool cantPlace;
    public bool dynamic;
    public GameObject placedPrefab;
    public Sprite image;
    public List<Sprite> dynamicImages;
    public int evolution = -1;
    public bool isCompleted;
}
