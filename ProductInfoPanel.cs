using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductInfoPanel : MonoBehaviour {
    
    char splitChar = ';';
    GameObject exPanel;
    public Content content;
    public ProductUnit productUnit;
    public RectTransform ingredientListPanel;
    public Transform ingredientInfoPanel;
    public Text bookmarkText;
    public Transform reviewSummaryPanel;
    public CircleGraph circleGraph;
    bool isBookmark = false;
    Product product;
    List<IngredientInfo> ingredientInfos = new List<IngredientInfo>();

    public void PanelOn(Product product, GameObject exPanel)
    {
        this.product = product;
        this.exPanel = exPanel;
        exPanel.gameObject.SetActive(false);
        List<Ingredient> ingredients = new List<Ingredient>();
        List<int> ingredientIntList = StringToIntList(product.ingredients);
        DbConnecter.instance.Connect();
        for(int i=0; i<ingredientIntList.Count; i++)
        {
            string sql = "SELECT * FROM ingredient WHERE id = " + ingredientIntList[i] + ";";
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
            ingredients.Add(ingredient);
            reader.Close();
        }
        DbConnecter.instance.CloseConnection();
        CheckBookmark();
        
        AllChange(ingredients);

        productUnit.InfoChange(product, gameObject);
        ChangeReviewSummary();
        circleGraph.Making(ingredients);
    }

    public void CheckBookmark()
    {
        isBookmark = false;
        if(Panels.instance.memberInfo.isNull()==false)
        {
            List<int> productBookmarkList = StringToIntList(Panels.instance.memberInfo.member.productBookmarks);
            if (productBookmarkList.Contains(product.id))
                isBookmark = true;
        }
        
        BookmarkButtonChange();
    }

    public void ChangeReviewSummary()
    {
        Transform starPanel = reviewSummaryPanel.GetChild(1);
        Transform scorePanel = reviewSummaryPanel.GetChild(2);

        //string sql = "SELECT COUNT(*), AVG(CAST(score as FLOAT)) FROM review GROUP BY productID HAVING productID = " + product.id + ";";
        string sql = "SELECT COUNT(*), AVG(score) FROM review GROUP BY productID HAVING productID = " + product.id + ";";
        MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
        int reviewer = 0;
        float score = 0;
        if (reader.Read())
        {
            reviewer = reader.GetInt32(0);
            score = (float)reader.GetDouble(1);
        }
        reader.Close();
        DbConnecter.instance.CloseConnection();
        for(int i=0; i<5; i++)
        {
            float fillAmount = Mathf.Clamp(score - i, 0, 1);
            Image yellowStar = starPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            yellowStar.fillAmount = fillAmount;
        }
        Text scoreText = scorePanel.GetChild(0).GetComponent<Text>();
        scoreText.text = score.ToString("0.00") + " (" + reviewer + ")"; 
    }

    

    void OnSuggetLogInPanel()
    {
        Panels.instance.suggestLogInPanel.gameObject.SetActive(true);
        Panels.instance.suggestLogInPanel.PanelOn(gameObject);
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

    void BookmarkButtonChange()
    {
        if (isBookmark == false)
            bookmarkText.text = "즐겨찾기 추가";
        else
            bookmarkText.text = "즐겨찾기 삭제";
    }

    public void BookmarkButtonClick()
    {
        string email = Panels.instance.memberInfo.member.email;
        DbConnecter.instance.Connect();

        if (Panels.instance.memberInfo.isNull()==false)
        {
            isBookmark = !isBookmark;
            BookmarkButtonChange();

            string exBookmarks = Panels.instance.memberInfo.member.productBookmarks;
            int productID = product.id;
            if (isBookmark == false)
            {
                List<int> intList = StringToIntList(exBookmarks);
                intList.Remove(productID);
                string bookmarks = IntListToString(intList);
                UpdateBookmark(bookmarks);
            }
            else
            {
                string bookmarks = exBookmarks + productID.ToString() + splitChar;
                UpdateBookmark(bookmarks);
            }
            DbConnecter.instance.CloseConnection();
        }
        else
        {
            Debug.Log("로그인 화면 띄우기");
            OnSuggetLogInPanel();
        }
    }

    void UpdateBookmark(string bookmarks)
    {
        string email = Panels.instance.memberInfo.member.email;
        string sql = "UPDATE member SET productBookmarks = '"+bookmarks+"' WHERE email = '"+ email+"';";
        DbConnecter.instance.ExecuteSQL(sql,true);
        Panels.instance.memberInfo.member.productBookmarks = bookmarks;
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
        exPanel.SetActive(true);
        if(exPanel.GetComponent<BookmarkPanel>())
            exPanel.GetComponent<BookmarkPanel>().ResetBookmarks(true, false);
        if (exPanel.GetComponent<HomePanel>())
            exPanel.GetComponent<HomePanel>().OnSearchPanel();
        TextPanel[] textPanels = exPanel.transform.GetComponentsInChildren<TextPanel>();
        for (int i = 0; i < textPanels.Length; i++)
            textPanels[i].ChangeTextSize();
        int childCount = ingredientListPanel.transform.childCount;
        if(childCount>0)
        {
            for (int i = 0; i < childCount; i++)
                Destroy(ingredientListPanel.transform.GetChild(childCount - 1 - i).gameObject);
        }
        Destroy(ingredientListPanel.GetComponent<SavedRectTransform>());
        RectTransform rectTransform = ingredientListPanel.GetComponent<RectTransform>();
        SizeChange(rectTransform, 0.63f, 0);
    }

    void AllChange(List<Ingredient> ingredients)
    {
        float startYMax = 0.27f;
        float nextYMax = 0.27f;
        nextYMax = ShowIngredient(ingredients, startYMax, 0.1f);
        if (nextYMax < 0)
        {
            float contentSize = content.getInitialContentYSize() * (1 - (nextYMax));
            content.ChangeSize(contentSize);
        }
        else
        {
            float contentSize = content.getInitialContentYSize();
            content.ChangeSize(contentSize);
        }
    }

    float SizeChange(RectTransform rectTransform, float yMax, float yMin)
    {
        rectTransform.anchorMin = new Vector2(0, yMin);
        rectTransform.anchorMax = new Vector2(1, yMax);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        return yMin;
    }

    float ShowIngredient(List<Ingredient> ingredients, float startYMax, float yRate)
    {
        int amount = ingredients.Count;
        float yAnchorHeight = amount * yRate;
        float nextYMax = SizeChange(ingredientListPanel, startYMax, startYMax - yAnchorHeight);
        for (int i=0; i<amount; i++)
        {
            Transform newTextBox = Instantiate(ingredientInfoPanel);
            newTextBox.SetParent(ingredientListPanel.transform);
            RectTransform rectTransform = newTextBox.GetComponent<RectTransform>();
            float yMax = 1 - i/(float)amount;
            float yMin = 1 - (i+1) / (float)amount;
            SizeChange(rectTransform, yMax, yMin);
            IngredientInfo ingredientInfo = newTextBox.GetComponent<IngredientInfo>();
            Ingredient ingredient = ingredients[i];
            ingredientInfo.IngredientChange(ingredient, gameObject);
        }
        return nextYMax;
    }

    public void MoveToReviewPanel()
    {
        Panels.instance.reviewPanel.gameObject.SetActive(true);
        Panels.instance.reviewPanel.PanelOn(gameObject, product);
        gameObject.SetActive(false);
    }

}
