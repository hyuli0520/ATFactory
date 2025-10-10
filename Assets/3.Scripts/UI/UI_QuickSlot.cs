using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class UI_QuickSlot : MonoBehaviour
{
    public List<UI_QuickSlot_Item> slots = new List<UI_QuickSlot_Item>();
    public int currentIndex;

    public BuildingPlacer placer;

    public void Start()
    {
        placer = BuildingPlacer.Instance;
    }

    /// <summary>
    /// Creates inventory slots and adds them to the slots list
    /// </summary>
    public void CreateSlots(int count, Transform parent = null)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Resources.Load<UI_QuickSlot_Item>("UI_QuickSlot_Item");
            item = Instantiate(item, parent);
            slots.Add(item);

            slots[0].SetOutline(true);
        }
    }

    /// <summary>
    /// Removes the given item from inventory if available
    /// </summary>
    public int RemoveItem(ItemData item, int amount = 1)
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
                    Debug.Log($"success remove item, remain {slot.count}");
                    return slot.count;
                }
            }
        }

        Debug.Log("fail remove item");
        return 0;
    }

    /// <summary>
    /// Switches quick slots using mouse scroll input
    /// </summary>
    public void WheelSlot(InputActionAsset input)
    {
        var scroll = input["ScrollWheel"].ReadValue<Vector2>().y;
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

    /// <summary>
    /// Updates the UI outline to reflect the current slot selection
    /// </summary>
    public void UpdateHotbarUI()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].SetOutline(false);
        slots[currentIndex].SetOutline(true);
        CheckBuildMode();
    }

    /// <summary>
    /// Updates build mode based on the currently selected slot item
    /// </summary>
    public void CheckBuildMode()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.NONE);

        ItemData item = slots[currentIndex].itemData;
        if (item == null)
            return;

        if(item.itemType == ItemType.Build)
        {
            placer.SelectBuildingPart(item.part);
        }
    }
}
