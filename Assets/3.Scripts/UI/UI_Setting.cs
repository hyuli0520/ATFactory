using UnityEngine;
using UnityEngine.Localization.Settings;

public class UI_Setting : MonoBehaviour
{
    public GameObject bindingKey;
    public GameObject localization;

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
    }
    public void ClickLocalization()
    {
        bindingKey.SetActive(false);
        localization.SetActive(true);
    }

    public void ClickEnglish()
    {
        UpdateLocalization(0);
    }
    public void ClickKorea()
    {
        UpdateLocalization(2);
    }
    public void ClickJapanese()
    {
        UpdateLocalization(1);
    }
    public void UpdateLocalization(int index)
    {   
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
