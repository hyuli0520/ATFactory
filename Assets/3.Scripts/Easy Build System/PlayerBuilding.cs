using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    BuildingPlacer placer;

    void Start()
    {
        placer = BuildingPlacer.Instance;
    }

    public void Build()
    {
        placer.PlacingBuildingPart();
    }
}
