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
    //在中间的星球图片
    GameObject myPlanet;
    //为实例池做准备
    public bool isUse;
    //上一个圈和下一个圈（用于切换圈）
    Circle upCircle, downCircle;
    //上交点和下交点
    public Vector3 upNode, downNode;
    //圆心到上下两交点的角度
    public float downAngle,upAngle;
    //碰撞半径
    float colRadius=2;
    //地图的半高
    float mapHeigth_half;
    public Circle UpCircle
    {
        set
        {
            upCircle = value;
            upNode = (value.center - center).normalized * m_Radius;
        }
        get
        {
            return upCircle;
        }
    }

    public Circle DownCircle
    {
        set
        {
            downCircle = value;
            downNode = (value.center - center).normalized * m_Radius;
        }
        get
        {
            return downCircle;
        }
    }
    public Circle(Transform circle,float height)
    {
        isUse = false;
        myCircle = circle;
        mapHeigth_half = height;
    }

    public void Init(Vector3 center, float radius,GameObject planet)
    {
        m_Radius = radius;
        myCircle.position = center;
        myCircle.localScale = new Vector3(m_Radius, m_Radius, 0);
        isUse = true;
        myPlanet = planet;
        myPlanet.transform.position = center;
    }

    

    public void Down(Vector3 pos)
    {
        myCircle.position += pos;
        myPlanet.transform.position += pos;
        if (myCircle.position.y < -mapHeigth_half-m_Radius)
        {
            isUse = false;
            Root.Destroy(myPlanet);
            
            myPlanet = null;
        }

    }


  
    /// <summary>
    /// 检测是否和上下碰撞盒碰撞
    /// </summary>
    public int HasCollided(Player player)
    {
        Vector3 pos = player.player.position;
     
        if ((pos-upNode-center).magnitude<colRadius)
            return 1;
        if ((pos - downNode - center).magnitude < colRadius)
            return -1;
        return 0;
    }
    

}
