using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ticker : MonoBehaviour
{
    public float angle;

    public float radius = 1.984f;

    public float speed = 1.5f;
    public GameObject Lock;
    ScoreTracker scoreTracker;
    public float randomSpeed = 1;
    public float logConst = 1;

    void Start()
    {
        scoreTracker = Lock.GetComponent<ScoreTracker>();
        angle = 0;
        transform.localPosition = new Vector3(0, radius, 0);
    }


    void Update()
    {
        if (scoreTracker.score > 10)
        {
            logConst = Mathf.Log10(scoreTracker.score);
        }

        angle += Time.deltaTime * speed * logConst;

        if (angle >= Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }

        transform.localPosition = new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0);
        transform.localRotation = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);
    }

    public void reverse()
    {
        if (logConst > 10)
        {
            speed /= logConst;
            logConst = Mathf.Log10(scoreTracker.score);
            speed *= logConst;
        }

        speed /= randomSpeed;
        randomSpeed = Random.Range(0.8f, 1.2f);
        speed = -speed * randomSpeed;
    }

    void OnDisable()
    {
        angle = 0;

        transform.localPosition = new Vector3(0, radius, 0);
        transform.localRotation = Quaternion.Euler(0, 0, -angle * 0);
    }

    void OnEnable()
    {
        speed = 1.5f;
        randomSpeed = 1;
        logConst = 1;
    }
}
