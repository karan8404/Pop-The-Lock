using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject Lock;
    GoalSpawner goalSpawner;
    ScoreTracker scoreTracker;
    Ticker ticker;
    bool gameOverScreen = false;
    bool gameStarted = false;
    public TMP_Text gameOverText;
    bool currentRayHits;
    bool lastRayHits;

    void Start()
    {
        goalSpawner = Lock.GetComponent<GoalSpawner>();
        scoreTracker = Lock.GetComponent<ScoreTracker>();
        ticker = Lock.GetComponentInChildren<Ticker>();
        ticker.enabled = false;
        gameOverText.enabled = false;
        lastRayHits = false;
    }

    void Update()
    {
        currentRayHits = Physics.Raycast(new Vector3(Mathf.Sin(ticker.angle) * ticker.radius, Mathf.Cos(ticker.angle) * ticker.radius, -1), Vector3.forward, 5);

        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began) && !gameOverScreen)
        {
            if (currentRayHits)//move goal on correct input
            {
                ticker.reverse();

                goalSpawner.moveGoal();
                scoreTracker.increaseScore();
                lastRayHits = false;
                return;
            }
            else if (!gameStarted)//start moving ticker after restart
            {
                gameStarted = true;
                ticker.enabled = true;
                goalSpawner.moveGoal();
            }
            else//gameover on wrong input
            {
                gameOver();

            }
        }
        else if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Began) && gameOverScreen)//restart game
        {
            restart();
        }
        else if (lastRayHits && !currentRayHits)//ticker moves past goal
        {
            gameOver();
        }

        lastRayHits = currentRayHits;
    }

    void gameOver()
    {
        gameOverText.enabled = true;
        gameOverText.text = "Score : " + scoreTracker.score.ToString();

        goalSpawner.enabled = false;
        scoreTracker.enabled = false;
        ticker.enabled = false;

        gameOverScreen = true;
        gameStarted = false;
    }

    void restart()
    {
        gameOverText.enabled = false;

        goalSpawner.enabled = true;
        scoreTracker.enabled = true;
        lastRayHits = false;

        gameOverScreen = false;
    }
}
