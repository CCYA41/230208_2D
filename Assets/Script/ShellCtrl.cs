using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCtrl : MonoBehaviour
{
    public float deleteTime = 3.0f;
    private void Start()
    {
        Destroy(this.gameObject, deleteTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
