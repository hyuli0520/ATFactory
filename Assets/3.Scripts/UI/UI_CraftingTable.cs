using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UI_CraftingTable : MonoBehaviour
{
    public List<Recipe> recipe;

    public ScrollViewController scroll;
    public List<GameObject> craftingItem;
    public GridLayoutGroup grid;

    public Recipe nowRecipe;

    public UI_Inven_Item output;
    public ItemData outputItem;

    private void Start()
    {
        foreach (var item in recipe)
        {
            scroll.AddNewUIObject(item.itemData.icon, item.itemData.itemName, ui =>
            {
                Debug.Log("Callback is success");
                ui.gameObject.AddComponent<CraftingPanel>().Init(item, this);
            });
        }
    }

    /// <summary>
    /// Displays required materials and output item for the selected recipe
    /// </summary>
    public void ShowNeedAndOutputItem(Recipe r)
    {
        foreach (Transform child in grid.transform)
        {
            Addressables.Release(child.gameObject);
            Destroy(child.gameObject);
        }

        for (int i = 0; i < r.materials.Count; i++)
        {
            int index = i;
            Addressables.InstantiateAsync("UI_NeedItem", grid.transform).Completed += (handle) =>
            {
                var img = handle.Result.GetComponent<Image>();
                var text = handle.Result.GetComponentInChildren<TMP_Text>();
                nowRecipe = r;
                img.sprite = r.materials[index].material.icon;
                text.text = r.materials[index].materialCount.ToString();
            };
        }
    }

    /// <summary>
    /// Attempts to craft the current recipe by removing materials and adding the result to output
    /// </summary>
    public void ClickCraftingButton()
    {
        bool canCraft = true;
        foreach (var mat in nowRecipe.materials)
        {
            if (!Managers.UI.inven.HasItem(mat.material, mat.materialCount))
            {
                canCraft = false;
                break;
            }
        }

        if (!canCraft)
            return;

        foreach (var mat in nowRecipe.materials)
        {
            Managers.UI.inven.RemoveItem(mat.material, mat.materialCount);
        }

        output.AddItem(nowRecipe.itemData, nowRecipe.outputCount);
    }

    /// <summary>
    /// Close the crafting table ui
    /// </summary>
    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
