using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpawner : MonoBehaviour
{
    public float angle;
    float radius = 1.984f;
    public GameObject prefab;
    public GameObject parent;
    GameObject instance;
    Ticker ticker;
    float minOffset = 0.6f;
    public float offset;
    ScoreTracker scoreTracker;
    void Start()
    {
        scoreTracker = GetComponent<ScoreTracker>();
        ticker = GetComponentInChildren<Ticker>();
        instance = new GameObject();
    }

    public void moveGoal()
    {
        float direction = Mathf.Sign(ticker.speed);

        Destroy(instance);

        offset = Random.Range(minOffset, Mathf.PI);
        
        if(scoreTracker.score>20){
            offset = Mathf.Pow((offset + (1 - minOffset)), (1 / (1 + (scoreTracker.score-20) / 100.0f))) - (1 - minOffset);
        }

        //why the hell does modulo operator not work correctly.
        angle = (angle + offset * direction) % (Mathf.PI * 2);
        if (angle < 0) { angle += Mathf.PI * 2; }

        Vector3 pos = new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0);
        Quaternion rot = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);

        instance = Instantiate(prefab, pos, rot, parent.transform);
    }

    void OnDisable()
    {
        Destroy(instance);
    }

    void OnEnable()
    {
        angle = 0;
        instance = new GameObject();
    }
}
