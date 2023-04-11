using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   // [8] Enemy Spawn : 필요 속성(적 공장, 소환 위치, 소환 딜레이, 소환 시간)
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;

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
        int ranPoint = Random.Range(0, 5);
        Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
    }
}
