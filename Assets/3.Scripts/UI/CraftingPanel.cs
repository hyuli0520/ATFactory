using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    private Recipe recipe;
    private UI_CraftingTable craftingTable;

    public void Init(Recipe r, UI_CraftingTable table)
    {
        recipe = r;
        craftingTable = table;

        Debug.Log($"Recipe is {recipe}");
        Debug.Log($"Table is {craftingTable}");

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("OnClick Crafting");
        craftingTable.ShowNeedAndOutputItem(recipe);
    }
}
