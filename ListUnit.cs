using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListUnit : MonoBehaviour, IPointerClickHandler {

    Product product;
    GameObject parentPanel;

    public void InfoChange(Product product, GameObject parentPanel)
    {
        this.product = product;
        this.parentPanel = parentPanel;

        TextPanel textPanel = transform.GetChild(0).GetComponent<TextPanel>();
        textPanel.Change(product.name);
    }

    public void OnPointerClick(PointerEventData data)
    {
        Panels.instance.productInfoPanel.gameObject.SetActive(true);
        Panels.instance.productInfoPanel.PanelOn(product, parentPanel);
        parentPanel.GetComponent<ProductSearchPanel>().DeleteAll();
        parentPanel.SetActive(false);
    }
}
