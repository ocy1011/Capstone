using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BookmarkPanel : MonoBehaviour {

    char splitChar = ';';
    public Transform productBookmark;
    public Transform ingredientBookmark;
    public Transform body;
    GameObject exPanel;
    List<ProductBookmark> productBookmarks = new List<ProductBookmark>();
    List<IngredientBookmark> ingredientBookmarks = new List<IngredientBookmark>();
    int productOnePageAmount = 5;
    int ingredientOnePageAmount = 7;

    public void OnProductBookmark()
    {
        Transform productBody = body.GetChild(0);
        Transform ingredientBody = body.GetChild(1);
        productBody.gameObject.SetActive(true);
        ingredientBody.gameObject.SetActive(false);
        Image productBtn = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Image ingredientBtn = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        productBtn.color = new Color(0,150,255,255)/255f;
        ingredientBtn.color = new Color(255, 255, 255, 255) / 255f;

        int productsCount = productBookmarks.Count;
        for (int i = 0; i < productsCount; i++)
            Destroy(productBookmarks[i].gameObject);
        productBookmarks = new List<ProductBookmark>();
        AddProductBookmarks();       
    }

    public void OnIngredientBookmark()
    {
        Transform productBody = body.GetChild(0);
        Transform ingredientBody = body.GetChild(1);
        productBody.gameObject.SetActive(false);
        ingredientBody.gameObject.SetActive(true);
        Image productBtn = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Image ingredientBtn = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        productBtn.color = new Color(255, 255, 255, 255) / 255f;
        ingredientBtn.color = new Color(0, 150, 255, 255) / 255f;

        int ingredientsCount = ingredientBookmarks.Count;
        for (int i = 0; i < ingredientsCount; i++)
            Destroy(ingredientBookmarks[i].gameObject);
        ingredientBookmarks = new List<IngredientBookmark>();
        AddIngredientBookmarks();
    }

    public void BackButton()
    {
        exPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnPanel(GameObject exPanel)
    {
        this.exPanel = exPanel;
        exPanel.gameObject.SetActive(false);
        ResetBookmarks(true, true);
    }

    public void ResetBookmarks(bool productReset, bool ingredientReset)
    {
        if (ingredientReset == true)
        {
            OnIngredientBookmark();
        }
        if (productReset == true)
        {
            OnProductBookmark();
        }
    }

    public void AddProductBookmarks()
    {
        List<int> productBookmarkList = StringToIntList(Panels.instance.memberInfo.member.productBookmarks);
        int amount = productBookmarkList.Count;
        Vector2 panelSize = body.GetComponent<RectTransform>().rect.size;
        RectTransform content = body.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        float ySize = panelSize.y / productOnePageAmount;
        float contentYSize = panelSize.y;
        if (amount > productOnePageAmount)
            contentYSize = ySize * amount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.localPosition = new Vector2(0, -contentYSize / 2);
        for (int i = 0; i < amount; i++)
        {
            Transform newBookmark = Instantiate(productBookmark);
            newBookmark.SetParent(content);
            ProductBookmark bookmarkInfo = newBookmark.GetComponent<ProductBookmark>();
            
            productBookmarks.Add(bookmarkInfo);
            RectTransform rectTransform = newBookmark.GetComponent<RectTransform>();

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);

            int productID = productBookmarkList[i];
            string sql = "SELECT * FROM product WHERE id = " + productID + ";";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            reader.Read();
            Product product = new Product();
            product.id = reader.GetInt32(0);
            product.name = reader.GetString(1);
            product.barcode = reader.GetString(2);
            product.company = reader.GetString(3);
            product.category = product.koreanToCategory(reader.GetString(4));
            product.ingredients = reader.GetString(5);
            product.imagePath = reader.GetString(6);
            ProductUnit productUnit = newBookmark.GetChild(0).GetComponent<ProductUnit>();
            productUnit.InfoChange(product, gameObject);
            reader.Close();
            DbConnecter.instance.CloseConnection();

            bookmarkInfo.productID = product.id;
        }
    }

    public void AddIngredientBookmarks()
    {
        List<int> ingredientBookmarkList = StringToIntList(Panels.instance.memberInfo.member.ingredientBookmarks);
        int amount = ingredientBookmarkList.Count;
        Vector2 panelSize = body.GetComponent<RectTransform>().rect.size;
        RectTransform content = body.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        float ySize = panelSize.y / ingredientOnePageAmount;
        float contentYSize = panelSize.y;
        if (amount > ingredientOnePageAmount)
            contentYSize = ySize * amount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.localPosition = new Vector2(0, -contentYSize / 2);
        for (int i = 0; i < amount; i++)
        {
            Transform newBookmark = Instantiate(ingredientBookmark);
            newBookmark.SetParent(content);
            IngredientBookmark bookmarkInfo = newBookmark.GetComponent<IngredientBookmark>();

            ingredientBookmarks.Add(bookmarkInfo);
            RectTransform rectTransform = newBookmark.GetComponent<RectTransform>();

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);

            int ingredientID = ingredientBookmarkList[i];
            string sql = "SELECT * FROM ingredient WHERE id = " + ingredientID + ";";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            reader.Read();

            Ingredient ingredient = new Ingredient();
            ingredient.id = reader.GetInt32(0);
            ingredient.casNo = reader.GetString(1);
            ingredient.english = reader.GetString(2);
            ingredient.korean = reader.GetString(3);
            ingredient.ewgGrade = reader.GetString(4);
            ingredient.eye = reader.GetString(5);
            ingredient.respiratory = reader.GetString(6);
            ingredient.digestive = reader.GetString(7);
            ingredient.reproductive = reader.GetString(8);
            ingredient.integumentary = reader.GetString(9);
            reader.Close();
            DbConnecter.instance.CloseConnection();
            IngredientInfo ingredientInfo = newBookmark.GetChild(0).GetComponent<IngredientInfo>();
            ingredientInfo.IngredientChange(ingredient, gameObject);
            
            bookmarkInfo.ingredientID = ingredient.id;
        }
    }

    public void DeleteProductBookmark(int productID, ProductBookmark productBookmark)
    {
        float ratePos = 1 - body.GetChild(0).GetComponent<ScrollRect>().verticalNormalizedPosition;
        int amount = productBookmarks.Count;
        DbConnecter.instance.Connect();
        List<int> intList = StringToIntList(Panels.instance.memberInfo.member.productBookmarks);
        int number = Mathf.FloorToInt(ratePos * amount);
        number = Mathf.Clamp(number - 2, 0, number);
        intList.Remove(productID);
        productBookmarks.Remove(productBookmark);
        Destroy(productBookmark.gameObject);
        SortProductBookmark(number);
        string bookmarks = IntListToString(intList);
        UpdateProductBookmark(bookmarks);
        DbConnecter.instance.CloseConnection();
        
    }

    public void DeleteIngredientBookmark(int ingredientID, IngredientBookmark ingredientBookmark)
    {
        float ratePos = 1 - body.GetChild(1).GetComponent<ScrollRect>().verticalNormalizedPosition;
        int amount = ingredientBookmarks.Count;
        DbConnecter.instance.Connect();
        List<int> intList = StringToIntList(Panels.instance.memberInfo.member.ingredientBookmarks);
        int number = Mathf.FloorToInt(ratePos * amount);
        number = Mathf.Clamp(number - 2, 0, number);
        intList.Remove(ingredientID);
        ingredientBookmarks.Remove(ingredientBookmark);
        Destroy(ingredientBookmark.gameObject);
        SortIngredientBookmark(number);
        string bookmarks = IntListToString(intList);
        UpdateIngredientBookmark(bookmarks);
        DbConnecter.instance.CloseConnection();

    }


    void SortProductBookmark(int number)
    {
        int amount = productBookmarks.Count;
        Vector2 panelSize = body.GetComponent<RectTransform>().rect.size;
        RectTransform content = body.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        float ySize = panelSize.y / productOnePageAmount;
        float contentYSize = panelSize.y;
        if (amount > productOnePageAmount)
            contentYSize = ySize * amount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.anchoredPosition = new Vector2(0, -contentYSize / 2 + ySize * number);
        for (int i = 0; i < amount; i++)
        {
            ProductBookmark unit = productBookmarks[i];
            RectTransform rectTransform = unit.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);
        }
    }

    void SortIngredientBookmark(int number)
    {
        int amount = ingredientBookmarks.Count;
        Vector2 panelSize = body.GetComponent<RectTransform>().rect.size;
        RectTransform content = body.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        float ySize = panelSize.y / ingredientOnePageAmount;
        float contentYSize = panelSize.y;
        if (amount > ingredientOnePageAmount)
            contentYSize = ySize * amount;
        content.offsetMin = new Vector2(0, 0);
        content.offsetMax = new Vector2(0, contentYSize);
        content.anchoredPosition = new Vector2(0, -contentYSize / 2 + ySize * number);
        for (int i = 0; i < amount; i++)
        {
            IngredientBookmark unit = ingredientBookmarks[i];
            RectTransform rectTransform = unit.GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = new Vector2(0, ySize);
            rectTransform.localPosition = new Vector2(0, contentYSize / 2 + -ySize * i - ySize / 2);
        }
    }

    void UpdateProductBookmark(string bookmarks)
    {
        string email = Panels.instance.memberInfo.member.email;
        string sql = "UPDATE member SET productBookmarks = '" + bookmarks + "' WHERE email = '" + email + "';";
        DbConnecter.instance.ExecuteSQL(sql,true);
        Panels.instance.memberInfo.member.productBookmarks = bookmarks;
    }

    void UpdateIngredientBookmark(string bookmarks)
    {
        string email = Panels.instance.memberInfo.member.email;
        string sql = "UPDATE member SET ingredientBookmarks = '" + bookmarks + "' WHERE email = '" + email + "';";
        DbConnecter.instance.ExecuteSQL(sql, true);
        Panels.instance.memberInfo.member.ingredientBookmarks = bookmarks;
    }

    string IntListToString(List<int> IntList)
    {
        if (IntList.Count > 0)
        {
            List<int> reversedList = new List<int>();
            for (int i = 0; i < IntList.Count; i++)
                reversedList.Add(IntList[IntList.Count - i - 1]);
            IntList = reversedList;
        }
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
        if(lists.Count>0)
        {
            List<int> reversedList = new List<int>();
            for (int i = 0; i < lists.Count; i++)
                reversedList.Add(lists[lists.Count - i - 1]);
            return reversedList;
        }
        else
            return lists;
    }


}
