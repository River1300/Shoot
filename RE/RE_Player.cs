using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RE_Player : MonoBehaviour
{
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameObject[] followerObject;

    public RE_GameManager manager;
    public RE_ObjectManager objectManager;

    public int maxPower;
    public int power;
    public int maxBoom;
    public int boom;
    public int life;
    public int score;

    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isHit;
    public bool isBoomTime;
    public bool isRespawnTime;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;
    public bool[] joyControl;

    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Unbeatable();
        Invoke("Unbeatable", 3);
    }

    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime;

        if(isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);

            for(int i = 0; i < followerObject.Length; i++)
            {
                followerObject[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for(int i = 0; i < followerObject.Length; i++)
            {
                followerObject[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void JoyPanel(int type)
    {
        for(int index = 0; index < 9; index++)
        {
            joyControl[index] = (index == type);
        }
    }
    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }

    void Boom()
    {
        if(!isButtonB || isBoomTime || boom == 0) return;
        boom--;
        isBoomTime = true;

        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject[] enemyS = objectManager.GetPool("EnemyS");
        GameObject[] enemyM = objectManager.GetPool("EnemyM");
        GameObject[] enemyL = objectManager.GetPool("EnemyL");
        for (int index = 0; index < enemyS.Length; index++)
        {
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

    void OffBoomEffect()
    {
        isBoomTime = false;
        boomEffect.SetActive(false);
    }

    void Update()
    {
        Move();
        Reload();
        Fire();
        Boom();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(joyControl[0]) { h = -1; v = 1; }
        if(joyControl[1]) { h = 0; v = 1; }
        if(joyControl[2]) { h = 1; v = 1; }
        if(joyControl[3]) { h = -1; v = 0; }
        if(joyControl[4]) { h = 0; v = 0; }
        if(joyControl[5]) { h = 1; v = 0; }
        if(joyControl[6]) { h = -1; v = -1; }
        if(joyControl[7]) { h = 0; v = -1; }
        if(joyControl[8]) { h = 1; v = -1; }

        if((h == 1 && isTouchRight) || (h == -1 && isTouchLeft) || !isControl) h = 0;
        if((v == 1 && isTouchTop) || (v == -1 && isTouchBottom) || !isControl) v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) anim.SetInteger("Input", (int)h);
    }

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonAUp()
    {
        isButtonA = false;
    }

    public void ButtonBDown()
    {
        isButtonB = true;
    }

    void Fire()
    {
        if(!Input.GetButton("Fire1")) return;

        if(!isButtonA) return;

        if(curShotDelay < maxShotDelay) return;

        switch(power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("PlayerBulletA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = objectManager.MakeObj("PlayerBulletA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                GameObject bulletR = objectManager.MakeObj("PlayerBulletA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;

                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default:
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
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void AddFollower()
    {
        if(power == 4)
            followerObject[0].SetActive(true);
        else if(power == 5)
            followerObject[1].SetActive(true);
        else if(power == 6)
            followerObject[2].SetActive(true);
    }

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
        {
            if(isRespawnTime) return;

            if(isHit) return;
            isHit = true;

            life--;
            manager.UpdateLifeIcon(life);

            manager.CallExplosion(transform.position, "P");

            if(life < 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }

            gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "Item")
        {
            RE_Item item = other.gameObject.GetComponent<RE_Item>();

            switch(item.type)
            {
                case "Coin":
                    score += 100;
                    break;
                case "Power":
                    if(power > maxPower)
                        score += 50;
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if(boom > maxBoom)
                    {
                        score += 50;
                    }
                    else
                    {
                        boom++;
                        manager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            other.gameObject.SetActive(false);
        }
    }
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