using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmikBlock : MonoBehaviour
{
    public float length = 0.0f;     // �÷��̾�� �Ÿ� ����
    public bool isDelete = false;   // �ٴڿ� ���̸� ����������� �ƴ���

    bool isFall = false;            // �ٴڿ� ��Ҵ��� �÷���
    float fadeTime = 0.5f;          // ���̵� �ƿ� ����ð�

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
            // ������ ���� Ȯ���� ����� ������Ʈ�̸� ����
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
