using EasyBuildSystem.Features.Runtime.Buildings.Part;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public BuildingPart part;
    public string itemName;
    public Sprite icon;
    public int maxStack = 99;
}

public enum ItemType
{
    Build,
    Ore,
}

[System.Serializable]
public class UI_Inven_Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemData itemData;
    public int count;
    public int maxStack;

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;

    public bool CanAdd(ItemData newItem) =>
        itemData != null && itemData == newItem && count < maxStack;

    public void Add(int amount)
    {
        count = Mathf.Min(count + amount, maxStack);
    }

    public bool IsFull => count >= maxStack;
    public bool IsEmpty => itemData == null;

    private void Start()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty) return;

        DragSlot.instance.dragSlot = this;
        DragSlot.instance.DragSetImage(itemData);
        DragSlot.instance.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void SetColor(float _alpha)
    {
        Color color = iconImage.color;
        color.a = _alpha;
        iconImage.color = color;
    }

    public void AddItem(ItemData data, int itemCount = 1)
    {
        itemData = data;
        count = itemCount;
        iconImage.sprite = data.icon;

        iconImage.gameObject.SetActive(true);
        countText.gameObject.SetActive(true);
        countText.text = count.ToString();

        SetColor(1);
    }

    public void SetSlotCount(int itemCount)
    {
        count += itemCount;
        countText.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        itemData = null;
        count = 0;
        iconImage.sprite = null;
        SetColor(0);

        countText.text = "0";
        countText.gameObject.SetActive(false);
    }

    public void ChangeSlot()
    {
        var from = DragSlot.instance.dragSlot;
        if (from == null) return;

        if (from.itemData != null && itemData != null && from.itemData == itemData)
        {
            int total = count + from.count;
            int max = itemData.maxStack;

            if (total <= max)
            {
                count = total;
                from.ClearSlot();
            }
            else
            {
                count = max;
                from.count = total - max;
                from.AddItem(from.itemData, from.count);
            }

            AddItem(itemData, count);
        }
        else
        {
            ItemData targetItem = itemData;
            int targetCount = count;

            if (from.itemData != null)
                AddItem(from.itemData, from.count);
            else
                ClearSlot();

            if (targetItem != null)
                from.AddItem(targetItem, targetCount);
            else
                from.ClearSlot();
        }
        Managers.UI.hotbar.CheckBuildMode();
    }
}