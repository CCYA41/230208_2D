using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;          // x의 이동거리
    public float moveY = 0.0f;          // y의 이동거리
    public float times = 0.0f;
    public float wait = 0.0f;           // 정지시간

    public bool isMoveWhenOn = false;
    public bool isCanMove = true;

    float perDx;                        // 1프레임당 x 이동 값
    float perDy;                        // 1프레임당 y 이동 값

    Vector3 defPos;                     // 초기 위치

    bool isReverse = false;             //

    void Start()
    {
        defPos = this.transform.position;
        // 1프레임에 걸리는 시간 == 이동시간
        float timeStep = Time.fixedDeltaTime;
        // 1프레임의 x 이동값
        perDx = moveX / (1.0f / timeStep * times);
        // 1프레임의 y 이동값
        perDy = moveY / (1.0f / timeStep * times);
    }
    private void FixedUpdate()
    {
        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;

            bool endX = false;
            bool endY = false;

            if (isReverse)
            {
                if ((perDx >= 0.0f && x <= defPos.x + moveX) || (perDx < 0.0f && x >= defPos.x + moveX))
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y <= defPos.y + moveY) || (perDy < 0.0f && y >= defPos.y + moveY))
                {
                    endY = true;
                }

                Vector3 v = new Vector3(-perDx, -perDy, defPos.z);
                transform.Translate(v);
            }
            else
            {
                if ((perDx >= 0.0f && x >= defPos.x + moveX) || (perDx < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y >= defPos.y + moveY) || (perDy < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;
                }

                Vector3 v = new Vector3(perDx, perDy, defPos.z);
                transform.Translate(v);
            }
            if(endX && endY)
            {
                if (isReverse)
                {
                    transform.position = defPos;
                }

                isReverse = !isReverse;
                isCanMove = false;

                if (isMoveWhenOn == false)
                {
                    Invoke("Move", wait);
                }
            }
        }
    }

    private void Move()
    {
        isCanMove = true;
    }
    private void Stop()
    {
        isCanMove = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
