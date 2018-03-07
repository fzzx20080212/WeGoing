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

    float moveSpeed=3;
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

    //地图的宽高
    float mapHeight_half, mapWidth_half;
    //星球数据
    List<CircleData> StarDatas;
    //背景
    BackGround backGround;
    void Start()
    {
        circleList = new List<Circle>();
        circleSprite = Resources.Load("circle") as GameObject;
        playerSprite = Resources.Load("player") as GameObject;
        player = new Player(Instantiate(playerSprite).transform);

        //获得地图的宽高
        Camera camera = Camera.main;
        float orthographicSize = camera.orthographicSize;
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        mapHeight_half = orthographicSize;
        mapWidth_half = mapHeight_half * aspectRatio;

        //获取星球数据
        Data data = new Data();
        StarDatas = data.Read();

        Init();
        player.SetCircle(circleList.First());

        backGround = new BackGround(mapHeight_half * 2);
      
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

    //当前生成到第几个星球
    int num = 0;
    //初始化对象池
    void Init()
    {

        for (int i = 0; i <maxNum; i++)
        {
            circleList.Add(new Circle(Instantiate(circleSprite).transform, mapHeight_half));
        }
        //第一个位置固定
        curCircle = getEmptyCircle();

        curCircle.Init(new Vector3(0, -7, 0), StarDatas[0].radius, Instantiate(Resources.Load("Planets/" +StarDatas[0].prefabName)as GameObject));
        num++;
        space = Random.Range(4, 8);
        for (int i = 0; i <= maxNum; i++)
            CreateCircle();
    }


    //两星球之间隔几个星球
    int space = 0;
    //创建一个星球
    void CreateCircle()
    {
        Circle circle= getEmptyCircle();
        if (circle == null)
            return ;

        //生成的圆与上一个圆的角度(圆心连线与y轴的夹角)
        float angle = Random.Range(-angleRange, angleRange);
        float radius;
        if (space == 0&&num<StarDatas.Count)
        {
            space = Random.Range(4, 8);
            radius = StarDatas[num].radius;
            float x = curCircle.center.x + (radius + curCircle.m_Radius) * Mathf.Sin(angle);
            //判断生成的圆是否会超过范围
            if (x + radius > mapWidth_half || x - radius < -mapWidth_half)
                angle = -angle;

            x = curCircle.center.x + (radius + curCircle.m_Radius) * Mathf.Sin(angle);
            float y = curCircle.center.y + (radius + curCircle.m_Radius) * Mathf.Cos(angle);
            circle.Init(new Vector3(x, y, 0), radius,Instantiate( Resources.Load("Planets/" + StarDatas[num].prefabName) as GameObject));
            num++;
        }
        else
        {
            radius = Random.Range(minRadius, maxRadius);
            float x = curCircle.center.x + (radius + curCircle.m_Radius) * Mathf.Sin(angle);
            //判断生成的圆是否会超过范围
            if (x + radius > mapWidth_half || x - radius < -mapWidth_half)
                angle = -angle;
            x = curCircle.center.x + (radius + curCircle.m_Radius) * Mathf.Sin(angle);
            float y = curCircle.center.y + (radius + curCircle.m_Radius) * Mathf.Cos(angle);
            circle.Init(new Vector3(x, y, 0), radius, Instantiate(Resources.Load("Planets/Falling") as GameObject));
            space--;
        }

        circle.downAngle = 1.5f * Mathf.PI - angle;
        curCircle.upAngle = Mathf.PI / 2 - angle;
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

    bool isSpeedUp = false;
    public void Move()
    {
        backGround.move();
        if (!hasStart)
            return;
        if (player.player.position.y > mapHeight_half/2&&!isSpeedUp)
        {
            moveSpeed *= 3;
            StartCoroutine(StopSpeedUp());
            isSpeedUp = true;
        }

        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 distance = new Vector3(0, -moveDistance, 0);
        foreach(Circle c in circleList)
        {
            if (!c.isUse)
                continue;
            c.Down(distance);
        }
      
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForFixedUpdate();
        if (player.player.position.y < 0)
        {
            moveSpeed /= 3;
            isSpeedUp = false;
        }
        else
            StartCoroutine(StopSpeedUp());

    }

    public void OnClick()
    {
        bool change=player.OnClick();
        if (change && !hasStart)
            hasStart = true;
    }

}
