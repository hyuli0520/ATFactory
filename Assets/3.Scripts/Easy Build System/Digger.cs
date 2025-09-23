using UnityEngine;

public class Digger : MonoBehaviour, IInteractable
{
    [Header("Mining Settings")]
    public float mineInterval = 1.0f;
    public int minePower = 2;
    public float mineRange = 1.0f;
    
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * mineRange);
    }

    public string GetInteractionText()
    {
        return "E: Open Digger";
    }

    public void Interact(PlayerController player)
    {
        var ui = Managers.UI;
        ui.digger.gameObject.SetActive(true);
        ui.activeInven = true;
        ui.inven.gameObject.SetActive(ui.activeInven);
        Cursor.lockState = CursorLockMode.None;
    }
}
