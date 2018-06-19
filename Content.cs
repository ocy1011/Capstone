using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour {


    public float getInitialContentYSize()
    {
        float initialContentYSize = transform.parent.GetComponent<RectTransform>().rect.size.y;
        return initialContentYSize;
    }

    public void ChangeSize(float contentSize)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, contentSize);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -contentSize/2);
        ChangeChildrenSize(transform, getInitialContentYSize());
    }

    void ChangeChildrenSize(Transform parentTransform, float parentYSize)
    {
        int childCount = parentTransform.childCount;
        for(int i=0; i<childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            if(child.GetComponent<SavedRectTransform>()==null)
            {
                child.gameObject.AddComponent<SavedRectTransform>();
                child.GetComponent<SavedRectTransform>().GetInfomation();
            }
            RectTransform childRectTransform = child.GetComponent<RectTransform>();
            SavedRectTransform savedRectTransform = child.GetComponent<SavedRectTransform>();
            Vector2 anchorMin = savedRectTransform.anchorMin;
            Vector2 anchorMax = savedRectTransform.anchorMax;
            Vector2 offsetMin = savedRectTransform.offsetMin;
            Vector2 offsetMax = savedRectTransform.offsetMax;
            float xSize = 0;
            float anchoredPositionX = 0;
            if (offsetMax.x != 0)
            {
                xSize = offsetMax.x;
                anchoredPositionX = savedRectTransform.anchoredPosition.x;
            }
            float ySize = parentYSize * (anchorMax.y- anchorMin.y);
            childRectTransform.anchorMin = new Vector2(anchorMin.x, 1);
            childRectTransform.anchorMax = new Vector2(anchorMax.x, 1);
            childRectTransform.sizeDelta = new Vector2(xSize, Mathf.Ceil(ySize));
            childRectTransform.anchoredPosition = new Vector2(anchoredPositionX, -Mathf.Ceil(ySize / 2 + (1 - anchorMax.y) * parentYSize));

            /*
            if (child.tag != "MovingText" && child.tag != "NoChangeChild")
               ChangeChildrenSize(child, ySize);
            */    
    }
    }
}
