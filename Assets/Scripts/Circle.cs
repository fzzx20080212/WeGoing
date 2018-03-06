using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Circle {
    public float m_Radius; // 圆环的半径
    public Vector3 center
    {
        get { return myCircle.position; }
    }
    Transform myCircle;
    //为实例池做准备
    public bool isUse;
    //上一个圈和下一个圈（用于切换圈）
    Circle upCircle, downCircle;
    //上交点和下交点
    public Vector3 upNode, downNode;
    public float downAngle;
    //碰撞半径
    float colRadius=2;
    public Circle UpCircle
    {
        set
        {
            upCircle = value;
            upNode = (value.center - center).normalized * m_Radius;
        }
    }

    public Circle DownCircle
    {
        set
        {
            downCircle = value;
            downNode = (value.center - center).normalized * m_Radius;
        }
    }
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
  
    /// <summary>
    /// 检测是否和上下碰撞盒碰撞
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Circle HasCollided(Player player)
    {
        Vector3 pos = player.player.position;
     
        if ((pos-upNode-center).magnitude<colRadius)
            return upCircle;
        if ((pos - downNode - center).magnitude < colRadius)
            return downCircle;
        return null;
    }
    

}
