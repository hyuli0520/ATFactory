using UnityEngine;
using UnityEngine.AddressableAssets;

public class AutomaticDigger : MonoBehaviour
{
    [Header("Mining Settings")]
    public float mineInterval = 1.0f;
    public int minePower = 2;
    public float mineRange = 1.0f;

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
                    SpawnMine();
                    Debug.Log("±¤¼®");
                }
            }
        }
    }

    private void SpawnMine()
    {
        Addressables.InstantiateAsync("Rock", spawner.transform).Completed += (handle) =>
        {

        };
    }
}
