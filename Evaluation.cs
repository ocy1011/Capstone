using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour {
    public float score;
    public Text scoreText;
    public List<Image> stars = new List<Image>();
	// Use this for initialization

    public void ChangeScore(float newScore)
    {
        score = newScore;
        scoreText.text = "" + score;
        for(int i=0; i<5; i++)
        {
            float value = score - (float)i;
            float starRate = Mathf.Clamp(value, 0, 1);
            stars[i].fillAmount = starRate;
        }
    }
}
