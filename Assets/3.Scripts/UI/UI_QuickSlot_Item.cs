using UnityEngine;
using UnityEngine.UI;

public class UI_QuickSlot_Item : UI_Inven_Item
{
    [SerializeField] private GameObject outLine;

    private void Start()
    {
    }

    /// <summary>
    /// Show outline highlight for this quick slot item
    /// </summary>
    public void SetOutline(bool isOutline)
    {
        outLine.SetActive(isOutline);
    }
}
