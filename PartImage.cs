using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartImage : MonoBehaviour {

    Vector2 targetPos;

    public void Move(Vector2 targetPos)
    {
        this.targetPos = targetPos;
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        float speed = 1 / 60f;
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        float posDelta = (targetPos.y - rectTransform.anchoredPosition.y) * speed;

        while ((Vector2)transform.position != targetPos)
        {
            yield return null;
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPos, posDelta);
        }
    }
}
