using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Represents a single item slot's saved data,
/// containing the item name and its quantity
/// </summary>
[System.Serializable]
public class SlotData
{
    public string itemName;
    public int count;
}

/// <summary>
/// Contains all saved item slots from both inventory and hotbar
/// </summary>
[System.Serializable]
public class InventorySaveData
{
    public List<SlotData> slots = new();
}