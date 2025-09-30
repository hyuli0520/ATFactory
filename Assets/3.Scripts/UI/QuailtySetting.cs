using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class QuailtySetting : MonoBehaviour
{
    [SerializeField]
    List<RenderPipelineAsset> renderPipelineAssets;
    [SerializeField]
    TMP_Dropdown dropdown;

    public void SetPipeline(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = renderPipelineAssets[value];
    }
}
