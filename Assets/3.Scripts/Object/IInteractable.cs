using UnityEngine;

/// <summary>
/// Interactable Interface
/// </summary>
public interface IInteractable
{
    string GetInteractionText();
    void Interact(PlayerController player);
}
