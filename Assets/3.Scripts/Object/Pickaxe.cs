using UnityEngine;

public class Pickaxe : MonoBehaviour, IMiningTool
{
    public int Power => 1;
    public float MiningTime => 1.2f;

    public void Use(IMinable target)
    {
        target.Mine(Power);
    }
}
