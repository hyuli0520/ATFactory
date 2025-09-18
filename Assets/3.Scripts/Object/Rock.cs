using UnityEngine;

public class Rock : MonoBehaviour, IMineable
{
    [SerializeField] private int durability = 5;
    public bool IsDepleted => durability <= 0;

    public void Mine(int power)
    {
        if (IsDepleted) 
            return;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
            Destroy(gameObject);
    }
}
