using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Circle {
    public float m_Radius = 1; // 圆环的半径
    public Vector3 center
    {
        get { return myCircle.position; }
    }
    Transform myCircle;
    //为实例池做准备
    public bool isUse;
    public Circle(Transform circle)
    {
        isUse = false;
        myCircle = circle;
    }

    public void Init(Vector3 center, float radius)
    {
        m_Radius = radius;
        myCircle.position = center;
        myCircle.localScale = new Vector3(m_Radius, m_Radius, 0);
        isUse = true;
    }


    public void Down(Vector3 pos)
    {
        myCircle.position += pos;
        if (myCircle.position.y < -25)
        {
            isUse = false;
        }

    }
  

}
