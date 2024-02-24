using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Item Manager", fileName = "New Item Manager")]
public class ItemManager : ScriptableObject
{
    [ListDrawerSettings(NumberOfItemsPerPage = 20)] public List<ItemSO> items;

    private void OnEnable()
    {
        Refresh();
    }

    [Button(ButtonSizes.Small, ButtonStyle.FoldoutButton)]
    private void Refresh()
    {
#if UNITY_EDITOR
        var itemSoList = AssetDatabase.FindAssets("t:ItemSO");
        foreach (var item in itemSoList)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            if(!items.Contains(AssetDatabase.LoadAssetAtPath<ItemSO>(path)))
                items.Add(AssetDatabase.LoadAssetAtPath<ItemSO>(path));
        }
#endif
    }
}