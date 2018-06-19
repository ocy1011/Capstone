using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPanel : MonoBehaviour {

    public void CheckLog()
    {
        Transform loggedOutPanel = transform.GetChild(0);
        Transform loggedInPanel = transform.GetChild(1);
        if (Panels.instance.memberInfo.isNull()==true)
        {
            loggedOutPanel.gameObject.SetActive(true);
            loggedInPanel.gameObject.SetActive(false);
        }
        else
        {
            loggedOutPanel.gameObject.SetActive(false);
            loggedInPanel.gameObject.SetActive(true);
        }
        
    }

    public void OnBookmarkButton()
    {
        Panels.instance.bookmarkPanel.gameObject.SetActive(true);
        Panels.instance.bookmarkPanel.OnPanel(Panels.instance.homePanel.gameObject);
    }

    public void Logout()
    {
        Panels.instance.memberInfo.member = new Member();
        CheckLog();
    }

    public void LeaveMember()
    {
        string email = Panels.instance.memberInfo.member.email;
        string sql = "DELETE FROM review WHERE email = '" + email + "';";
        DbConnecter.instance.ExecuteSQL(sql,false);
        sql = "DELETE FROM member WHERE email = '"+email+"';";
        DbConnecter.instance.ExecuteSQL(sql,true);
        DbConnecter.instance.CloseConnection();
        Logout();
        Debug.Log("탈퇴 완료");
    }

    public void MoveToLogIn()
    {
        Panels.instance.logInPanel.gameObject.SetActive(true);
        Panels.instance.logInPanel.OnPanel(Panels.instance.homePanel.gameObject);
    }

    public void MoveToSignIn()
    {
        Panels.instance.signInPanel.gameObject.SetActive(true);
        Panels.instance.signInPanel.OnPanel(Panels.instance.homePanel.gameObject);
    }

    public void MoveToMyReview()
    {
        Panels.instance.myReviewPanel.gameObject.SetActive(true);
        Panels.instance.myReviewPanel.PanelOn(Panels.instance.homePanel.gameObject);
    }
}
