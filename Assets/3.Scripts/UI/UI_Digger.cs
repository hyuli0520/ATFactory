using UnityEngine;
using UnityEngine.UI;

public class UI_Digger : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button getAllButton;
    [SerializeField] public UI_Inven_Item item;

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void Digging(ItemData itemData, int count = 1)
    {
        if (!item.IsEmpty && item.itemData == itemData && item.count < item.maxStack)
        {
            int space = item.maxStack - item.count;
            int toAdd = Mathf.Min(space, count);
            item.Add(toAdd);
            item.AddItem(item.itemData, item.count);
            count -= toAdd;

            if (count <= 0) return;
        }

        if (item.IsEmpty)
        {
            item.itemData = itemData;
            item.count = Mathf.Min(count, item.maxStack);
            item.maxStack = itemData.maxStack;

            item.AddItem(item.itemData);

            count -= item.count;

            if (count <= 0) return;
        }
    }

    public void GetAll()
    {
        Managers.UI.inven.AddItem(item.itemData, item.count);
    }
}
