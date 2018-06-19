using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryPanel : MonoBehaviour {
    RectTransform content;
    Vector2 panelSize;
    public Transform unit;
    int onePageAmount = 6;
    int nowAmount = 0;

    public List<ProductUnit> AddUnits(int amount)
    {
        List<ProductUnit> units = new List<ProductUnit>();
        float ySize = panelSize.y / onePageAmount;
        float contentYSize = panelSize.y;
        int totalAmount = nowAmount + amount;
        if (totalAmount > onePageAmount)
            contentYSize = ySize * totalAmount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.localPosition = new Vector2(0, -contentYSize / 2);
        for (int i = nowAmount; i < totalAmount; i++)
        {
            Transform newUnit = Instantiate(unit);
            newUnit.SetParent(content);
            ProductUnit unitInfo = newUnit.GetComponent<ProductUnit>();
            units.Add(unitInfo);
            RectTransform rectTransform = newUnit.GetComponent<RectTransform>();

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);

            
        }
        nowAmount += amount;
        return units;
    }

    /*
     syntheticDetergent = 0, //합성세제
    bleach = 1, //표백제
    softner = 2, //섬유유연제
    freshener = 3, //방향제
    deodorant = 4, //탈취제
    detergent = 5 //세정제
     */

    public void PanelOn(Category category)
    {
        
        Transform body = transform.GetChild(1);
        content = body.GetChild(0).GetComponent<RectTransform>();
        panelSize = body.GetComponent<RectTransform>().rect.size;
        string categoryKorean = "";
        if(category==Category.syntheticDetergent)
            categoryKorean = "합성세제";
        else if (category == Category.bleach)
            categoryKorean = "표백제";
        else if (category == Category.softner)
            categoryKorean = "섬유유연제";
        else if (category == Category.freshener)
            categoryKorean = "방향제";
        else if (category == Category.deodorant)
            categoryKorean = "탈취제";
        else if (category == Category.detergent)
            categoryKorean = "세정제"; 

        string sql = "SELECT * FROM product WHERE category = '" + categoryKorean + "';";
        int idCount = DbConnecter.instance.Count("id", "product","category = '"+categoryKorean+"'");
        List<ProductUnit> units = AddUnits(idCount);
        MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
        int number = 0;
        while (reader.Read())
        {
            Product product = new Product();
            product.id = reader.GetInt32(0);
            product.name = reader.GetString(1);
            product.barcode = reader.GetString(2);
            product.company = reader.GetString(3);
            product.ingredients = reader.GetString(5);
            product.imagePath = reader.GetString(6);
            product.category = category;
            units[number].InfoChange(product, gameObject);
            number++;
        }
        reader.Close();
        DbConnecter.instance.CloseConnection();
    }

    public void BackButton()
    {
        Panels.instance.homePanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
        nowAmount = 0;
        int childCount = content.transform.childCount;
        for(int i=0; i<childCount; i++)
            Destroy(content.transform.GetChild(childCount-1-i).gameObject);
    }

}
