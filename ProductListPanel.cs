using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ProductListPanel : MonoBehaviour {
    public RectTransform content;
    GameObject exPanel;
    int onePageAmount = 10;

   public void Search(GameObject exPanel)
    {
        this.exPanel = exPanel;
        
    }
}
