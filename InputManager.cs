using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour{

    public InputField inputField;
    TouchScreenKeyboard keyboard;
    public Text text;
    public bool search=false;
    public GameObject deleteAllBtn;
    bool check = false;
    public string realText;

    private void Start()
    {
        keyboard = inputField.touchScreenKeyboard;
        //inputField.ActivateInputField();
        //inputField.onValueChanged.AddListener(delegate { InputChnaged(); });
    }

    void Update()
    {
        if (inputField.isFocused == true)
        {
            if(search==true)
            {
                if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
                {
                    Debug.Log("done");
                }
                if (text.text.Length > 0)
                    deleteAllBtn.SetActive(true);
                else
                    deleteAllBtn.SetActive(false);
            }
            if (Input.compositionString == "")
            {
                if (check == false)
                {
                    check = true;
                    text.text = inputField.text + Input.compositionString;
                }
            }
            else
            {
                check = false;
            }
            if (realText != text.text)
            {
                realText = text.text;
 
            }
            try
            {
                if (Input.inputString[0] == 8)
                    Panels.instance.productSearchPanel.DeleteAll();
            }
            catch
            {

            }
             
            
        }
        else
        {
            inputField.text = realText;
        }
    }
    
    public void DeleteAll()
    {
        text.text = "";
        realText = "";
        inputField.text = "";
        deleteAllBtn.SetActive(false);
    }

    public void ResetText()
    {
        text.text = "";
        realText = "";
    }

    public string ChangedText()
    {
        return realText;
    }

}
