using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngredientInfo : MonoBehaviour, IPointerClickHandler {

    Ingredient ingredient;
    public Image image;
    public TextPanel korean;
    public TextPanel english;
    GameObject parentPanel;
    public bool canClick = false;
  
    public void IngredientChange(Ingredient ingredient, GameObject parentPanel)
    {
        this.ingredient = ingredient;
        this.parentPanel = parentPanel;
        korean.Change(ingredient.korean);
        english.Change(ingredient.english);
        image.sprite = Resources.Load<Sprite>("EWG_Image\\"+ingredient.ewgGrade);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canClick==true)
        {
            Panels.instance.ingredientPanel.gameObject.SetActive(true);
            Panels.instance.ingredientPanel.PanelOn(ingredient, parentPanel);
        }
    }
}
