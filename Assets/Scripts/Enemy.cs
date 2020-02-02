using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public Transform[] points;
    int currentPoint;
    Vector2[] pointsPos;
    // Start is called before the first frame update
    void Start(){
        pointsPos = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            pointsPos[i] = points[i].position;
        }
        currentPoint = 1;
        transform.position = points[0].position;
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, pointsPos[currentPoint], speed * Time.deltaTime);
        if((Vector2)transform.position == pointsPos[currentPoint]){
            currentPoint++;
            if (currentPoint >= pointsPos.Length)
                currentPoint = 0;
        }

    }
}
