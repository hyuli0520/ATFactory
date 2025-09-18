using UnityEngine;

public class Pickaxe : MonoBehaviour, IMiningTool
{
    public int Power => 1;
    public float MiningTime => 1.2f;

    public void Use(IMineable target)
    {
        target.Mine(Power);
    }
}
