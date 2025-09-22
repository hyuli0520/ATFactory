using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UI_Inventory : MonoBehaviour
{
    public List<UI_Inven_Item> slots = new List<UI_Inven_Item>();

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

    public void CreateSlots(int count, Transform parent = null)
    {
        for (int i = 0; i < count; i++)
        {
            Addressables.InstantiateAsync("UI_Inven_Item", parent).Completed += (handle) =>
            {
                var item = handle.Result.GetComponent<UI_Inven_Item>();
                slots.Add(item);
            };
        }
    }
}
