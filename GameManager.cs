using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// [25] File Read : 1) 파일을 읽기 위한 라이브러리 추가
using System.IO;

public class GameManager : MonoBehaviour
{   // [8] Enemy Spawn : 필요 속성(적 공장, 소환 위치, 소환 딜레이, 소환 시간)
    // [23] Object pool : 4) 이제 공장에서 바로 만들지 않고 오브젝트 매니저에 문자열로 전달할 것이기 때문에 문자열 배열로 바꾼다.
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;
    // [10] Enemy Bullet : 3) 프리팹은 객체를 받아올 수 없으므로 게임 매니저에서 인스턴스화 될 때 플레이어 오브젝트를 넘겨준다.
    public GameObject player;
    // [13] UI On : 필요 속성(Text UI, Image UI, Over set UI)
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;
    // [18] Input.Boom : 필요 속성(폭탄 Image UI)
    public Image[] boomImage;
    // [23] Object pool : 필요 속성(오브젝트 매니저 변수)
    public ObjectManager objectManager;
    // [24] File : 필요 속성(구조체 리스트, 리스트 인덱스, 플래그)
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    // [43] Stage : 필요 속성(스테이지 번호)
    public int stage;
    // [44] Stage UI : 필요 속성(텍스트 UI의 애니매이터)
    public Animator startAnim;
    public Animator endAnim;
    // [45] Fade In/Out : 필요 속성(애니매이터, 플레이어 위치)
    public Animator fadeAnim;
    public Transform playerPos;

    void Awake()
    {
        spawnList = new List<Spawn>();

        // [33] Boss Prefab : 1) 보스의 이름을 등록한다.
        enemyObjs = new string[] {"EnemyS", "EnemyM", "EnemyL", "EnemyB"};

        // [26] File End : 4) 게임이 시작될 때 스테이지 파일을 읽도록 한다.
        
        StartStage();
    }

    // [43] Stage : 2) 스테이지 파일을 처음 읽을 때와 다 읽었을 때의 함수를 만든다.
    public void StartStage()
    {   // [44] Stage UI : 1) 트리거를 쏜다.
        startAnim.SetTrigger("On");
        // [44] Stage UI : 2) Text 컴포넌트를 받아와서 스테이지 번호를 입력한다.
        startAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        endAnim.GetComponent<Text>().text = "Stage " + stage + "\nEnd";

        ReadSpawnFile();

        // [45] Fade In/Out : 1) 장막을 들춘다.
        fadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        endAnim.SetTrigger("On");

        stage++;

        fadeAnim.SetTrigger("Out");

        // [45] Fade In/Out : 2) 플레이어 위치를 조정한다. -> Enemy
        player.transform.position = playerPos.position;

        // [45] Fade In/Out : 4) 다음 스테이지가 있다면 스테이지 시작 함수를 호출하고 없다면 게임 오버 함수를 호출한다.
        if(stage > 2)
            Invoke("GameOver", 3);
        else
            Invoke("StartStage", 5);
    }

    // [24] File : 1) 파일을 읽는 함수를 만든다.
    void ReadSpawnFile()
    {   // [24] File : 2) 읽기 전, 초기화 작업을 실행한다.
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // [25] File Read : 2) 메모장을 읽기 위한 변수를 준비한다.
        // [43] Stage : 1) 스테이지 번호를 입력한다.
        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // [26] File End : 1) 파일을 모두 읽을 때 까지 반복한다.
        while(stringReader != null)
        {
            // [25] File Read : 3) 파일을 한 줄씩 읽는다.
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if(line == null) break;

            // [25] File Read : 4) 읽은 데이터를 구조체에 저장한다.
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            // [25] File Read : 5) 저장한 구조체 데이터를 리스트에 넣는다.
            spawnList.Add(spawnData);
        }

        // [26] File End : 2) 모든 파일을 읽었다면 파일을 닫는다.
        stringReader.Close();
        // [26] File End : 3) 가장 먼저 출현하는 적의 시간을 리스트에 저장된 최초 시간으로 배정한다.
        maxSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {   // [8] Enemy Spawn : 1) 적을 소환하기 위해 소환 시간은 매 프레임마다 증가한다.
        curSpawnDelay += Time.deltaTime;
        // [8] Enemy Spawn : 2) 딜레이 시간이 찼다면 적을 소환한다.
        if(curSpawnDelay >= maxSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            // [8] Enemy Spawn : 3) 적을 소환 하였다면 딜레이는 0으로 초기화 하고, 소환 시간은 랜덤 초기화 한다.
            curSpawnDelay = 0;
        }

        // [13] UI On : 2) 매 프레임마다 플레이어의 점수를 출력한다.
        //            : 문자열 포맷으로 구분자를 넣어 text에 집어 넣는다. -> Player
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {   // [27] Enemy File Spawn : 1) 적의 타입을 리스트에서 받아서 오브젝트 매니저에 전달한다.
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {   // [33] Boss Prefab : 2) 보스의 이름으로 객체를 받아온다. -> Enemy
            case "B":
                enemyIndex = 3;
                break;
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }
        // [27] Enemy File Spawn : 2) 소환 위치도 리스트에서 받아온다.
        int ranPoint = spawnList[spawnIndex].point;
        // [9] Spawn Upgrade : 3) 적을 생성할 때 저장하고 리지드바디를 받아와 방향을 지정해 준다.
        // [23] Object pool : 5) 오브젝트 매니저의 함수를 호출한다. -> Player
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        // [9] Spawn Upgrade : 4) 속도에는 속력이 필요함으로 Enemy 스크립트도 받아온다.
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        // [10] Enemy Bullet : 4) 인스턴스화가 되면서 Enemy 컴포넌트를 변수로 받아오게되는데 이때 player를 넘겨준다. -> Enemy
        enemyLogic.player = player;
        // [23] Object pool : 7) 게임 매니저에 있는 오브젝트 매니저를 적 스크립트에 배정해 준다. -> Enemy
        enemyLogic.objectManager = objectManager;

        // [41] Explosion : 9) 게임 매니저 스크립트에 자기 자신을 전달한다.
        enemyLogic.gameManager = this;

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

        // [27] Enemy File Spawn : 3) 적을 생성하였다면 인덱스를 증가 시킨다.
        spawnIndex++;
        // [27] Enemy File Spawn : 4) 인덱스가 리스트의 갯수를 증가 하였는지 확인한다.
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        // [27] Enemy File Spawn : 5) 인덱스를 증가 시킨 뒤에 다음 소환 시간을 배정한다. -> Follower
        maxSpawnDelay = spawnList[spawnIndex].delay;
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

    // [18] Input.Boom : 7) 폭탄을 그린다. -> Player
    public void UpdateBoomIcon(int boom)
    {
        for(int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }
        for(int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
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

    // [40] Explosion : 3) 오브젝트가 죽을 때 호출할 폭발 함수를 만든다.
    public void CallExplosion(Vector3 pos, string type)
    {   // [40] Explosion : 4) 폭발 객체와 폭발 스크립트를 받아온다.
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();
        // [40] Explosion : 5) 매개변수로 죽는 오브젝트의 위치를 받아서 폭발 위치에 배정한다.
        explosion.transform.position = pos;
        // [41] Explosion : 6) 폭발 애니매이션 함수를 호출한다. -> Player
        explosionLogic.StartExplosion(type);
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
