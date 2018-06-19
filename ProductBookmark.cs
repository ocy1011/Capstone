using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBookmark : MonoBehaviour {

    public int productID;

    public void Delete()
    {
        Panels.instance.bookmarkPanel.DeleteProductBookmark(productID, this);
    }
}
