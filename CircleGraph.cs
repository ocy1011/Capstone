using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGraph : MonoBehaviour {

    public Transform backGround;
    public Transform partImage;
    public Transform textPanel;
    public Transform bar;

    public void Making(List<Ingredient> ingredients)
    {
        int backgroundCount = backGround.childCount;
        if(backgroundCount>0)
        {
            for (int i = 0; i < backgroundCount; i++)
                Destroy(backGround.GetChild(backgroundCount - i - 1).gameObject);
        }

        List<int> grades = new List<int>();
        for (int i = 0; i < 11; i++)
            grades.Add(0);
        for(int i=0; i<ingredients.Count; i++)
        {
            string ewgGrade = ingredients[i].ewgGrade;
            int number = 0;
            if (ewgGrade == "1")
                number = 1;
            else if (ewgGrade == "2")
                number = 2;
            else if (ewgGrade == "3")
                number = 3;
            else if (ewgGrade == "3-4")
                number = 4;
            else if (ewgGrade == "3-5")
                number = 5;
            else if (ewgGrade == "4")
                number = 6;
            else if (ewgGrade == "5")
                number = 7;
            else if (ewgGrade == "6")
                number = 8;
            else if (ewgGrade == "6-7")
                number = 9;
            else if (ewgGrade == "7")
                number = 10;
            grades[number] += 1;
        }
        int maxValue = 0;
        for(int i=0; i<grades.Count; i++)
        {
            if (grades[i] > maxValue)
                maxValue = grades[i];
        }
        Sprite[] sprites = Resources.LoadAll<Sprite>("EWG_Image");
        int gradeCount = grades.Count;
        Vector2 backgroundSize = backGround.GetComponent<RectTransform>().rect.size;
        float size = backgroundSize.x / gradeCount;
        float minYPos = size / 2;
        float maxYPos = backgroundSize.y - size / 2;
        float spriteStartYPos = minYPos + size;
        float heightRate = (maxYPos - spriteStartYPos) / maxValue;
        for (int i = 0; i < gradeCount; i++)
        {
            if(grades[i]>0)
            {
                Transform newBar = Instantiate(bar);
                newBar.SetParent(backGround);
                newBar.gameObject.SetActive(true);

                RectTransform rectTransform = newBar.GetComponent<RectTransform>();
                Vector2 startSize = new Vector2(size * 2 / 5, heightRate);
                startSize = new Vector2(size * 2 / 5, 0);
                Vector2 targetSize = new Vector2(size * 2 / 5, heightRate * grades[i]);
                Vector2 startPos = new Vector2(size / 2 + size * i, spriteStartYPos - size/2);
                Vector2 targetPos = new Vector2(size / 2 + size * i, startPos.y + targetSize.y / 2);
                rectTransform.sizeDelta = startSize;
                rectTransform.anchoredPosition = startPos;

                newBar.GetComponent<BarGraph>().Move(targetSize, targetPos);

                Image image = newBar.GetComponent<Image>();
                Color color = Color.black;
                if (i > 0 && i <= 2)
                    color = new Color(57, 181, 74, 255) / 255f;
                else if (i > 2 && i <= 8)
                    color = new Color(252,176,64,255) / 255f;
                else if(i > 8)
                    color = new Color(237,28,36, 255) / 255f;
                image.color = color;
            }
        }

        for (int i=0; i< gradeCount; i++)
        {
            Transform newPart = Instantiate(partImage);
            newPart.SetParent(backGround);
            newPart.gameObject.SetActive(true);

            RectTransform rectTransform = newPart.GetComponent<RectTransform>();
            rectTransform.sizeDelta = Vector2.one * size * ((float)4 / 5);
            rectTransform.anchoredPosition = new Vector2(size / 2 + size * i, minYPos);

            Image image = newPart.GetComponent<Image>();
            image.sprite = sprites[i];

            

            
        }
        for(int i=0; i<gradeCount; i++)
        {
            Transform newText = Instantiate(textPanel);
            newText.SetParent(backGround);
            newText.gameObject.SetActive(true);

            RectTransform rectTransform = newText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = Vector2.one * size * ((float)4 / 5);
            rectTransform.anchoredPosition = new Vector2(size / 2 + size * i, spriteStartYPos);
            Vector2 targetPos = new Vector2(size / 2 + size * i, spriteStartYPos + heightRate * grades[i]);

            Text text = newText.GetChild(0).GetComponent<Text>();
            text.text = "" + grades[i];

            newText.GetComponent<PartImage>().Move(targetPos);
        }  
    }
}
