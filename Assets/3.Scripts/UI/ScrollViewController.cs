using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect;

    public string objectName;
    public float space = 10f;
    public List<RectTransform> uiObjects = new List<RectTransform>();

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update()
    {

    }

    public void AddNewUIObject(Sprite img, string txt, Action<RectTransform> onCreated)
    {
        Addressables.InstantiateAsync(objectName, scrollRect.content).Completed += (handle) =>
        {
            var newUI = handle.Result.GetComponent<RectTransform>();
            var newImg = Util.FindChild<Image>(newUI.gameObject, "ItemImage");
            newImg.sprite = img;
            newUI.GetComponentInChildren<TMP_Text>().text = txt;

            uiObjects.Add(newUI);

            float y = 0f;
            for (int i = 0; i < uiObjects.Count; i++)
            {
                uiObjects[i].anchoredPosition = new Vector2(0f, -y);
                y += uiObjects[i].sizeDelta.y + space;
            }

            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.y, y);

            onCreated?.Invoke(newUI);
        };
    }
}
