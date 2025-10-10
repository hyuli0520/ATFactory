using UnityEngine;

public class Digger : MonoBehaviour, IInteractable
{
    [Header("Mining Settings")]
    public float mineInterval = 1.0f; // Time between mining attemps
    public int minePower = 2; // Mining Strength
    public float mineRange = 1.0f; // Mining distance

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= mineInterval)
        {
            _timer = 0;
            TryMine();
        }
    }

    /// <summary>
    /// Attempts to mine a minable object below this object
    /// </summary>
    private void TryMine()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, mineRange))
        {
            IMinable minable = hit.collider.GetComponent<IMinable>();
            if (minable != null)
            {
                minable.MineDigger(minePower, minable);
                Debug.Log("±¤¼® Ã¤±¼µÊ");
            }
        }
    }

    /// <summary>
    /// Visualizes the mining range in the editor
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * mineRange);
    }

    /// <summary>
    /// Returns the interaction text for this object
    /// </summary>
    public string GetInteractionText()
    {
        return "E: Open Digger";
    }

    /// <summary>
    /// Opens the digger UI and inventory
    /// </summary>
    public void Interact(PlayerController player)
    {
        var ui = Managers.UI;
        ui.digger.gameObject.SetActive(true);
        ui.activeInven = true;
        ui.inven.gameObject.SetActive(ui.activeInven);
        Cursor.lockState = CursorLockMode.None;
    }
}
