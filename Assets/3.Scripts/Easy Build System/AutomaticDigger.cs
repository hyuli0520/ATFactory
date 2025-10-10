using UnityEngine;
using UnityEngine.AddressableAssets;

public class AutomaticDigger : MonoBehaviour
{
    [Header("Mining Settings")]
    public float mineInterval = 1.0f; // Time between mining attemps
    public int minePower = 2; // Mining Strength
    public float mineRange = 1.0f; // Mining distance

    private float _timer;

    public GameObject spawner;

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
                var res = minable.AutoDigger(minePower);
                if (res)
                {
                    minable.MakeMinedItem();
                    Debug.Log("±¤¼®");
                }
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
}
