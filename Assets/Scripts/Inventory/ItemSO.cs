using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    public GameObject placedPrefab;
    public Sprite image;
}
