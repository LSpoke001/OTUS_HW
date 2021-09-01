using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public float speed;
    
    private Transform secondPoint;
    
    
    void Start()
    {
        transform.position = point1.position;
    }

    private void Update()
    {
        if (transform.position == point1.position)
        {
            secondPoint = point2;
        } else if (transform.position == point2.position)
        {
            secondPoint = point3;
        } else if(transform.position == point3.position)
        {
            secondPoint = point1;
        }
        transform.position = Vector3.MoveTowards(transform.position, secondPoint.position, speed * Time.deltaTime);
    }
}
