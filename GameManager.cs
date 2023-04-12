using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   // [8] Enemy Spawn : 필요 속성(적 공장, 소환 위치, 소환 딜레이, 소환 시간)
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;
    // [10] Enemy Bullet : 3) 프리팹은 객체를 받아올 수 없으므로 게임 매니저에서 인스턴스화 될 때 플레이어 오브젝트를 넘겨준다.
    public GameObject player;
    // [13] UI On : 필요 속성(Text UI, Image UI, Over set UI)
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;

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

        // [13] UI On : 2) 매 프레임마다 플레이어의 점수를 출력한다.
        //            : 문자열 포맷으로 구분자를 넣어 text에 집어 넣는다. -> Player
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
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

    // [13] UI On : 4) 플레이어의 현재 목숨을 Image UI로 그리는 함수를 만든다. -> Player
    public void UpdateLifeIcon(int life)
    {
        for(int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        for(int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    // [11] Player Hit : 2) 비활성화된 플레이어를 딜레이를 두고 다시 활성화 시킨다. -> Player
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        // [14] OnHit Bug : 3) 플레이어 로직으로 부터 bool 속성을 받아와 false를 준다. -> Item
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    // [13] UI On : 6) 게임 오버 오브젝트를 활성화 시킨다. -> Player
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    // [13] UI On : 8) 재시작 버튼을 누르면 씬을 새로 불러온다. -> Player
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
