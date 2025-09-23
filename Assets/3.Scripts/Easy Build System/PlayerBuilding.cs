using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using UnityEngine;

public class PlayerBuilding : MonoBehaviour
{
    BuildingPlacer placer;

    public BuildingPlacer.BuildMode nowMode;

    void Start()
    {
        placer = BuildingPlacer.Instance;
        nowMode = BuildingPlacer.BuildMode.NONE;
    }

    public void Validate(BuildingPlacer.BuildMode mode)
    {
        switch (mode)
        {
            case BuildingPlacer.BuildMode.PLACE:
                placer.PlacingBuildingPart();
                break;
            case BuildingPlacer.BuildMode.EDIT:
                placer.EditingBuildingPart();
                nowMode = BuildingPlacer.BuildMode.PLACE;
                break;
            case BuildingPlacer.BuildMode.DESTROY:
                placer.DestroyBuildingPart();
                break;
        }
    }

    public void Build()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
        nowMode = BuildingPlacer.BuildMode.PLACE;
    }

    public void Edit()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.EDIT);
        nowMode = BuildingPlacer.BuildMode.EDIT;
    }

    public void Delete()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.DESTROY);
        nowMode = BuildingPlacer.BuildMode.DESTROY;
    }

    public void Rotate()
    {
        placer.RotatePreview();
    }
}
