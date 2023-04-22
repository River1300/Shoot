using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   // [7] Enemy : 필요 속성(속도, 체력, 스프라이트 배열, 스프라이트 렌더러, 리지드바디)
    public float speed;
    public int health;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    // [10] Enemy Bullet : 필요 속성(총알 공장, 발사 딜레이 + 이름 + 플레이어)
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public float maxShotDelay;
    public float curShotDelay;
    public string enemyName;
    public GameObject player;
    // [13] UI On : 필요 속성(적의 점수)
    public int enemyScore;
    // [19] Item Drop : 필요 속성(아이템 공장)
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    // [23] Object pool : 필요 속성(오브젝트 매니저)
    public ObjectManager objectManager;
    // [32] Boss Basic : 필요 속성(애니매이터)
    Animator anim;
    // [34] Boss Logic : 필요 속성(보스의 패턴 번호, 패턴 반복 횟수, 최대 패턴 반복 횟수)
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {   // [7] Enemy : 1) 컴포넌트를 초기화 하고 속력 값을 초기화 한다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        // [9] Spawn Upgrade : 2) 사이드에서 탄생한 적은 아래로만 내려가서는 않되기 때문에 생성과 함께 속도의 초기화는 이제 필요 없다. -> GameManager

        // [32] Boss Basic : 1) 보스일 경우에 애니매이터를 초기화 한다.
        if(enemyName == "B")
            anim = GetComponent<Animator>();
    }

    // [23] Object pool : 11) 적이 활성화 될 때마다 체력을 초기화 한다. -> ObjectManager
    void OnEnable()
    {
        switch(enemyName)
        {   // [34] Boss Logic : 1) 보스의 체력을 초기화 하고 끝까지 내려가지 않도록 멈추는 함수를 호출한다.
            case "B":
                health = 150;
                Invoke("StopMove", 2);
                break;
            case "S":
                health = 1;
                break;
            case "M":
                health = 5;
                break;
            case "L":
                health = 10;
                break;
        }
    }

    // [10] Enemy Bullet : 1) 발사 함수와 재장선 함수를 매 프레임마다 호출한다.
    void Update()
    {   
        // [32] Boss Basic : 2) 보스는 일반적인 방식으로 총을 발사하지 않는다.
        if(enemyName == "B")
            return;

        Fire();
        Reload();
    }

    void StopMove()
    {   // [34] Boss Logic : 2) 비활성화 상태에서 호출되는 것을 맊기 위해 제어한다.
        if(!gameObject.activeSelf) return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        // [34] Boss Logic : 3) 보스의 패턴을 실행하기 위한 생각 함수를 호출한다.
        Invoke("Think", 2);
    }

    void Think()
    {   // [34] Boss Logic : 4) 패턴 번호와 패턴 카운트를 초기화 한다.
        patternIndex = (patternIndex == 3) ? 0 : patternIndex + 1;
        curPatternCount = 0;
        // [34] Boss Logic : 5) switch 문을 통해서 패턴 별 함수를 호출한다.
        switch(patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    // [34] Boss Logic : 6) 각각의 패턴 함수를 만든다.
    void FireForward()
    {   // [35] FireForward : 1) 오브젝트 매니저로부터 총알을 받아오고 위치를 지정한다.
        GameObject bulletL = objectManager.MakeObj("BossBulletA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BossBulletA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;
        GameObject bulletR = objectManager.MakeObj("BossBulletA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BossBulletA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        // [35] FireForward : 2) 리지드바디 컴포넌트를 받는다.
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        // [35] FireForward : 3) 아래 방향으로 발사.
        rigidL.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidR.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 6, ForceMode2D.Impulse);

        // [34] Boss Logic : 7) 모든 패턴은 최대 반복 횟수가 있고 이 반복 횟수를 채울 때까지 재귀한다.
        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 3);
    }
    void FireShot()
    {   // [36] FireShot : 1) 반복문을 통해서 5발의 총알을 플레이어를 향해 발사한다.
        for(int i = 0; i < 5; i++)
        {
            GameObject bullet = objectManager.MakeObj("EnemyBulletB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = player.transform.position - transform.position;
            // [36] FireShot : 2) 플레이어의 방향에서 약간씩 변화를 주어 랜덤한 방향으로 발사 한다.
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {   // [37] FireShot : 1) 회전하는 총알을 받아온다. 회전하기 전에 회전 값을 초기화 시킨다.
        GameObject bullet = objectManager.MakeObj("EnemyBulletA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        // [37] FireShot : 2) 부채꼴 방향을 위해 Mathf.Cos()함수를 사용한다.
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 15 * curPatternCount / maxPatternCount[patternIndex]), -1f);
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        int roundNumA = 30;
        int roundNumB = 40;
        int roundNum = (curPatternCount % 2 == 0) ? roundNumA : roundNumB;
        // [38] FireAround : 1) 반복 문으로 원형 형태의 총알을 발사한다.
        for(int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BossBulletB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            // [38] FireAround : 2) 원형 형태로 발사한다.
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

            // [38] FireAround : 3) 총알 이미지의 방향을 조정해 준다. -> Player
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Fire()
    {   
        if(curShotDelay < maxShotDelay) return;

        // [10] Enemy Bullet : 2) 총알은 S타입의 적과 L타입의 적만이 발사 한다. -> GameManager
        // [23] Object pool : 7) 적의 총알을 오브젝트 매니저에게서 받아온다. -> GameManager
        if(enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("EnemyBulletA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            // [10] Enemy Bullet : 5) 방향 벡터에 플레이어로 향하는 방향을 저장한다.
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {   // [10] Enemy Bullet : 6) L타입은 두발의 다른 총알을 발사 한다.
            GameObject bulletL = objectManager.MakeObj("EnemyBulletB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            GameObject bulletR = objectManager.MakeObj("EnemyBulletB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;

            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            // [10] Enemy Bullet : 7) 방향 값에서도 위치 조정이 필요하다.
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            // [10] Enemy Bullet : 8) 벡터의 크기를 일반화 한다. -> Player
            rigidL.AddForce(dirVecL.normalized * 2, ForceMode2D.Impulse);
            rigidR.AddForce(dirVecR.normalized * 2, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    // [7] Enemy : 2) 적이 플레이어 총알에 피격 당했을 때 호출되는 함수를 만든다. 매개변수로 데미지를 받는다.
    public void OnHit(int dmg)
    {   // [19] Item Drop : 3) 이미 체력이 0보다 작다면 반환한다.
        if(health <= 0) return;
        
        // [7] Enemy : 3) 현재 체력에서 데미지를 빼기 연산한다.
        health -= dmg;

        // [32] Boss Basic : 3) 보스는 피격될 때 애니메이션 피격을 출력한다.
        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            // [7] Enemy : 5) 피격 당했다면 스프라이트 렌더러를 이용하여 스프라이트를 교체한다.
            spriteRenderer.sprite = sprites[1];
            // [7] Enemy : 7) Invoke()를 통해 스프라이트를 되돌린다.
            Invoke("ReturnSprite", 0.2f);
        }

        // [7] Enemy : 4) 만약 체력이 0 이하로 떨어지면 오브젝트는 파괴된다.
        if (health <= 0) 
        {   // [13] UI On : 1) 플레이어의 로직에 접근하여 점수를 + 연산한다. -> GameManager
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            // [19] Item Drop : 1) 아이템이 나올 확률을 지정하기 위해 랜덤 값을 받는다.
            // [32] Boss Basic : 4) 보스는 아이템을 뱉지 않는다.
            int ran = (enemyName == "B") ? 0 : Random.Range(0, 11);
            // [19] Item Drop : 2) 확률에 따라서 아이템을 드랍한다. -> Background
            // [23] Object pool : 8) 아이템 객체를 불러온다.
            if(ran < 3){
                Debug.Log("NoDrop");
            }else if(ran < 5){
                GameObject ItemCoin = objectManager.MakeObj("ItemCoin");
                ItemCoin.transform.position = transform.position;
            }else if(ran < 8){
                GameObject ItemPower = objectManager.MakeObj("ItemPower");
                ItemPower.transform.position = transform.position;
            }else if(ran < 10){
                GameObject ItemBoom = objectManager.MakeObj("ItemBoom");
                ItemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            // [23] Object pool : 9) 적 객체가 비활성화 될 때 방향값을 초기화 시킨다. -> Item
            transform.rotation = Quaternion.identity;
        }
    }
    // [7] Enemy : 6) 스프라이트를 원 상태로 되돌리는 함수를 만든다.
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    // [7] Enemy : 8) 다른 콜라이더와 충돌할 때 호출되는 함수를 만든다.
    void OnTriggerEnter2D(Collider2D other)
    {   // [7] Enemy : 9) 충돌한 콜라이더가 총알 경계선이라면?
        // [32] Boss Basic : 5) 보스는 경계선에 닿아도 제거되지 않는다. -> GameManager
        if(other.gameObject.tag == "BoarderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        // [7] Enemy : 10) 충돌한 콜라이더가 플레이어 총알이라면?
        if(other.gameObject.tag == "PlayerBullet")
        {   // [7] Enemy : 11) OnHit()함수에 데미지를 매개변수로 전달하기 위해 Bullet 스크립트에 속성을 추가해 준다. -> Bullet
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            // [7] Enemy : 13) 총을 관통형이 아니기 때문에 제거한다. -> GameManager
            other.gameObject.SetActive(false);
        }
    }
}
