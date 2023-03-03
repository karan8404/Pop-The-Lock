using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score;

    void Start()
    {
        score = 0;
        scoreText.text = "0";
    }

    public void increaseScore()
    {
        score = score + 1;
        scoreText.text = score.ToString();
    }

    void OnDisable()
    {
        scoreText.text = "Game Over";
    }

    void OnEnable()
    {
        score = 0;
        scoreText.text = "0";
    }
}
