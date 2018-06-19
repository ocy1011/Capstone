using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBookmark : MonoBehaviour {

    public int ingredientID;

    public void Delete()
    {
        Panels.instance.bookmarkPanel.DeleteIngredientBookmark(ingredientID, this);
    }
}
