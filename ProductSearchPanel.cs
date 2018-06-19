using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductSearchPanel : MonoBehaviour {

    GameObject exPanel;
    List<ListUnit> productList = new List<ListUnit>();
    public Transform listUnit;
    public Transform listPanel;

    public InputManager inputManager;

    public void PanelOn(GameObject exPanel)
    {
        this.exPanel = exPanel;
        exPanel.SetActive(false);
        DeleteAll();
    }
    
    public void BackButton()
    {
        exPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DeleteAll()
    {
        int listCount = productList.Count;
        if(listCount>0)
        {
            for (int i = 0; i < listCount; i++)
            {
                Destroy(productList[listCount - i - 1].gameObject);
            }
        }
        productList = new List<ListUnit>();
        inputManager.DeleteAll();
    }

    public void Search()
    {
        string str = inputManager.ChangedText();
        if(str.Length>0)
        {
            string sql = "SELECT * FROM product WHERE name LIKE '%" + str + "%';";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            int count = 0;
            while (reader.Read())
            {
                Transform newUnit = Instantiate(listUnit);
                newUnit.SetParent(listPanel);
                RectTransform rectTransform = newUnit.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0, 0.9f - count*0.1f);
                rectTransform.anchorMax = new Vector2(1, 1f - count*0.1f);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

                Product product = new Product();
                product.id = reader.GetInt32(0);
                product.name = reader.GetString(1);
                product.barcode = reader.GetString(2);
                product.company = reader.GetString(3);
                product.category = product.koreanToCategory(reader.GetString(4));
                product.ingredients = reader.GetString(5);
                product.imagePath = reader.GetString(6);

                ListUnit unitInfo = newUnit.GetComponent<ListUnit>();
                unitInfo.InfoChange(product, gameObject);
                productList.Add(unitInfo);

                count++;
                if (count == 10)
                    break;
            }
            reader.Close();
            DbConnecter.instance.CloseConnection();
        }
    }
}
