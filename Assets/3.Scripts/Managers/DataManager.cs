using System;
using UnityEngine;

public class DataManager
{
    /// <summary>Key used for saving and loading inventory data</summary>
    private const string SAVE_KEY = "PlayerInventorySaveAll";
    private UI_Inventory inven;
    private UI_QuickSlot hotbar;
    public DataManagerHelper helper;

    /// <summary>
    /// Initializes references to inventory, hotbar, and DataManagerHelper
    /// </summary>
    public void Init()
    {
        inven = Managers.UI.inven;
        hotbar = Managers.UI.hotbar;

        helper = GameObject.Find("DataManagerHelper").GetComponent<DataManagerHelper>();
        helper.Init();
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
    /// Loads all inventory and hotbar data from persistent storage
    /// </summary>
    public void LoadAll()
    {
        if (!ES3.KeyExists(SAVE_KEY))
        {
            Debug.Log("Not found inventory data");
            return;
        }

        var data = ES3.Load<InventorySaveData>(SAVE_KEY);
        int totalCount = data.slots.Count;

        int invenCount = inven.slots.Count;

        for (int i = 0; i < invenCount && i < totalCount; i++)
        {
            LoadSlot(inven.slots[i], data.slots[i]);
        }

        for (int i = 0; i < hotbar.slots.Count; i++)
        {
            int index = invenCount + i;
            if (index < totalCount)
                LoadSlot(hotbar.slots[i], data.slots[index]);
        }

        Debug.Log("Load complete");
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

    /// <summary>
    /// Loads item data into a specific UI slot from saved SlotData
    /// </summary>
    private void LoadSlot(UI_Inven_Item uiSlot, SlotData slotData)
    {
        if (!string.IsNullOrEmpty(slotData.itemName))
        {
            var item = helper.GetItem(slotData.itemName);
            uiSlot.AddItem(item, slotData.count);
        }
    }
}
