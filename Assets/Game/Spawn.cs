using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CYF_Circle
{
    public GameObject gameObj { get; set; }
    public Vector3 Target { get; set; }
}

public class Spawn : MonoBehaviour 
{
    public GameObject circlePrefab;
    public Sprite[] colors;
    public List<CYF_Circle> circles;

    public float speed;
    private Vector3 Target;
    public float endTime;
    public float timer;

    private bool isQustionAsked = false;

	// Use this for initialization
	void Start () 
    {
        circles = new List<CYF_Circle>();
        if(colors.Length > 0)
        {
            generateRandomCircles(10);
        }
	}
	
    void generateRandomCircles(int num)
    {
        for(int i = 0; i < num; ++i)
        {
            int arrayIdx = Random.Range(0, colors.Length-1);
            Sprite color = colors[arrayIdx];
            string name = color.name;

            CYF_Circle circle = new CYF_Circle();
            circle.gameObj = Instantiate(circlePrefab);
            circle.gameObj.name = name;
            circle.gameObj.GetComponent<SpriteRenderer>().sprite = color;
            circle.gameObj.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f));
            circles.Add(circle);
        }
    }

    private void MoveCircles()
    {
        for(int i = 0; i < 10; ++i)
        {
            moveCircle(circles[i]);
        }
    }

    private Vector3 newTarget()
    {
        float Xpos = Random.Range(-5.0f, 5.0f);
        float Ypos = Random.Range(-4.0f, 4.0f);
        return new Vector3(Xpos, Ypos);
    }

    private void moveCircle(CYF_Circle circle)
    {
        circle.gameObj.transform.position =  Vector3.MoveTowards(circle.gameObj.transform.position, circle.Target, speed);

        if (Vector3.Distance(circle.gameObj.transform.position, circle.Target) < 0.2)
        {
            circle.Target = newTarget();
        }
    }

    void Update()
    {
        if(timer < endTime)
        {
            MoveCircles();
            updateTimer();
        }
        else
        {
            if (!isQustionAsked)
            {
                doQustionMarkedCircle(circles[Random.Range(0, circles.Count - 1)]);
                isQustionAsked = true;
            }
        }
    }

    private void doQustionMarkedCircle(CYF_Circle circle)
    {
        circle.gameObj.GetComponent<SpriteRenderer>().sprite = colors[2];
    }

    private void updateTimer()
    {
        timer += Time.deltaTime;
    }
}
