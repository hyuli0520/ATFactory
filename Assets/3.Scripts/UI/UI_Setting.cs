using UnityEngine;
using UnityEngine.Localization.Settings;

public class UI_Setting : MonoBehaviour
{
    public GameObject bindingKey;
    public GameObject localization;
    public GameObject hdrp;

    /// <summary>
    /// Closes the settings UI and resumes the game.
    /// </summary>
    public void Exit()
    {
        Managers.UI.setting.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Shows the key binding panel
    /// </summary>
    public void ClickBinding()
    {
        bindingKey.SetActive(true);
        localization.SetActive(false);
        hdrp.SetActive(false);
    }
    /// <summary>
    /// Shows the localization panel
    /// </summary>
    public void ClickLocalization()
    {
        bindingKey.SetActive(false);
        localization.SetActive(true);
        hdrp.SetActive(false);
    }
    /// <summary>
    /// Shows the quality settings panel
    /// </summary>
    public void ClickQuality()
    {
        bindingKey.SetActive(false);
        localization.SetActive(false);
        hdrp.SetActive(true);
    }

    /// <summary>
    /// Updates the game localization to the setting ui
    /// 0 is English, 1 is Japanese, 2 is Korean
    /// </summary>
    public void UpdateLocalization(int index)
    {   
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
