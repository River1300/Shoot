using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   // [1] Player Move : 필요 속성(속도, 방향, 위치)
    public float speed;
    // [2] Boarder : 필요 속성(상/하/좌/우 경계선에 닿았는지 알려줄 bool)
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    // [3] Animation : 필요 속성(애니매이터)
    Animator anim;
    // [4] Bullet : 필요 속성(프리팹을 담을 게임 오브젝트)
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    // [5] Fire Update : 필요 속성(총알 발사 시간, 현재 발사 시간)
    public float maxShotDelay;
    public float curShotDelay;
    // [6] Power : 필요 속성(파워를 수치화 할 변수)
    public int power;
    // [11] Player Hit : 3) 플레이어가 비활성화 되기전에 게임 매니저의 함수를 호출해야 한다.
    public GameManager manager;
    // [12] UI Setting : 필요 속성(플레이어의 목숨, 점수) -> Enemy
    public int life;
    public int score;
    // [14] OnHit Bug : 필요 속성(현재 피격 상태인지 확인할 bool)
    public bool isHit;
    // [16] Item : 필요 속성(최대 파워 값)
    public int maxPower;
    // [17] BoomEffect : 필요 속성(폭탄 이펙트를 받을 오브젝트)
    public GameObject boomEffect;
    // [18] Input.Boom : 필요 속성(현재 폭탄 갯수, 최대 폭탄 갯수, 폭탄 사용 중? bool)
    public int boom;
    public int maxBoom;
    public bool isBoomTime;
    // [23] Object pool : 필요 속성(오브젝트 매니저)
    public ObjectManager objectManager;
    // [30] Sub Follow : 필요 속성(보조 무기 오브젝트를 담을 게임 오브젝트 배열)
    public GameObject[] followerObject;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        Move();
        Fire();
        Boom();
        Reload();
    }

    // [4] Bullet : 1) Update() 함수 정리를 위해 이동 로직을 캡슐화
    void Move()
    {   // [1] Player Move : 1) 수직/수평 입력값을 받아서 저장한 뒤 방향으로 사용
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // [2] Boarder : 2) 이동 값이 있고 bool 변수가 true라면 이동 값은 0이 된다.
        if((h == 1 && isTouchRight) || (h == -1 && isTouchLeft)) h = 0;
        if((v == 1 && isTouchTop) || (v == -1 && isTouchBottom)) v = 0;

        // [1] Player Move : 2) 미래의 위치는 전달받은 방향으로 속도와 시간을 곱한 값
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        // [1] Player Move : 3) 현재 위치에 미래의 위치를 더하여 이동
        transform.position = curPos + nextPos;

        // [3] Animation : 1) 수평방향 키 버튼이 눌렸다면? 수평방향 키 버튼이 때였다면? 수평 값을 전달한다.
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) anim.SetInteger("Input", (int)h);
    }

    // [4] Bullet : 2) 발사 함수에서 발사 로직을 작성한다.
    void Fire()
    {   // [5] Fire Update : 1) 사용자가 발사 버튼을 누르지 않았다면 총알은 발사되지 않는다.
        if(!Input.GetButton("Fire1")) return;
        // [5] Fire Update : 3) 아직 발사 시간에 도달하지 못하였다면 총알은 발사되지 않는다.
        if(curShotDelay < maxShotDelay) return;

        // [6] Power : 1) switch문을 활용하여 파워에 따라 각기 다른 방식으로 총알을 발사한다.
        switch(power)
        {   // [23] Object pool : 6) 객체를 받고 위치를 조정해 준다. -> Enemy
            case 1:
                // [4] Bullet : 3) 먼저 총알A부터 발사해 본다. 공장에서 객체를 만든다.
                GameObject bullet = objectManager.MakeObj("PlayerBulletA");
                bullet.transform.position = transform.position;
                // [4] Bullet : 4) 객체가 만들어 졌다면 해당 객체로 부터 Rigidbody2D 컴포넌트를 받아와 발사한다.
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                // [6] Power : 2) 파워가 2단계일 때는 총알을 두 발씩 발사한다. 이때 총알의 위치가 곂치지 않도록 생성할 때 위치를 조정해 준다.
                GameObject bulletL = objectManager.MakeObj("PlayerBulletA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                GameObject bulletR = objectManager.MakeObj("PlayerBulletA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            // [30] Sub Follow : 3) 3 이상의 파워는 세 발로 고정한다. -> Bullet
            default:
                // [6] Power : 3) 파워가 3단계일 때는 총알 두발에 + 큰 총알을 발사한다. 객체를 생성할 때 다른 프리팹을 사용한다.
                GameObject bulletLL = objectManager.MakeObj("PlayerBulletA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;
                GameObject bulletCC = objectManager.MakeObj("PlayerBulletB");
                bulletCC.transform.position = transform.position;
                GameObject bulletRR = objectManager.MakeObj("PlayerBulletA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;

                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;   
        }

        // [5] Fire Update : 4) 총알을 발사 하였다면 총알 발사 시간을 초기화 한다.
        curShotDelay = 0;
    }

    // [18] Input.Boom : 1) 폭탄 발사 함수를 만든다.
    void Boom()
    {   // [18] Input.Boom : 2) 발사 버튼, 발사 중, 갯수가 0 에 대한 제어문을 만든다.
        if(!Input.GetButton("Fire2") || isBoomTime || boom == 0) return;
        // [18] Input.Boom : 3) 사용 했으므로 폭탄 갯수를 줄인다. 그리고 폭탄 사용 중 임을 체크
        boom--;
        isBoomTime = true;
        // [18] Input.Boom : 9) 폭탄을 새로 그린다. -> Enemy
        manager.UpdateBoomIcon(boom);
        // [18] Input.Boom : 4) switch 문의 폭탄 기능을 이쪽으로 옮긴다.
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        // [23] Object pool : 13) 배열로 적 객채를 모두 받아온다.
        GameObject[] enemyS = objectManager.GetPool("EnemyS");
        GameObject[] enemyM = objectManager.GetPool("EnemyM");
        GameObject[] enemyL = objectManager.GetPool("EnemyL");
        for (int index = 0; index < enemyS.Length; index++)
        {   // [23] Object pool : 14) 활성화된 객체에게만 데미지를 준다.
            if(enemyS[index].activeSelf)
            {
                Enemy enemyLogic = enemyS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            if(enemyM[index].activeSelf)
            {
                Enemy enemyLogic = enemyM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemyL.Length; index++)
        {
            if(enemyL[index].activeSelf)
            {
                Enemy enemyLogic = enemyL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        // [23] Object pool : 15) 적 총알도 같은 방식으로 진행한다. -> GameManager
        GameObject[] bulletA = objectManager.GetPool("EnemyBulletA");
        GameObject[] bulletB = objectManager.GetPool("EnemyBulletB");
        for (int index = 0; index < bulletA.Length; index++)
        {
            if(bulletA[index].activeSelf)
            {
                bulletA[index].SetActive(false);
            }
        }
        for (int index = 0; index < bulletB.Length; index++)
        {
            if(bulletB[index].activeSelf)
            {
                bulletB[index].SetActive(false);
            }
        }
    }

    // [5] Fire Update : 2) 매 프래임 마다 총알 발사 시간의 값이 증가한다.
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    // [17] BoomEffect : 6) 폭탄 이펙트를 비활성화 시키는 함수를 만든다.
    void OffBoomEffect()
    {   // [18] Input.Boom : 6) 폭탄 사용 중이 아님을 체크 한다. -> GameManager
        isBoomTime = false;

        boomEffect.SetActive(false);
    }

    // [30] Sub Follow : 1) 보조 무기를 추가하는 함수를 만든다.
    void AddFollower()
    {
        if(power == 4)
            followerObject[0].SetActive(true);
        else if(power == 5)
            followerObject[1].SetActive(true);
        else if(power == 6)
            followerObject[2].SetActive(true);
    }

    // [2] Boarder : 1) 경계선에 닿았다면 bool 값을 true로 배정한다.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boarder")
        {
            switch(other.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
        {   // [14] OnHit Bug : 1) 현재 피격된 상태라면 아래 로직을 실행하지 않는다.
            if(isHit) return;
            // [14] OnHit Bug : 2) 현재 피격된 상태 전이라면 아래 로직을 실행하고 true를 준다. -> GameManager
            isHit = true;
            
            // [13] UI On : 3) 플레이어의 목숨 값을 -1 하고 게임 매니저의 목숨 이미지 함수를 호출한다. -> GameManager
            life--;
            // [13] UI On : 5) 피격될 때 게임 매니저에게 목숨아이콘을 하나 없에 달라고 요청한다. -> GameManager
            manager.UpdateLifeIcon(life);

            if (life <= 0)
            {   // [13] UI On : 7) 만약 목숨이 0이 된다면 게임 오버 화면을 띄운다. -> GameManager
                manager.GameOver();
            }
            else
            {
                // [11] Player Hit : 4) 게임 매니저의 부활 함수를 호출한다.
                manager.RespawnPlayer();
            }
            // [11] Player Hit : 1) 플레이어가 적, 적의 총알에 충돌하면 비활성화 된다. 충돌한 오브젝트는 제거된다. -> GameManager
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "Item")
        {   // [16] Item : 1) 충돌한 아이템 오브젝트로 부터 Item 로직을 받아 이름을 확인한다.
            Item item = other.gameObject.GetComponent<Item>();
            // [16] Item : 2) switch 문에서 이름에 따라 각기 다른 로직을 작성한다.
            switch(item.type)
            {
                case "Coin":
                    score += 100;
                break;
                case "Power":
                    if(power == maxPower)
                    {
                        score += 50;
                    }
                    else
                    {
                        power++;
                        // [30] Sub Follow : 2) 보조 무기 소환 함수를 호출한다.
                        AddFollower();
                    }
                break;
                case "Boom":
                    // [18] Input.Boom : 5) 폭탄 아이템을 먹으면 폭탄 갯수가 증가한다.
                    if(boom == maxBoom)
                    {
                        score += 50;
                    }
                    else
                    {
                        boom++;
                        // [18] Input.Boom : 8) 폭탄을 새로 그린다.
                        manager.UpdateBoomIcon(boom);
                    }
                break;
            }
            // [17] BoomEffect : 8) 충돌한 아이템은 삭제한다.
            other.gameObject.SetActive(false);
        }
    }
    // [2] Boarder : 3) 플레이어가 경계선을 벗어 났다면 bool 에 false를 배정하여 이동을 정상화 한다.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boarder")
        {
            switch(other.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
