using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{   // [22] ObjectManager : 필요 속성(모든 공장, 공장의 인스턴스를 담을 배열들)
    public GameObject enemyBPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject playerBulletAPrefab;
    public GameObject playerBulletBPrefab;
    public GameObject enemyBulletAPrefab;
    public GameObject enemyBulletBPrefab;
    public GameObject followerBulletPrefab;
    public GameObject bossBulletAPrefab;
    public GameObject bossBulletBPrefab;
    public GameObject explosionPrefab;
    GameObject[] enemyB;
    GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;
    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;
    GameObject[] playerBulletA;
    GameObject[] playerBulletB;
    GameObject[] enemyBulletA;
    GameObject[] enemyBulletB;
    GameObject[] followerBullet;
    GameObject[] bossBulletA;
    GameObject[] bossBulletB;
    GameObject[] explosion;
    // [23] Object pool : 필요 속성(임시 객체 저장소)
    GameObject[] targetPool;

    // [22] ObjectManager : 1) 배열을 초기화 해준다.
    void Awake()
    {
        enemyB = new GameObject[3];
        enemyS = new GameObject[20];
        enemyM = new GameObject[10];
        enemyL = new GameObject[10];
        itemCoin = new GameObject[10];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];
        playerBulletA = new GameObject[100];
        playerBulletB = new GameObject[100];
        enemyBulletA = new GameObject[100];
        enemyBulletB = new GameObject[100];
        followerBullet = new GameObject[100];
        bossBulletA = new GameObject[100];
        bossBulletB = new GameObject[1000];
        explosion = new GameObject[50];

        Generate();
    }
    // [22] ObjectManager : 2) 배열의 길이만큼 인스턴스를 채워준다.
    void Generate()
    {
        for(int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }
        for(int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }
        for(int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }
        for(int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }

        for(int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }
        for(int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
        for(int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        for(int index = 0; index < playerBulletA.Length; index++)
        {
            playerBulletA[index] = Instantiate(playerBulletAPrefab);
            playerBulletA[index].SetActive(false);
        }
        for(int index = 0; index < playerBulletB.Length; index++)
        {
            playerBulletB[index] = Instantiate(playerBulletBPrefab);
            playerBulletB[index].SetActive(false);
        }
        for(int index = 0; index < enemyBulletA.Length; index++)
        {
            enemyBulletA[index] = Instantiate(enemyBulletAPrefab);
            enemyBulletA[index].SetActive(false);
        }
        for(int index = 0; index < enemyBulletB.Length; index++)
        {
            enemyBulletB[index] = Instantiate(enemyBulletBPrefab);
            enemyBulletB[index].SetActive(false);
        }
        for(int index = 0; index < followerBullet.Length; index++)
        {
            followerBullet[index] = Instantiate(followerBulletPrefab);
            followerBullet[index].SetActive(false);
        }
        for(int index = 0; index < bossBulletA.Length; index++)
        {
            bossBulletA[index] = Instantiate(bossBulletAPrefab);
            bossBulletA[index].SetActive(false);
        }
        for(int index = 0; index < bossBulletB.Length; index++)
        {
            bossBulletB[index] = Instantiate(bossBulletBPrefab);
            bossBulletB[index].SetActive(false);
        }
        for(int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    // [23] Object pool : 1) 외부에서 객체를 받을 수 있는 public 함수를 만든다.
    public GameObject MakeObj(string type)
    {
        // [23] Object pool : 2) 객체의 종류가 많으니 switch문으로 구분하여 객체를 반환한다.
        switch(type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "PlayerBulletA":
                targetPool = playerBulletA;
                break;
            case "PlayerBulletB":
                targetPool = playerBulletB;
                break;
            case "EnemyBulletA":
                targetPool = enemyBulletA;
                break;
            case "EnemyBulletB":
                targetPool = enemyBulletB;
                break;
            case "FollowerBullet":
                targetPool = followerBullet;
                break;
            case "BossBulletA":
                targetPool = bossBulletA;
                break;
            case "BossBulletB":
                targetPool = bossBulletB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;
        }
        // [23] Object pool : 3) 비활성화 되어 있는 객체를 골라서 활성화 한 뒤 반환 한다. -> GameManager
        for(int index = 0; index < targetPool.Length; index++)
        {
            if(!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    // [23] Object pool : 12) 해당 오브젝트 배열을 통째로 넘기는 함수를 만든다. -> Player
    public GameObject[] GetPool(string type)
    {
        switch(type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "PlayerBulletA":
                targetPool = playerBulletA;
                break;
            case "PlayerBulletB":
                targetPool = playerBulletB;
                break;
            case "EnemyBulletA":
                targetPool = enemyBulletA;
                break;
            case "EnemyBulletB":
                targetPool = enemyBulletB;
                break;
            case "FollowerBullet":
                targetPool = followerBullet;
                break;
            case "BossBulletA":
                targetPool = bossBulletA;
                break;
            case "BossBulletB":
                targetPool = bossBulletB;
                break;
            case "Explosion":
                targetPool = explosion;
                break;
        }
        return targetPool;
    }
}
