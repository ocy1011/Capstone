using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    syntheticDetergent = 0, //합성세제
    bleach = 1, //표백제
    softner = 2, //섬유유연제
    freshener = 3, //방향제
    deodorant = 4, //탈취제
    detergent = 5 //세정제
}

[System.Serializable]
public class Product {

    public int id;
    public string name;
    public string company;
    public Category category;
    public string barcode;
    public string ingredients;
    public string imagePath;

    public Product()
    {

    }

    public string categoryKorean()
    {
        string categoryKorean = "";
        if (category == Category.syntheticDetergent)
            categoryKorean = "합성세제";
        else if (category == Category.bleach)
            categoryKorean = "표백제";
        else if (category == Category.softner)
            categoryKorean = "섬유유연제";
        else if (category == Category.freshener)
            categoryKorean = "방향제";
        else if (category == Category.deodorant)
            categoryKorean = "탈취제";
        else if (category == Category.detergent)
            categoryKorean = "세정제";
        return categoryKorean;
    }

    public Category koreanToCategory(string korean)
    {
        Category category = Category.syntheticDetergent;
        if (korean == "합성세제")
            category = Category.syntheticDetergent;
        else if (korean == "표백제")
            category = Category.bleach;
        else if (korean == "섬유유연제")
            category = Category.softner;
        else if (korean == "방향제")
            category = Category.freshener;
        else if (korean == "탈취제")
            category = Category.deodorant;
        else if (korean == "세정제")
            category = Category.detergent;
        return category;
    }
}
