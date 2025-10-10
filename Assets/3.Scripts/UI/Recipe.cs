using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a crafting recipe containing required materials and output item
/// </summary>
[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public ItemData itemData;
    [TextArea]
    public string decs;
    public List<CraftingMaterial> materials = new();
    public int outputCount;
}

/// <summary>
/// Represents a single material requirement for a recipe
/// </summary>
[Serializable]
public class CraftingMaterial
{
    public ItemData material;
    public int materialCount;
}