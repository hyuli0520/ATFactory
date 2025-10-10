using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public UI_Inven_Item dragSlot;

    [SerializeField]
    private Image imageItem;

    private void Start()
    {
        instance = this;
        SetColor(0);
    }

    /// <summary>
    /// Sets the drag image sprite to the given item and makes it visible
    /// </summary>
    public void DragSetImage(ItemData itemData)
    {
        imageItem.sprite = itemData.icon;
        SetColor(1);
    }

    /// <summary>
    /// Updates the drag image alpha value
    /// </summary>
    public void SetColor(float alpha)
    {
        Color color = imageItem.color;
        color.a = alpha;
        imageItem.color = color;
    }
}
