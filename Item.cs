using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{   // [15] Item Set : 필요 속성(아이템 이름, 리지드바디) -> Player
    public string type;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // [23] Object pool : 10) 아이템이 활성화 될 때마다 움직이도록 활성화 함수를 만든다. -> Enemy
    void OnEnable()
    {
        rigid.velocity = Vector2.down * 1.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BoarderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
