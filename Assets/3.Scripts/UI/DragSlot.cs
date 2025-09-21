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

    public void DragSetImage(ItemData itemData)
    {
        imageItem.sprite = itemData.icon;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = imageItem.color;
        color.a = alpha;
        imageItem.color = color;
    }
}
