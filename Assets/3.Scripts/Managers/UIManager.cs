using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem.Samples.RebindUI;

public class UIManager
{
    public Transform canvasTransform;
    public UI_Inventory inven;
    public UI_QuickSlot hotbar;
    public UI_Digger digger;
    public UI_CraftingTable craftingTable;
    public UI_BindingKey key;
    public UI_Setting setting;
    public TMP_Text interactionText;
    public bool activeInven = false;

    public void Init()
    {
        // Instantiate Canvas
        var canvasPrefab = Resources.Load<GameObject>("UI_Canvas");
        var canvasInstance = Object.Instantiate(canvasPrefab);
        canvasTransform = canvasInstance.transform;

        // Instantiate QuickSlot UI
        {
            var hotbarPrefab = Resources.Load<UI_QuickSlot>("UI_QuickSlot");
            hotbar = Object.Instantiate(hotbarPrefab, canvasTransform);

            var gridPanel = Util.FindChild<Transform>(hotbar.gameObject, "GridPanel");
            hotbar.CreateSlots(5, gridPanel);
        }

        // Instantiate Inventory UI
        {
            var invenPrefab = Resources.Load<UI_Inventory>("UI_Inventory");
            inven = Object.Instantiate(invenPrefab, canvasTransform);
            inven.gameObject.SetActive(false);

            var gridPanel = Util.FindChild<Transform>(inven.gameObject, "GridPanel");
            inven.CreateSlots(42, gridPanel);
        }

        // Instantiate Digger UI
        {
            var diggerPrefab = Resources.Load<UI_Digger>("UI_Digger");
            digger = Object.Instantiate(diggerPrefab, canvasTransform);
            digger.gameObject.SetActive(false);
        }

        // Instantiate CraftingTable UI
        {
            var craftingTablePrefab = Resources.Load<UI_CraftingTable>("UI_CraftingTable");
            craftingTable = Object.Instantiate(craftingTablePrefab, canvasTransform);
            craftingTable.gameObject.SetActive(false);
        }

        // Instantiate Binding Key UI
        {
            var settingPrefab = Resources.Load<UI_Setting>("UI_Setting");
            setting = Object.Instantiate(settingPrefab, canvasTransform);
            setting.gameObject.SetActive(false);
        }

        // Find InteractionText
        interactionText = Util.FindChild<TMP_Text>(canvasTransform.gameObject, "InteractionText");
    }
}