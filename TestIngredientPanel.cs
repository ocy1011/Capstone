using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TestIngredientPanel : MonoBehaviour {

    GameObject exPanel;
    Ingredient ingredient;
    public IngredientInfo ingredientInfo;
    public HumanBody humanBody;
    public Transform symtomPanel;
    public Transform symtomUnit;
    bool isBookmark = false;
    char splitChar = ';';
    float symtomYSize = 0.1f;

    public void PanelOn(Ingredient ingredient, GameObject exPanel)
    {
        this.ingredient = ingredient;
        this.exPanel = exPanel;
        exPanel.gameObject.SetActive(false);
        ingredientInfo.IngredientChange(ingredient, gameObject);
        Eye();
    }

    public void BackButton()
    {
        exPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
        if (exPanel.GetComponent<BookmarkPanel>())
            exPanel.GetComponent<BookmarkPanel>().ResetBookmarks(false, true);

    }


    string IntListToString(List<int> IntList)
    {
        string str = "";
        for (int i = 0; i < IntList.Count; i++)
            str += IntList[i].ToString() + splitChar;
        return str;
    }

    List<int> StringToIntList(string str)
    {
        List<int> lists = new List<int>();
        int startPoint = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == splitChar)
            {
                string temp = "";
                for (int j = startPoint; j < i; j++)
                {
                    temp += str[j];
                }
                int number = Convert.ToInt32(temp);
                lists.Add(number);
                startPoint = i + 1;
            }
        }
        return lists;
    }

    public void Eye()
    {
        symtomPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "눈";
        Transform body = symtomPanel.GetChild(1);
        int bodyChildCount = body.childCount;
        if (bodyChildCount > 0)
        {
            for (int i = 0; i < bodyChildCount; i++)
                Destroy(body.GetChild(bodyChildCount - i - 1).gameObject);
        }
        List<string> list = StringToStrList(ingredient.eye);
        for (int i = 0; i < list.Count; i++)
        {
            Transform newUnit = Instantiate(symtomUnit);
            newUnit.SetParent(body);

            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1 - symtomYSize - symtomYSize * i);
            rectTransform.anchorMax = new Vector2(1, 1 - symtomYSize * i);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Text text = newUnit.GetChild(0).GetComponent<Text>();
            text.text = "ㆍ" + list[i];
        }
    }

    public void Respiratory()
    {
        symtomPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "호흡기관";
        Transform body = symtomPanel.GetChild(1);
        int bodyChildCount = body.childCount;
        if (bodyChildCount > 0)
        {
            for (int i = 0; i < bodyChildCount; i++)
                Destroy(body.GetChild(bodyChildCount - i - 1).gameObject);
        }
        List<string> list = StringToStrList(ingredient.respiratory);
        for (int i = 0; i < list.Count; i++)
        {
            Transform newUnit = Instantiate(symtomUnit);
            newUnit.SetParent(body);

            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1 - symtomYSize - symtomYSize * i);
            rectTransform.anchorMax = new Vector2(1, 1 - symtomYSize * i);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Text text = newUnit.GetChild(0).GetComponent<Text>();
            text.text = "ㆍ" + list[i];
        }
    }

    public void Digestive()
    {
        symtomPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "소화기관";
        Transform body = symtomPanel.GetChild(1);
        int bodyChildCount = body.childCount;
        if (bodyChildCount > 0)
        {
            for (int i = 0; i < bodyChildCount; i++)
                Destroy(body.GetChild(bodyChildCount - i - 1).gameObject);
        }
        List<string> list = StringToStrList(ingredient.digestive);
        for (int i = 0; i < list.Count; i++)
        {
            Transform newUnit = Instantiate(symtomUnit);
            newUnit.SetParent(body);

            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1 - symtomYSize - symtomYSize * i);
            rectTransform.anchorMax = new Vector2(1, 1 - symtomYSize * i);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Text text = newUnit.GetChild(0).GetComponent<Text>();
            text.text = "ㆍ" + list[i];
        }
    }

    public void Reproductive()
    {
        symtomPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "생식기관";
        Transform body = symtomPanel.GetChild(1);
        int bodyChildCount = body.childCount;
        if (bodyChildCount > 0)
        {
            for (int i = 0; i < bodyChildCount; i++)
                Destroy(body.GetChild(bodyChildCount - i - 1).gameObject);
        }
        List<string> list = StringToStrList(ingredient.reproductive);
        for (int i = 0; i < list.Count; i++)
        {
            Transform newUnit = Instantiate(symtomUnit);
            newUnit.SetParent(body);

            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1 - symtomYSize - symtomYSize * i);
            rectTransform.anchorMax = new Vector2(1, 1 - symtomYSize * i);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Text text = newUnit.GetChild(0).GetComponent<Text>();
            text.text = "ㆍ" + list[i];
        }
    }

    public void Integumentary()
    {
        symtomPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "피부기관";
        Transform body = symtomPanel.GetChild(1);
        int bodyChildCount = body.childCount;
        if (bodyChildCount > 0)
        {
            for (int i = 0; i < bodyChildCount; i++)
                Destroy(body.GetChild(bodyChildCount - i - 1).gameObject);
        }
        List<string> list = StringToStrList(ingredient.integumentary);
        for (int i = 0; i < list.Count; i++)
        {
            Transform newUnit = Instantiate(symtomUnit);
            newUnit.SetParent(body);

            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1 - symtomYSize - symtomYSize * i);
            rectTransform.anchorMax = new Vector2(1, 1 - symtomYSize * i);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Text text = newUnit.GetChild(0).GetComponent<Text>();
            text.text = "ㆍ" + list[i];
        }
    }

    List<string> StringToStrList(string str)
    {
        List<string> lists = new List<string>();
        int startPoint = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == splitChar)
            {
                string temp = "";
                for (int j = startPoint; j < i; j++)
                {
                    temp += str[j];
                }
                lists.Add(temp);
                startPoint = i + 1;
            }
        }
        return lists;
    }
}
