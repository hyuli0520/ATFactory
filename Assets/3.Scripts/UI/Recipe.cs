using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public ItemData itemData;
    [TextArea]
    public string decs;
    public List<CraftingMaterial> materials = new();
    public int outputCount;
}

[Serializable]
public class CraftingMaterial
{
    public ItemData material;
    public int materialCount;
}