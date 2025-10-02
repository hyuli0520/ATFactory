using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UI_Inventory : MonoBehaviour
{
    public List<UI_Inven_Item> slots = new List<UI_Inven_Item>();

    /// <summary>
    /// Add an item to inventory
    /// Try to stack into first slot, then uses empty slot
    /// </summary>
    public void AddItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.itemData == item && slot.count < slot.maxStack)
            {
                int space = slot.maxStack - slot.count;
                int toAdd = Mathf.Min(space, amount);
                slot.Add(toAdd);
                slot.AddItem(slot.itemData, slot.count);
                amount -= toAdd;

                if (amount <= 0) return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.itemData = item;
                slot.count = Mathf.Min(amount, item.maxStack);
                slot.maxStack = item.maxStack;

                slot.AddItem(slot.itemData);

                amount -= slot.count;

                if (amount <= 0) return;
            }
        }

        if (amount > 0)
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return;
        }
    }

    /// <summary>
    /// Removes the given item from inventory if available
    /// </summary>
    public bool RemoveItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            Debug.Log($"slot.itemData: {slot.itemData}, item: {item}");
            if (slot.itemData == item)
            {
                Debug.Log($"slot.count: {slot.count}, amount: {amount}");
                if (slot.count >= amount)
                {
                    slot.count -= amount;
                    slot.SetSlotCount(slot.count);
                    Debug.Log("success remove item");
                    return true;
                }
            }
        }

        Debug.Log("fail remove item");
        return false;
    }

    /// <summary>
    /// Creates inventory slots and adds them to the slots list
    /// </summary>
    public void CreateSlots(int count, Transform parent = null)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Resources.Load<UI_Inven_Item>("UI_Inven_Item");
            item = Instantiate(item, parent);
            slots.Add(item);
        }
    }
}
