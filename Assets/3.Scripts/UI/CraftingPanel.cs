using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    private Recipe recipe;
    private UI_CraftingTable craftingTable;

    /// <summary>
    /// Initializes the crafting panel with recipe data and parent crafting table
    /// </summary>
    public void Init(Recipe r, UI_CraftingTable table)
    {
        recipe = r;
        craftingTable = table;

        Debug.Log($"Recipe is {recipe}");
        Debug.Log($"Table is {craftingTable}");

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    /// <summary>
    /// Called when the crafting panel button is clicked
    /// </summary>
    public void OnClick()
    {
        Debug.Log("OnClick Crafting");
        craftingTable.ShowNeedAndOutputItem(recipe);
    }
}
