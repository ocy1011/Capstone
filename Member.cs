using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Member{

    public string email;
    public string password;
    public int gender;
    public int age;
    public int child;
    public string productBookmarks;
    public string ingredientBookmarks;

    public Member()
    {
        email = "";
        password = "";
        gender = 0;
        age = 0;
        child = 0;
        productBookmarks = "";
        ingredientBookmarks = "";
    }
}
