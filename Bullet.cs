using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   // [7] Enemy : 12) public으로 변수를 생성하여 총알의 종류마다 각기 다른 데미지를 설정해 준다. -> Enemy
    public int dmg;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BoarderBullet") gameObject.SetActive(false);
    }
}
