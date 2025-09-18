using UnityEngine;

/// <summary>
/// Mineable Object
/// </summary>
public interface IMinable
{
    void Mine(int power);
    bool IsDepleted { get; }
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