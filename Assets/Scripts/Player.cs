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
    float rotateSpeed;
    //每秒中走得距离
    float rotateDis=4;
    float curAngle;
    //旋转方向
    enum Direction
    {
        shun=-1,
        ni=1,
    }

    enum Pathway
    {
        inPath,
        outPath,
    }
    Direction direction;
    Pathway pathway;
    public Player(Transform player)
    {
        this.player = player;
        curAngle = 0;
        direction = Direction.ni;
        pHalfWidth = player.GetComponent<SpriteRenderer>().sprite.bounds.size.x*player.localScale.x/2;
    }

    public void Update()
    {
        Rolling();
    }

    void Rolling()
    {
        float deleteAugle = (int)direction * Time.fixedDeltaTime * rotateSpeed;
        curAngle = (curAngle + deleteAugle) % (2 * Mathf.PI);
        Vector3 pos = new Vector3(rotateRadius * Mathf.Cos(curAngle),rotateRadius * Mathf.Sin(curAngle), 0);
        player.position =pos+myCircle.center;
        player.Rotate(new Vector3(0,0,deleteAugle *180/Mathf.PI));
    }

    //设置正在旋转这的圆
    public void SetCircle(Circle circle)
    {
        myCircle = circle;
        rotateRadius = myCircle.m_Radius-pHalfWidth;
        pathway = Pathway.inPath;
        rotateSpeed = rotateDis / rotateRadius;

    }
	

    public bool OnClick()
    {
        Circle circle = myCircle.HasCollided(this);
        if (circle == null)
        {
            ChangePath();
            return false;
        }
        if (pathway == Pathway.inPath)
        {
            direction = direction == Direction.ni ? Direction.shun : Direction.ni;
            SetCircle(circle);
            float temp = curAngle;
            curAngle = myCircle.downAngle;
            player.Rotate(0,0,(curAngle -temp-Mathf.PI) * 180 / Mathf.PI);
            return true;
        }
            
        else
            ChangePath();
        return false;

    }

    //换轨道
    public void ChangePath()
    {
        if (pathway == Pathway.inPath)
        {
            rotateRadius = myCircle.m_Radius + pHalfWidth;
            pathway = Pathway.outPath;
        }
        else
        {
            rotateRadius = myCircle.m_Radius - pHalfWidth;
            pathway = Pathway.inPath;
        }
            
    }
}
