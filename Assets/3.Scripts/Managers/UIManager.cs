using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager
{
    public Transform canvasTransform;
    public UI_Inventory inven;
    public UI_QuickSlot hotbar;
    public UI_Digger digger;
    public TMP_Text interactionText;
    public bool activeInven = false;

    public void Init()
    {
        Addressables.InstantiateAsync("UI_Canvas").Completed += (handle) =>
        {
            canvasTransform = handle.Result.transform;
            Addressables.InstantiateAsync("UI_Inventory", canvasTransform).Completed += (handle) =>
            {
                inven = handle.Result.GetComponent<UI_Inventory>();
                inven.gameObject.SetActive(false);

                if (inven == null)
                    Debug.Log("inven is null");

                var gridPanel = Util.FindChild<Transform>(inven.gameObject, "GridPanel");

                if (gridPanel == null)
                    Debug.Log("GridPanel 못 찾음");

                inven.CreateSlots(42, gridPanel);
            };
            Addressables.InstantiateAsync("UI_QuickSlot", canvasTransform).Completed += (handle) =>
            {
                hotbar = handle.Result.GetComponent<UI_QuickSlot>();
                if (hotbar == null)
                    Debug.Log("hotbar is null");

                var gridPanel = Util.FindChild<Transform>(hotbar.gameObject, "GridPanel");
                if (gridPanel == null)
                    Debug.Log("GridPanel 못 찾음");

                hotbar.CreateSlots(5, gridPanel);
            };
            Addressables.InstantiateAsync("UI_Digger", canvasTransform).Completed += (handle) =>
            {
                digger = handle.Result.GetComponent<UI_Digger>();
                if (digger == null)
                    Debug.Log("hotbar is null");

                var gridPanel = Util.FindChild<Transform>(digger.gameObject, "GridPanel");
                if (gridPanel == null)
                    Debug.Log("GridPanel 못 찾음");

                digger.gameObject.SetActive(false);
            };
            interactionText = Util.FindChild<TMP_Text>(canvasTransform.gameObject, "InteractionText");
        };
    }
}
