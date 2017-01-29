using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_movement : MonoBehaviour 
{
    public float timer;
    public float endTime;
    private bool gameEnd = false;
    public float speed;
    public Vector3 Target;

    GameObject obj;

	// Use this for initialization
	void Start () 
    {
        //gameObject.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f));
	}

    public void Move()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Target, speed);
        if (!gameEnd)
        {
            updateTimer();
            if (Vector3.Distance(gameObject.transform.position, Target) < 0.2)
                newTarget();
        }

        if (timer > endTime)
        {
            gameEnd = true;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        //Move();
        //transform.position = Vector3.MoveTowards(transform.position, Target, speed);
        //if (!gameEnd)
        //{
        //    updateTimer();
        //    if (Vector3.Distance(gameObject.transform.position, Target) < 0.2)
        //        newTarget();
        //}

        //if (timer > endTime)
        //{
        //    gameEnd = true;
        //}
	}

    private void newTarget()
    {
        float Xpos = Random.Range(-5.0f, 5.0f);
        float Ypos = Random.Range(-4.0f, 4.0f);
        Target = new Vector3(Xpos, Ypos);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, Target, speed);    
    }

    private void updateTimer()
    {
        timer += Time.deltaTime;
    }
}
