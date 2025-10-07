using System.Collections.Generic;
using UnityEngine;

public class DataManagerHelper : MonoBehaviour
{
    [SerializeField] private List<ItemData> allItems;

    private Dictionary<string, ItemData> itemDic;

    /// <summary>
    /// Initializes the item dictionary with all available items
    /// </summary>
    public void Init()
    {
        itemDic = new Dictionary<string, ItemData>();
        foreach (var item in allItems)
            itemDic[item.itemName] = item;
    }

    /// <summary>
    /// Returns the ItemData associated with the given item name
    /// </summary>
    public ItemData GetItem(string name)
    {
        itemDic.TryGetValue(name, out var data);
        return data;
    }
}
