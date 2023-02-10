using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeCtrl : MonoBehaviour
{
    public bool isCountDown = true;
    public bool isTimeOver = false; // true => 타이머정지

    public float gameTime = 0f;
    public float displayTime = 0f; // 표시 시간

    float curTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimeOver)
        {
            curTime += Time.deltaTime;
           // UnityEngine.Debug.Log("CURTIME : " + curTime);
            if (isCountDown)
            {
                displayTime = gameTime - curTime;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }
            else
            {
                displayTime = curTime;
                if (displayTime > gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }

            }
          //  UnityEngine.Debug.Log("Times : " + displayTime);
        }
    }
}
