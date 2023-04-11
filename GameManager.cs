using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   // [8] Enemy Spawn : 필요 속성(적 공장, 소환 위치, 소환 딜레이, 소환 시간)
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;
    // [10] Enemy Bullet : 3) 프리팹은 객체를 받아올 수 없으므로 게임 매니저에서 인스턴스화 될 때 플레이어 오브젝트를 넘겨준다.
    public GameObject player;

    void Update()
    {   // [8] Enemy Spawn : 1) 적을 소환하기 위해 소환 시간은 매 프레임마다 증가한다.
        curSpawnDelay += Time.deltaTime;
        // [8] Enemy Spawn : 2) 딜레이 시간이 찼다면 적을 소환한다.
        if(curSpawnDelay >= maxSpawnDelay)
        {
            SpawnEnemy();
            // [8] Enemy Spawn : 3) 적을 소환 하였다면 딜레이는 0으로 초기화 하고, 소환 시간은 랜덤 초기화 한다.
            curSpawnDelay = 0;
            maxSpawnDelay = Random.Range(0.5f, 3f);
        }

    }

    void SpawnEnemy()
    {   // [8] Enemy Spawn : 4) 랜덤한 적을 랜덤한 위치에 생성한다.
        int ranEnemy = Random.Range(0, 3);

        // [9] Spawn Upgrade : 1) 사이드 위치를 추가하여 랜덤 위치값을 조정한다. -> Enemy
        int ranPoint = Random.Range(0, 9);
        // [9] Spawn Upgrade : 3) 적을 생성할 때 저장하고 리지드바디를 받아와 방향을 지정해 준다.
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        // [9] Spawn Upgrade : 4) 속도에는 속력이 필요함으로 Enemy 스크립트도 받아온다.
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        // [10] Enemy Bullet : 4) 인스턴스화가 되면서 Enemy 컴포넌트를 변수로 받아오게되는데 이때 player를 넘겨준다. -> Enemy
        enemyLogic.player = player;

        // [9] Spawn Upgrade : 5) ranPoint에 따라서 방향과 속도를 지정해준다. -> Enemy
        if(ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if(ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
}
