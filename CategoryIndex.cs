using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CategoryIndex : MonoBehaviour, IPointerClickHandler {

    public Category category;
    public HomePanel homePanel;
    public CategoryPanel categoryPanel;

	public void OnPointerClick(PointerEventData data)
    {
        homePanel.gameObject.SetActive(false);
        categoryPanel.gameObject.SetActive(true);
        categoryPanel.PanelOn(category);
    }
}
