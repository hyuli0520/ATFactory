using System;
using UnityEngine;

public class DataManager
{
    /// <summary>Key used for saving and loading inventory data</summary>
    private const string SAVE_KEY = "PlayerInventorySaveAll";
    private UI_Inventory inven;
    private UI_QuickSlot hotbar;

    /// <summary>
    /// Initializes references to inventory, hotbar, and DataManagerHelper
    /// </summary>
    public void Init()
    {
        inven = Managers.UI.inven;
        hotbar = Managers.UI.hotbar;
    }

    /// <summary>
    /// Saves all inventory and hotbar data to persistent storage
    /// </summary>
    public void SaveAll()
    {
        var data = new InventorySaveData();

        foreach (var item in inven.slots)
        {
            data.slots.Add(MakeSlotData(item));
        }

        foreach (var item in hotbar.slots)
        {
            data.slots.Add(MakeSlotData(item));
        }

        ES3.Save(SAVE_KEY, data);
        Debug.Log($"Saved {data.slots.Count} slots (Inventory + Hotbar)");
    }

    /// <summary>
    /// Converts a UI inventory slot into a serializable SlotData structure
    /// </summary>
    private SlotData MakeSlotData(UI_Inven_Item slot)
    {
        var slotData = new SlotData();

        if (slot.itemData != null)
        {
            slotData.itemName = slot.itemData.name;
            slotData.count = slot.count;
        }
        else
        {
            slotData.itemName = "";
            slotData.count = 0;
        }

        return slotData;
    }
}
