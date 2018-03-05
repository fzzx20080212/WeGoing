using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {

    //当前绕着转的圈圈
    Circle myCircle;
    //角色
    public Transform player;
    float pHalfWidth;
    float rotateRadius;
    //旋转速度
    float rotateSpeed=Mathf.PI/3;
    float curAngle;
    //旋转方向
    enum Direction
    {
        shun=1,
        ni=-1,
    }
    Direction direction;

    public Player(Transform player)
    {
        this.player = player;
        curAngle = 0;
        direction = Direction.shun;
        pHalfWidth = player.GetComponent<SpriteRenderer>().sprite.bounds.size.x*player.localScale.x/2;
    }

    public void Update()
    {
        Rolling();
    }

    void Rolling()
    {
        float deleteAugle = Time.fixedDeltaTime * rotateSpeed;
        curAngle = (curAngle + deleteAugle) % (2 * Mathf.PI);
        Vector3 pos = new Vector3(rotateRadius * Mathf.Cos(curAngle), rotateRadius * Mathf.Sin(curAngle), 0);
        player.position = pos+myCircle.center;
        player.Rotate(new Vector3(0,0, deleteAugle*180/Mathf.PI));
    }

    //设置正在旋转这的圆
    public void SetCircle(Circle circle)
    {
        myCircle = circle;
        rotateRadius = myCircle.m_Radius-pHalfWidth;
    }
	
    //换轨道
    public void ChangePath()
    {
        rotateRadius = myCircle.m_Radius > rotateRadius ? myCircle.m_Radius + pHalfWidth : myCircle.m_Radius - pHalfWidth;
    }
}
