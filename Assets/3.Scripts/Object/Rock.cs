using UnityEngine;

public class Rock : MonoBehaviour, IMinable
{
    [SerializeField] private int durability = 5;
    public bool IsDepleted => durability <= 0;
    public ItemData itemData;
    public ItemData Data => itemData;

    public void Mine(int power)
    {
        if (IsDepleted)
            return;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            Managers.UI.inven.AddItem(itemData);
            Destroy(gameObject);
        }
    }

    public void MineDigger(int power, IMinable minable)
    {
        if (IsDepleted)
            return;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            Managers.UI.digger.Digging(minable.Data);
            durability = 5;
        }
    }

    public bool AutoDigger(int power)
    {
        if (IsDepleted)
            return true;

        durability -= power;
        Debug.Log($"Rock mined, Remaining: {durability}");

        if (IsDepleted)
        {
            durability = 5;
            return true;
        }

        return false;
    }
}
