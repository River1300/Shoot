using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{   // [28] Sub Weapon : 필요 속성(총알 발사 시간, 오브젝트 매니저)
    public float maxShotDelay;
    public float curShotDelay;
    public ObjectManager objectManager;
    // [29] Sub Follow : 필요 속성(따라 다닐 위치값, 플레이어의 위치값, 위치값을 저장할 큐, 시간 차)
    public Vector3 followerPos;
    public Transform playerPos;
    public Queue<Vector3> parentPos;
    public int followDelay;

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    // [28] Sub Weapon : 1) 이동, 발사, 재장전 함수가 필요하다.
    void Update()
    {   
        Watch();
        Follow();
        Fire();
        Reload();
    }

    // [29] Sub Follow : 2) 매 프레임 마다 플레이어의 위치를 추적하는 함수를 만든다.
    void Watch()
    {
        // [29] Sub Follow : 5) 이미 큐에 같은 플레이어의 위치 값이 있다면 그 값은 넣지 않는다.
        if(!parentPos.Contains(playerPos.position))
        {   // [29] Sub Follow : 3) 큐에 플레이어의 위치를 매 프레임 마다 넣는다.
            parentPos.Enqueue(playerPos.position);
        }
        
        // [29] Sub Follow : 4) 큐에 저장되어 있는 위치 값의 갯수가 시간 차의 값보다 클 때 보조 무기에 위치값을 전달한다.
        if(parentPos.Count > followDelay)
        {
            followerPos = parentPos.Dequeue();
        }
        else if(parentPos.Count < followDelay)
        {   // [29] Sub Follow : 6) 큐에 저장된 위치 값의 갯수가 크면 Dequeue, 작으면 playerPos, 같으면 멈춤 -> Player
            followerPos = playerPos.position;
        }
    }

    // [29] Sub Follow : 1) 매 프레임 마다 보조 무기의 위치에 따라 다닐 위치를 배정한다.
    void Follow()
    {
        transform.position = followerPos;
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
