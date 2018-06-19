using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class ProductUnit : MonoBehaviour, IPointerClickHandler
{
    Product product;
    GameObject parentPanel;
    public RawImage image;
    public Text category;
    public Text company;
    public TextPanel productName;

    public void InfoChange(Product product, GameObject parentPanel)
    {
        this.product = product;
        this.parentPanel = parentPanel;
        category.text = product.categoryKorean();
        company.text = product.company;
        //if (product.imagePath != null)
            //image.texture = ReadImage(product.imagePath);
        //else
        image.texture = Resources.Load<Texture2D>("TestImage\\" + product.id);
        ChangeImageSize(5);
        productName.Change(product.name);
    }

    public void OnPointerClick(PointerEventData data)
    {
        if(GetComponent<Button>())
        {
            parentPanel.SetActive(false);
            Panels.instance.productInfoPanel.gameObject.SetActive(true);
            Panels.instance.productInfoPanel.PanelOn(product, parentPanel);
        } 
    }

    void ChangeImageSize(float f)
    {
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        Vector2 tempSize = image.transform.parent.GetComponent<RectTransform>().rect.size;
        Vector2 size = Vector2.one * tempSize.x;
        if (tempSize.y < tempSize.x)
            size = Vector2.one * tempSize.y;
        size -= Vector2.one * f;
        rectTransform.sizeDelta = size;
    }

    Texture2D ReadImage(string path)
    {
        Texture2D texture = new Texture2D(2, 2);
        byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            texture.LoadImage(fileData);
            return texture;
        }
        else
            return null;
    }
}
