using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround{
    public GameObject bg_1, bg_2;
    float moveSpeed ;
    float screenH;
    public BackGround(float mapH)
    {
        moveSpeed = mapH / 20;
        screenH = mapH;
        bg_1 = GameObject.Find("Canvas/BG_0");
        bg_2 = GameObject.Find("Canvas/BG_1");
        bg_1.transform.position = new Vector3(0,screenH,0);
    }

    public void move()
    {
        bg_1.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        bg_2.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        check(bg_1);
        check(bg_2);
        
    }

    void check(GameObject bg)
    {
        if (bg.transform.position.y < -screenH)
            bg.transform.position = new Vector3(0, screenH, 0);
    }
}
