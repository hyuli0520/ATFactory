using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "E: Open Table";
    }

    public void Interact(PlayerController player)
    {
        var ui = Managers.UI;
        ui.craftingTable.gameObject.SetActive(true);
        ui.activeInven = true;
        ui.inven.gameObject.SetActive(ui.activeInven);
        Cursor.lockState = CursorLockMode.None;
    }
}
