using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    UIManager _ui = new();

    public static UIManager UI { get { return Instance._ui; } }

    public static void Init()
    {
        if (instance == null)
        {
            GameObject go = null;

            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

            Instance._ui.Init();
        }
    }
}
