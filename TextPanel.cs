using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextPanel : MonoBehaviour {
    public string text;
    public Text mainText;
    public Text testText;
    RectTransform content;
    float panelXSize;
    bool changed = false;
    bool move = false;

    public void Change(string _text)
    {
        move = false;
        text = _text;
        StartCoroutine(ChangeText(_text));
    }

    public void Move()
    {
        move = true;
        StartCoroutine(MoveText());
    }

    public void ChangeTextSize()
    {
        int textSize = testText.cachedTextGenerator.fontSizeUsedForBestFit;
        testText.gameObject.SetActive(false);
        mainText.fontSize = textSize;

        content = transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform textRectTransform = mainText.GetComponent<RectTransform>();
        RectTransform panelRectTransform = GetComponent<RectTransform>();
        float left = textRectTransform.offsetMin.x;
        float right = textRectTransform.offsetMax.x;
        panelXSize = mainText.preferredWidth + left - right;
        if (panelRectTransform.rect.size.x < panelXSize)
        {
            Move();

        }
        else
        {
            panelXSize = panelRectTransform.rect.size.x;
            move = false;
            StopCoroutine(MoveText());
        }
        content.sizeDelta = new Vector2(panelXSize, 0);
        content.anchoredPosition = new Vector2(panelXSize / 2, 0);
    }

    IEnumerator ChangeText(string _text)
    {
        yield return null;
        mainText.text = _text;
        ChangeTextSize(); 
    }

    IEnumerator MoveText()
    {
        bool check = false;
        while(move==true)
        {
            if(check==false)
            {
                content.anchoredPosition = new Vector2(panelXSize / 2, 0);
                yield return new WaitForSeconds(2.0f);
                check = true;
            }
            else
            {
                yield return null;
                Vector2 current = content.anchoredPosition;
                Vector2 target = new Vector2(-panelXSize / 2, 0);
                float speed = 60f;
                content.anchoredPosition = Vector2.MoveTowards(current, target, Time.deltaTime * speed);
                if (content.anchoredPosition == target)
                    check = false;
            }
        }
    }
}
