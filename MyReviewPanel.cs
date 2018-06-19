using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyReviewPanel : MonoBehaviour {

    public Transform myReviewUnit;
    public Transform body;
    int onePageAmount = 2;
    GameObject exPanel;
    List<MyReviewUnit> myReviewUnits = new List<MyReviewUnit>();

    public void BackButton()
    {
        exPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PanelOn(GameObject exPanel)
    {
        this.exPanel = exPanel;
        exPanel.SetActive(false);
        ResetReviewAndRead();
    }

    public void ResetReviewAndRead()
    {
        int reviewUnitsCount = myReviewUnits.Count;
        if (reviewUnitsCount > 0)
        {
            for (int i = 0; i < reviewUnitsCount; i++)
                Destroy(myReviewUnits[reviewUnitsCount - 1 - i].gameObject);
        }
        myReviewUnits = new List<MyReviewUnit>();
        ReadAll();
    }

    void ReadAll()
    {
        string email = Panels.instance.memberInfo.member.email;
        string sql = "SELECT * FROM review WHERE email = '"+email+"' ORDER BY dateTime DESC;";
        MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
        List<Review> reviews = new List<Review>();
        List<Product> products = new List<Product>();
        while (reader.Read())
        {
            Review newReview = new Review();
            newReview.email = reader.GetString(0);
            newReview.productID = reader.GetInt32(1);
            newReview.score = reader.GetInt32(2);
            newReview.first = reader.GetInt32(3);
            newReview.second = reader.GetInt32(4);
            newReview.third = reader.GetInt32(5);
            newReview.fourth = reader.GetInt32(6);
            newReview.fifth = reader.GetInt32(7);
            reviews.Add(newReview);
        }
        reader.Close();
        for(int i=0; i<reviews.Count; i++)
        {
            int productID = reviews[i].productID;
            sql = "SELECT * FROM product WHERE id = " + productID + ";";
            reader = DbConnecter.instance.Reader(sql);
            reader.Read();
            Product newProduct = new Product();
            newProduct.id = reader.GetInt32(0);
            newProduct.name = reader.GetString(1);
            newProduct.barcode = reader.GetString(2);
            newProduct.company = reader.GetString(3);
            newProduct.category = newProduct.koreanToCategory(reader.GetString(4));
            newProduct.ingredients = reader.GetString(5);
            newProduct.imagePath = reader.GetString(6);
            products.Add(newProduct);
            reader.Close();
        }
        DbConnecter.instance.CloseConnection();
        AddMyReview(reviews, products);
    }

    void AddMyReview(List<Review> reviews, List<Product> products)
    {
        int amount = reviews.Count;
        RectTransform content = body.GetChild(0).GetComponent<RectTransform>();
        for (int i = 0; i < amount; i++)
        {
            Transform newMyReviewUnit = Instantiate(myReviewUnit);
            newMyReviewUnit.SetParent(content);
            MyReviewUnit myReviewUnitInfo = newMyReviewUnit.GetComponent<MyReviewUnit>();
            myReviewUnits.Add(myReviewUnitInfo);
        }
        SortReviewPanels(0);
        for(int i=0; i<amount; i++)
        {
            Transform unit = myReviewUnits[i].transform;
            Product product = products[i];
            Review review = reviews[i];
            ProductUnit productUnit = unit.GetChild(0).GetComponent<ProductUnit>();
            productUnit.InfoChange(product, gameObject);
            ReviewUnit reviewUnit = unit.GetChild(1).GetComponent<ReviewUnit>();
            reviewUnit.Change(review, gameObject);
        }
    }

    public void DeleteReivew(ReviewUnit reviewUnit)
    {
        int number = myReviewUnits.FindIndex(a => a.transform.GetChild(1).GetComponent<ReviewUnit>() == reviewUnit);
        GameObject temp = myReviewUnits[number].gameObject;
        myReviewUnits.RemoveAt(number);
        Destroy(temp);
        SortReviewPanels(number);
    }

    void SortReviewPanels(int number)
    {
        int amount = myReviewUnits.Count;
        Vector2 panelSize = body.GetComponent<RectTransform>().rect.size;
        RectTransform content = body.GetChild(0).GetComponent<RectTransform>();
        float ySize = panelSize.y / onePageAmount;
        float contentYSize = panelSize.y;
        if (amount > onePageAmount)
            contentYSize = ySize * amount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.localPosition = new Vector2(0, -contentYSize / 2 + ySize * number);
        for (int i = 0; i < amount; i++)
        {
            MyReviewUnit unit = myReviewUnits[i];
            RectTransform rectTransform = unit.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);
        }
    }
}
