using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanBody : MonoBehaviour {

    public RectTransform body;
    public RectTransform eye;
    public RectTransform respiratory;
    public RectTransform digestive;
    public RectTransform reproductive;
    public RectTransform integumentary;

    // Use this for initialization
    void Start () {
        ChangeBodySize();
	}
	
	void ChangeBodySize()
    {
        Vector2 panelSize = transform.GetComponent<RectTransform>().rect.size;
        float maxRate = Mathf.Ceil(panelSize.y / 5);
        float rate = maxRate * 0.9f;
        float width = 2 * rate;
        float height = 5 * rate;
        Vector2 size = new Vector2(width, height);
        body.sizeDelta = size;
        body.anchoredPosition = new Vector2(width /2 + width/32, 0);
        PartMove(size);
    }

    void PartMove(Vector2 bodySize)
    {
        float circleSize = bodySize.x / 5;
        float ySize = bodySize.y / 2;
        float xSize = bodySize.x / 2;
        ChangeSize(eye, circleSize, new Vector2(0, ySize*7/8f));
        ChangeSize(respiratory, circleSize, new Vector2(0, ySize * 5f / 8f));
        ChangeSize(digestive, circleSize, new Vector2(0, ySize * 2 / 8f));
        ChangeSize(reproductive, circleSize, new Vector2(0, 0));
        ChangeSize(integumentary, circleSize, new Vector2(xSize *5/8f, 0));

    }

    void ChangeSize(RectTransform rectTransform, float size, Vector2 position)
    {
        rectTransform.sizeDelta = Vector2.one * size;
        rectTransform.anchoredPosition = position;
    }
}
