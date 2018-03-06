using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Map:MonoBehaviour {
    //对象池
    public List<Circle> circleList;
    //对象池最大数量
    int maxNum = 10;

    float moveSpeed=2;
    //prefabs
    GameObject circleSprite,playerSprite;
    bool hasStart = false;

    //生成的角度范围
    float angleRange = Mathf.PI/6;
    //生成的圆的半径范围
    public float minRadius = 1;
    public float maxRadius = 4;
    public Image myInage;

    //当前最后一次生成的圆
    Circle curCircle;

    //玩家
    Player player;
    
    void Start()
    {
        circleList = new List<Circle>();
        circleSprite = Resources.Load("circle") as GameObject;
        playerSprite = Resources.Load("player") as GameObject;
        player = new Player(Instantiate(playerSprite).transform);
        Init();
        player.SetCircle(circleList.First());
    }

    void FixedUpdate()
    {
        player.Update();
        Move();
        CreateCircle();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick();
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

    
    
    void CreateCircle()
    {
        Circle circle= getEmptyCircle();
        if (circle == null)
            return ;
        //生成的圆与上一个圆的角度(圆心连线与y轴的夹角)
        float angle =Random.Range(-angleRange, angleRange);
        float radius = Random.Range(minRadius,maxRadius);
        circle.Init(new Vector3(curCircle.center.x +(radius + curCircle.m_Radius)* Mathf.Sin(angle), curCircle.center.y+(radius + curCircle.m_Radius) * Mathf.Cos(angle), 0), radius);

        circle.downAngle = 1.5f * Mathf.PI - angle;
        curCircle.UpCircle = circle;
        circle.DownCircle = curCircle;
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
        if (!hasStart)
            return;
        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 distance = new Vector3(0, -moveDistance, 0);
        foreach(Circle c in circleList)
        {
            if (!c.isUse)
                continue;
            c.Down(distance);
        }
    }


    public void OnClick()
    {
        bool change=player.OnClick();
        if (change && !hasStart)
            hasStart = true;
    }

}
