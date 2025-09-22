using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UI_QuickSlot : MonoBehaviour
{
    public List<UI_QuickSlot_Item> slots = new List<UI_QuickSlot_Item>();
    public int currentIndex;

    public void Start()
    {
    }

    public void AddItem(ItemData item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.CanAdd(item))
            {
                int space = slot.maxStack - slot.count;
                int toAdd = Mathf.Min(space, amount);
                slot.Add(toAdd);
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
            Addressables.InstantiateAsync("UI_QuickSlot_Item", parent).Completed += (handle) =>
            {
                var item = handle.Result.GetComponent<UI_QuickSlot_Item>();
                slots.Add(item);

                slots[0].SetOutline(true);
            };
        }
    }

    public void WheelSlot(InputSystem_Actions input)
    {
        var scroll = input.UI.ScrollWheel.ReadValue<Vector2>().y;
        if (scroll > 0f)
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = slots.Count - 1;
            UpdateHotbarUI();
        }
        else if (scroll < 0f)
        {
            currentIndex++;
            if (currentIndex >= slots.Count)
                currentIndex = 0;
            UpdateHotbarUI();
        }
    }

    public void UpdateHotbarUI()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].SetOutline(false);
        slots[currentIndex].SetOutline(true);
    }
}
