using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedRectTransform : MonoBehaviour {

    public Vector2 offsetMin;
    public Vector2 offsetMax;
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 anchoredPosition;

    public void GetInfomation()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        offsetMin = rectTransform.offsetMin;
        offsetMax = rectTransform.offsetMax;
        anchorMin = rectTransform.anchorMin;
        anchorMax = rectTransform.anchorMax;
        anchoredPosition = rectTransform.anchoredPosition;
    }
}
