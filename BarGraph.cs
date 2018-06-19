using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarGraph : MonoBehaviour {

    Vector2 targetSize;
    Vector2 targetPos;

    public void Move(Vector2 targetSize, Vector2 targetPos)
    {
        this.targetSize = targetSize;
        this.targetPos = targetPos;
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        float speed = 1 / 60f;
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        float posDelta = (targetPos.y - rectTransform.anchoredPosition.y) * speed;
        float sizeDelta = (targetSize.y - rectTransform.rect.size.y) * speed;

        while ((Vector2)transform.position != targetPos)
        {
            yield return null;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPos, posDelta);
            Vector2 nowSize = rectTransform.rect.size;
            Vector2 size = Vector2.MoveTowards(nowSize, targetSize, sizeDelta);
            rectTransform.sizeDelta = size;
        }
    }
}
