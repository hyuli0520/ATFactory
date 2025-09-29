using UnityEngine;

public class UI_Setting : MonoBehaviour
{
    public void Exit()
    {
        Managers.UI.setting.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
