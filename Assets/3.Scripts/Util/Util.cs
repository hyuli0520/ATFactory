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
}
