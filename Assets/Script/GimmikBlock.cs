using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmikBlock : MonoBehaviour
{
    public float length = 0.0f;     // 플레이어와 거리 설정
    public bool isDelete = false;   // 바닥에 닿이면 사라질것인지 아닌지

    bool isFall = false;            // 바닥에 닿았는지 플래그
    float fadeTime = 0.5f;          // 페이드 아웃 연출시간

    void Start()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }

        if (isFall)
        {
            // 떨어진 것을 확인후 사라질 오브젝트이면 연출
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime;

            GetComponent<SpriteRenderer>().color = col;

            if (fadeTime <= 0.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDelete && collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isFall = true;
        }
    }
}
