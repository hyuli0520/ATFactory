using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStack = 99;
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
        countText.text = itemCount.ToString();

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
        var from = DragSlot.instance.dragSlot;  // 드래그해온 슬롯
        if (from == null) return;

        // 현재 슬롯 데이터 백업
        ItemData targetItem = itemData;
        int targetCount = count;

        // 드래그 슬롯 아이템 → 현재 슬롯으로 이동
        if (from.itemData != null)
            AddItem(from.itemData, from.count);
        else
            ClearSlot();

        // 원래 현재 슬롯 아이템 → 드래그 슬롯으로 이동
        if (targetItem != null)
            from.AddItem(targetItem, targetCount);
        else
            from.ClearSlot();
    }
}