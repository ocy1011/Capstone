using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInPanel : MonoBehaviour {

    public InputField emailField;
    public InputField passwordField;
    GameObject exPanel;

    public void OnPanel(GameObject exPanel)
    {
        this.exPanel = exPanel;
        exPanel.gameObject.SetActive(false);
        emailField.text = "";
        passwordField.text = "";
    }
    
    public void LogIn()
    {
        string email = emailField.text;
        string password = passwordField.text;
        if(Panels.instance.memberInfo.Login(email, password)==false)
        {
            Debug.Log("로그인 실패");
        }
        else
        {
            Debug.Log("로그인 성공");
            exPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        Panels.instance.userPanel.CheckLog();
    }

    public void MoveToSignIn()
    {
        Panels.instance.signInPanel.gameObject.SetActive(true);
        Panels.instance.signInPanel.OnPanel(gameObject);
    }

    public void BackButton()
    {
        exPanel.SetActive(true);
        if (exPanel.GetComponent<ProductInfoPanel>())
            Panels.instance.productInfoPanel.CheckBookmark();
        if (exPanel.GetComponent<IngredientPanel>())
            exPanel.GetComponent<IngredientPanel>().CheckBookmark();
        gameObject.SetActive(false);
        TextPanel[] textPanels = exPanel.transform.GetComponentsInChildren<TextPanel>();
        for (int i = 0; i < textPanels.Length; i++)
            textPanels[i].ChangeTextSize();
    }
}
