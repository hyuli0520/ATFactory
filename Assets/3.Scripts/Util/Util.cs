using Unity.Mathematics;
using UnityEngine;

public static class Util
{
    public static T FindChild<T>(GameObject go, string name = null) where T : Object
    {
        if (go == null)
            return null;

        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform transform = go.transform.GetChild(i);
            if (string.IsNullOrEmpty(name) || transform.name == name)
            {
                T component = transform.GetComponent<T>();
                if (component != null)
                    return component;
            }
        }

        return null;
    }

    /// <summary>
    /// Calculate two-dimensional bezier curve 
    /// </summary>
    public static float3 Bezier(float3 start, float3 control, float3 end, float t)
    {
        float3 u = 1 - t;
        return u * u * start + 2 * u * t * control + t * t * end;
    }
}
