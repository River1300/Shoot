using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RE_Item : MonoBehaviour
{
    public string type;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rigid.velocity = Vector3.down * 1f;
    }
}