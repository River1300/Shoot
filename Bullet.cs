using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   // [7] Enemy : 12) public으로 변수를 생성하여 총알의 종류마다 각기 다른 데미지를 설정해 준다. -> Enemy
    public int dmg;
    // [31] BossBullet : 필요 속성(회전하는 총알을 확인할 플래그)
    public bool isRotate;

    void Update()
    {   // [31] BossBullet : 1) 이 총알이 회전하는 총알 이라면 회전 시킨다. -> Enemy
        if(isRotate)
            transform.Rotate(Vector3.forward * 10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BoarderBullet") gameObject.SetActive(false);
    }
}
