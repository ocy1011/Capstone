using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPanel : MonoBehaviour {

    GameObject exPanel;
    Product product;
    List<ReviewUnit> reviewUnits = new List<ReviewUnit>();
    int oneLoadAmount = 10;
    public Transform reviewUnit;
    public Content content;
    public ProductUnit productUnit;
    float anchorGap = 0.3f;

    public void PanelOn(GameObject exPanel, Product product)
    {
        this.exPanel = exPanel;
        this.product = product;
        exPanel.gameObject.SetActive(false);

        productUnit.InfoChange(product, gameObject);
        ResetReviewAndRead();
    }

    public void ResetReviewAndRead()
    {
        int reviewUnitsCount = reviewUnits.Count;
        if (reviewUnitsCount > 0)
        {
            for (int i = 0; i < reviewUnitsCount; i++)
                Destroy(reviewUnits[reviewUnitsCount - 1 - i].gameObject);
        }
        reviewUnits = new List<ReviewUnit>();
        ReadReviews();
    }

    public void BackButton()
    {
        exPanel.gameObject.SetActive(true);
        if (exPanel.GetComponent<ProductInfoPanel>())
            exPanel.GetComponent<ProductInfoPanel>().ChangeReviewSummary();
        TextPanel[] textPanels = exPanel.transform.GetComponentsInChildren<TextPanel>();
        for (int i = 0; i < textPanels.Length; i++)
            textPanels[i].ChangeTextSize();
        gameObject.SetActive(false);
    }

    public void MoveToWriteReviewPanel()
    {
        Panels.instance.writeReviewPanel.gameObject.SetActive(true);
        Panels.instance.writeReviewPanel.PanelOn(gameObject, product);
        gameObject.SetActive(false);
    }

    public void ReadReviews()
    {
        int productID = product.id;
        int count = DbConnecter.instance.Count("productID", "review","productID = "+productID+";");
        float anchorGap = 0.3f;
        float firstYMax = 0.75f;
        float lastYMax = 0;
        int maxRange = Mathf.Clamp(count - reviewUnits.Count, 0, count);
        int minRange = Mathf.Clamp(maxRange - oneLoadAmount, 0, maxRange);
        if(maxRange>0)
        {
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY(dateTime) DESC) AS id, email, productID, score, first, second, third, fourth, fifth FROM review WHERE productID = "+productID+ ") newtable WHERE id <=" + maxRange + " AND id >" + minRange + "; ";
            /*
            string sql = @"SELECT *
                       FROM (
                                SELECT ROW_NUMBER() OVER(ORDER BY(dateTime) DESC) AS id, * 
                                FROM review
                                WHERE productID = " + productID +
                          @") newtable
                        WHERE id <=" + maxRange + " AND id >" + minRange + "; ";
            */
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            while (reader.Read())
            {
                Transform newReviewUnit = Instantiate(reviewUnit);
                newReviewUnit.SetParent(content.transform);
                RectTransform rectTransform = newReviewUnit.GetComponent<RectTransform>();
                int reviewCount = reviewUnits.Count;
                float yMax = firstYMax - anchorGap * reviewCount;
                float yMin = yMax - anchorGap;
                lastYMax = yMin;
                rectTransform.anchorMax = new Vector2(1, yMax);
                rectTransform.anchorMin = new Vector2(0, yMin);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;


                Review review = new Review();
                review.id = (int)reader.GetInt64(0);
                review.email = reader.GetString(1);
                review.productID = reader.GetInt32(2);
                review.score = reader.GetInt32(3);
                review.first = reader.GetInt32(4);
                review.second = reader.GetInt32(5);
                review.third = reader.GetInt32(6);
                review.fourth = reader.GetInt32(7);
                review.fifth = reader.GetInt32(8);
                

                ReviewUnit reviewUnitInfo = newReviewUnit.GetComponent<ReviewUnit>();
                reviewUnitInfo.Change(review, gameObject);
                reviewUnits.Add(reviewUnitInfo);
            }
            reader.Close();
            DbConnecter.instance.CloseConnection();
        }
        else
        {
            Debug.Log("불러올 리뷰가 없습니다");
        }
        if (lastYMax < 0)
        {
            float contentSize = content.getInitialContentYSize() * (1 - (lastYMax));
            content.ChangeSize(contentSize);
        }
        else
        {
            float contentSize = content.getInitialContentYSize();
            content.ChangeSize(contentSize);
        }
    }





}
