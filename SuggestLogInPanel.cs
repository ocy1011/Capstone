using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestLogInPanel : MonoBehaviour {

    GameObject exPanel;

    public void PanelOn(GameObject exPanel)
    {
        this.exPanel = exPanel;

    }

    public void PanelOff()
    {
        gameObject.SetActive(false);
    }

    public void MovetoLogIn()
    {
        PanelOff();
        LogInPanel logInPanel = Panels.instance.logInPanel;
        logInPanel.gameObject.SetActive(true);
        logInPanel.OnPanel(exPanel);
        exPanel.gameObject.SetActive(false);
    }

    public void MoveToSignIn()
    {
        PanelOff();
        SignInPanel signInPanel = Panels.instance.signInPanel;
        signInPanel.gameObject.SetActive(true);
        signInPanel.OnPanel(exPanel);
        exPanel.gameObject.SetActive(false);
    }
}
