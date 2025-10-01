using UnityEngine;
using UnityEngine.Localization.Settings;

public class UI_Setting : MonoBehaviour
{
    public GameObject bindingKey;
    public GameObject localization;
    public GameObject hdrp;

    public void Exit()
    {
        Managers.UI.setting.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ClickBinding()
    {
        bindingKey.SetActive(true);
        localization.SetActive(false);
        hdrp.SetActive(false);
    }
    public void ClickLocalization()
    {
        bindingKey.SetActive(false);
        localization.SetActive(true);
        hdrp.SetActive(false);
    }
    public void ClickQuality()
    {
        bindingKey.SetActive(false);
        localization.SetActive(false);
        hdrp.SetActive(true);
    }

    public void UpdateLocalization(int index)
    {   
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
