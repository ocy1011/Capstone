using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewUnit : MonoBehaviour {

    Review review;
    public Text emailText;
    public Transform starPanel;
    public Text first;
    public Text second;
    public Text third;
    public Text fourth;
    public Text fifth;
    public GameObject deleteButton;
    GameObject parentPanel;

    public void Change(Review review, GameObject parentPanel)
    {
        this.review = review;
        this.parentPanel = parentPanel;
        emailText.text = review.email;
        first.text = "구입 장소 - " + review.firstText(review.first);
        second.text = "사용 기간 - " + review.secondText(review.second);
        third.text = "피부 반응 여부 - " + review.thirdText(review.third);
        fourth.text = "재구매 횟수 - " + review.fourthText(review.fourth);
        fifth.text = "구매경로 - " + review.fifthText(review.fifth);
        ChangeStars(review.score);
        if (review.email == Panels.instance.memberInfo.member.email)
            deleteButton.SetActive(true);
        else
            deleteButton.SetActive(false);

    }

    public void DeleteButton()
    {
        int productID = review.productID;
        string sql = "DELETE FROM review WHERE email = '"+review.email+"' AND productID = "+ productID + ";";
        DbConnecter.instance.ExecuteSQL(sql, true);
        DbConnecter.instance.CloseConnection();
        if (parentPanel.GetComponent<ReviewPanel>())
            parentPanel.GetComponent<ReviewPanel>().ResetReviewAndRead();
        else if(parentPanel.GetComponent<MyReviewPanel>())
            parentPanel.GetComponent<MyReviewPanel>().DeleteReivew(this);

    }

    void ChangeStars(int score)
    {
        for (int i = 0; i < 5; i++)
        {
            float fillAmount = Mathf.Clamp(score - i, 0, 1);
            Image yellowStar = starPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            yellowStar.fillAmount = fillAmount;
        }
    }
}
