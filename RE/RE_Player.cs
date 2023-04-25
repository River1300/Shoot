using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RE_Player : MonoBehaviour
{
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;

    public RE_GameManager manager;

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

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Boom()
    {
        if(!Input.GetButton("Fire2") || isBoomTime || boom == 0) return;
        boom--;
        isBoomTime = true;

        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            RE_Enemy enemyLogic = enemies[i].GetComponent<RE_Enemy>();
            enemyLogic.OnHit(5000);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for(int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
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

        if ((h == -1 && isTouchLeft) || (h == 1 && isTouchRight)) h = 0;
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom)) v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if(Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) anim.SetInteger("Input", (int)h);
    }

    void Fire()
    {
        if(!Input.GetButton("Fire1")) return;
        if(curShotDelay < maxShotDelay) return;

        switch(power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
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
            if(isHit) return;
            isHit = true;

            life--;
            manager.UpdateLifeIcon(life);

            if(life < 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }

            Destroy(gameObject);
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
                        power++;
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
            Destroy(other.gameObject);
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