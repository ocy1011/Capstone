using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WriteReviewPanel : MonoBehaviour {

    public Transform starPanel;
    public Content content;
    public ProductUnit productUnit;
    GameObject exPanel;
    Product product;
    int score;
    char splitChar = ';';
    bool sizeChanged = false;

    private void Start()
    {
        //AdjustPanelsSize();
    }

    public void PanelOn(GameObject exPanel, Product product)
    {
        this.exPanel = exPanel;
        this.product = product;
        exPanel.SetActive(false);
        ChangeContentSize();
        ResetComponents();
        productUnit.InfoChange(product, gameObject);
    }

    public void BackButton()
    {
        exPanel.gameObject.SetActive(true);
        if (exPanel.GetComponent<ReviewPanel>())
            exPanel.GetComponent<ReviewPanel>().ResetReviewAndRead();
        TextPanel[] textPanels = exPanel.transform.GetComponentsInChildren<TextPanel>();
        for (int i = 0; i < textPanels.Length; i++)
            textPanels[i].ChangeTextSize();
        gameObject.SetActive(false);
    }

    public void WriteReview()
    {
        int productID = product.id;
        string email = Panels.instance.memberInfo.member.email;
        int first = content.transform.GetChild(2).GetComponent<ToggleGroupInfo>().ActiveToggle();
        int second = content.transform.GetChild(3).GetComponent<ToggleGroupInfo>().ActiveToggle();
        int third = content.transform.GetChild(4).GetComponent<ToggleGroupInfo>().ActiveToggle();
        int fourth = content.transform.GetChild(5).GetComponent<ToggleGroupInfo>().ActiveToggle();
        int fifth = content.transform.GetChild(6).GetComponent<ToggleGroupInfo>().ActiveToggle();
        string sql = "SELECT productReviews FROM member WHERE email = '" + email + "';";
        int count = DbConnecter.instance.Count("email", "review", "email = '" + email + "' AND productID = " + productID);
        if(count!=0)
        {
            sql = "Delete FROM review WHERE email = '" + email + "' AND productID = " + productID+";";
            DbConnecter.instance.ExecuteSQL(sql, false);
        }
        DateTime dateTime = DateTime.Now;
        string format = "yyyy-MM-dd HH:mm:ss";
        Debug.Log(dateTime.ToString(format));
        sql = "INSERT INTO review VALUES(" + "'" + email + "', " + productID + ", " + score + ", " + first + ", " + second + ", " + third + ", " + fourth + ", " + fifth + ", '" + dateTime.ToString(format) + "');";
        DbConnecter.instance.ExecuteSQL(sql, true);
        DbConnecter.instance.CloseConnection();
        BackButton();
    }

    void ResetComponents()
    {
        FirstStar();
        for (int i = 2; i < 7; i++)
        {
            ToggleGroupInfo groupInfo = content.transform.GetChild(i).GetComponent<ToggleGroupInfo>();
            groupInfo.SelectToggle(0);
        }
    }

    public void WriteReview(int amount)
    {
        int productID = 1;
        for(int i=0; i<amount; i++)
        {
            string email = "changyeong" + i;
            int score = UnityEngine.Random.Range(1, 6);
            int first = UnityEngine.Random.Range(0, 5);
            int second = UnityEngine.Random.Range(0, 6);
            int third = UnityEngine.Random.Range(0, 3);
            int fourth = UnityEngine.Random.Range(0, 6);
            int fifth = UnityEngine.Random.Range(0, 5);
            string sql = "SELECT productReviews FROM member WHERE email = '"+ email + "';";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            reader.Read();
            string productReviews = "";
            if(reader.IsDBNull(0)==false)
                productReviews = reader.GetString(0);
            reader.Close();
            productReviews = productReviews + productID + ';';

            sql = "INSERT INTO review VALUES(" + "'" + email + "', " + productID + ", " + score + ", " + first + ", " + second + ", " + third + ", " + fourth + ", " + fifth + ");";
            DbConnecter.instance.ExecuteSQL(sql, false);
            sql = "UPDATE member SET productReviews = '" + productReviews + "' WHERE email = '" + email + "';";
            DbConnecter.instance.ExecuteSQL(sql, false);
            sql = "SELECT COUNT(*), AVG(CAST(score as FLOAT)) FROM review GROUP BY productID HAVING productID = " + productID + ";";
            reader = DbConnecter.instance.Reader(sql);
            reader.Read();
            int reviewer = reader.GetInt32(0);
            float grade = (float)reader.GetDouble(1);
            reader.Close();
            sql = "UPDATE product SET grade = " + grade + ", reviewer = " + reviewer + " WHERE id = " + productID + ";";
            DbConnecter.instance.ExecuteSQL(sql, true);
            DbConnecter.instance.CloseConnection();
            Debug.Log(i);
        }
  
    }

    void ChangeContentSize()
    {
        if(sizeChanged==false)
        {
            int childCount = content.transform.childCount;
            RectTransform lastPanel = content.transform.GetChild(childCount - 1).GetComponent<RectTransform>();
            float nextYMax = lastPanel.anchorMax.y;
            int lastChild = lastPanel.transform.childCount;
            RectTransform lastChildRect = lastPanel.transform.GetChild(lastChild - 1).GetComponent<RectTransform>();
            float minusY = 1 - lastChildRect.anchorMin.y;
            nextYMax -= minusY;
            float contentSize = content.getInitialContentYSize() * (1 - (nextYMax));
            content.ChangeSize(contentSize);
            sizeChanged = true;
        }
    }

    void AdjustPanelsSize()
    {
        int scorePanelOrder = 1;
        int contentChildCount = content.transform.childCount;
        float nextYMax = content.transform.GetChild(scorePanelOrder).GetComponent<RectTransform>().anchorMin.y;
        float gap = 0.02f;
        float componentHeight = 0.05f;
        for (int i = scorePanelOrder + 1; i < contentChildCount; i++)
        {
            nextYMax -= gap;
            Transform nextPanel = content.transform.GetChild(i);
            nextPanel.gameObject.SetActive(true);
            RectTransform rectTransform = nextPanel.GetComponent<RectTransform>();
            rectTransform.anchorMax = new Vector2(1, nextYMax);
            rectTransform.anchorMin = new Vector2(0, nextYMax - 1f);
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
            int childCount = nextPanel.childCount;
            float yMax = 1f;
            float stack = 0;
            for (int j = 0; j < childCount; j++)
            {
                float height = componentHeight;
                if (j == 0)
                    height += 0.01f;
                stack += height;
                float yMin = yMax - height;
                RectTransform childRect = nextPanel.GetChild(j).GetComponent<RectTransform>();
                childRect.anchorMax = new Vector2(1, yMax);
                childRect.anchorMin = new Vector2(0, yMin);
                childRect.offsetMax = Vector2.zero;
                childRect.offsetMin = Vector2.zero;
                yMax -= height;
            }
            nextYMax -= stack;
        }
    }

    public void FirstStar()
    {
        score = 1;
        ChangeStars();
    }

    public void SecondStar()
    {
        score = 2;
        ChangeStars();
    }

    public void ThirdStar()
    {
        score = 3;
        ChangeStars();
    }

    public void FourthStar()
    {
        score = 4;
        ChangeStars();
    }

    public void FifthStar()
    {
        score = 5;
        ChangeStars();
    }

    void ChangeStars()
    {
        for (int i = 0; i < 5; i++)
        {
            float fillAmount = Mathf.Clamp(score - i, 0, 1);
            Image yellowStar = starPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            yellowStar.fillAmount = fillAmount;
        }
    }
}
