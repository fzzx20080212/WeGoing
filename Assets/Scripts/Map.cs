﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Map:MonoBehaviour {
    //对象池
    public List<Circle> circleList;
    //对象池最大数量
    int maxNum = 10;

    float moveSpeed=4;
    GameObject circleSprite;

    //生成的角度范围
    float angleRange = Mathf.PI/6;
    //生成的圆的半径范围
    public float minRadius = 1;
    public float maxRadius = 4;
    public Image myInage;

    //当前最后一次生成的圆
    Circle curCircle;
    void Start()
    {
        circleList = new List<Circle>();
        circleSprite = Resources.Load("circle") as GameObject;
        Init();

    }

 
    //初始化对象池
    void Init()
    {

        for (int i = 0; i <maxNum; i++)
        {
            circleList.Add(new Circle(Instantiate(circleSprite).transform));
        }
        //第一个位置固定
        curCircle = getEmptyCircle();

        curCircle.Init(new Vector3(0, -10, 0), 3);
        for (int i = 0; i <= maxNum; i++)
            CreateCircle();
    }

    void FixedUpdate()
    {
        Move();
        CreateCircle();
    }

    
    void CreateCircle()
    {
        Circle circle= getEmptyCircle();
        if (circle == null)
            return ;
        //生成的圆与上一个圆的角度
        float angle =Random.Range(-angleRange, angleRange);
        float radius = Random.Range(minRadius,maxRadius);
        circle.Init(new Vector3(curCircle.center.x +(radius + curCircle.m_Radius)* Mathf.Sin(angle), curCircle.center.y+(radius + curCircle.m_Radius) * Mathf.Cos(angle), 0), radius);
        curCircle = circle;
    }

    //获得未被使用的对象
    Circle getEmptyCircle()
    {
        foreach(Circle c in circleList)
        {
            if (!c.isUse)
                return c;
        }
        return null;
    }

    public void Move()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 distance = new Vector3(0, -moveDistance, 0);
        foreach(Circle c in circleList)
        {
            if (!c.isUse)
                continue;
            c.Down(distance);
        }
    }


	
}
