using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    public bool isNote;
    public bool cantPlace;
    public bool dynamic;
    [HideIf("cantPlace")] public GameObject placedPrefab;
    [HideIf("dynamic")] public Sprite image;
    [ShowIf("dynamic")] public List<Sprite> dynamicImages;
    [HideInInspector] public int evolution = -1;
    [HideInInspector] public bool isCompleted;
}
