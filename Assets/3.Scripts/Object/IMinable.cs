using UnityEngine;

/// <summary>
/// Mineable Object
/// </summary>
public interface IMinable
{
    void Mine(int power);
    void MineDigger(int power, IMinable minable);
    bool AutoDigger(int power);
    public void MakeMinedItem();
    bool IsDepleted { get; }
    ItemData Data { get; }
}

/// <summary>
/// Minning Tool
/// </summary>
public interface IMiningTool
{
    int Power { get; }
    float MiningTime { get; }
    void Use(IMinable target);
}