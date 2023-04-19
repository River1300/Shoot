using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{   // [28] Sub Weapon : 필요 속성(총알 발사 시간, 오브젝트 매니저)
    public float maxShotDelay;
    public float curShotDelay;
    public ObjectManager objectManager;

    // [28] Sub Weapon : 1) 이동, 발사, 재장전 함수가 필요하다.
    void Update()
    {   
        Move();
        Fire();
        Reload();
    }

    void Move()
    {

    }

    void Fire()
    {
        if(!Input.GetButton("Fire1")) return;
        if(curShotDelay < maxShotDelay) return;
        // [28] Sub Weapon : 2) 오브젝트 매니저로 부터 총알을 받는다.
        GameObject bullet = objectManager.MakeObj("FollowerBullet");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

}
