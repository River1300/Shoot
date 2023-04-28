using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class RE_GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverSet;
    public string[] enemyObjs;
    public List<RE_Spawn> spawnList;

    public Transform[] spawnPoints;

    public int spawnIndex;
    public int stage;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public bool spawnEnd;

    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;

    public RE_ObjectManager objectManager;

    public Animator startAnim;
    public Animator endAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    void Awake()
    {
        spawnList = new List<RE_Spawn>();
        enemyObjs = new string[] {"EnemyS", "EnemyM", "EnemyL", "EnemyB"};

        StartStage();
    }

    public void StartStage()
    {
        startAnim.SetTrigger("On");

        startAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        endAnim.GetComponent<Text>().text = "Stage " + stage + "\nEnd";

        ReadSpawnFile();

        fadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        endAnim.SetTrigger("On");

        stage++;

        fadeAnim.SetTrigger("Out");

        player.transform.position = playerPos.position;

        if(stage > 2)
            Invoke("GameOver", 3);
        else
            Invoke("StartStage", 5);
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnEnd = false;
        spawnIndex = 0;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            if(line == null) break;

            RE_Spawn spawnData = new RE_Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnAdd(spawnData);
        }
        stringReader.Close();
        maxSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
            maxSpawnDelay = Random.Range(0.5f, 3f);
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;

        switch(spawnList[spawnIndex].type)
        {
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
        int ranPoint = Random.Range(0, 9);

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        enemyLogic.gameManager = this;

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

        spawnIndex++;
        if(spanwIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        maxSpawnDelay = spawnList[spawnIndex].delay;
    }

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

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        RE_Player playerLogic = player.GetComponent<RE_Player>();
        playerLogic.isHit = false;
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}