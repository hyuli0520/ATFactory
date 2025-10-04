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

    /// <summary>
    /// Executes the action corresponding to the given build mode
    /// </summary>
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

    /// <summary>
    /// Set build mode to place
    /// </summary>
    public void Build()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
        nowMode = BuildingPlacer.BuildMode.PLACE;
    }

    /// <summary>
    /// Set build mode to edit
    /// </summary>
    public void Edit()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.EDIT);
        nowMode = BuildingPlacer.BuildMode.EDIT;
    }

    /// <summary>
    /// Set build mode to delete
    /// </summary>
    public void Delete()
    {
        placer.ChangeBuildMode(BuildingPlacer.BuildMode.DESTROY);
        nowMode = BuildingPlacer.BuildMode.DESTROY;
    }

    /// <summary>
    /// Rotates the currently selected building preview
    /// </summary>
    public void Rotate()
    {
        placer.RotatePreview();
    }
}
