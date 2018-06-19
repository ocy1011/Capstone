using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReivewSummary : MonoBehaviour {
    public Transform evaluationPanel;
    public Text averageText;
    List<Evaluation> evaluations = new List<Evaluation>();

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 4; i++)
            evaluations.Add(evaluationPanel.GetChild(i).GetComponent<Evaluation>());
        ChangeEvaluations(2.5f, 4.0f, 4.5f, 3.1f);
    }

    public void ChangeEvaluations(float score1, float score2, float score3, float score4)
    {
        float average = (score1 + score2 + score3 + score4) / 4;
        average = Mathf.Ceil(average * 100) / 100f;
        averageText.text = "평균 평점 " + average;
        evaluations[0].ChangeScore(score1);
        evaluations[1].ChangeScore(score2);
        evaluations[2].ChangeScore(score3);
        evaluations[3].ChangeScore(score4);
    }
}
