using UnityEngine;

/// <summary>
/// Mineable Object
/// </summary>
public interface IMineable
{
    void Mine(int power);
    bool IsDepleted { get; }
}
