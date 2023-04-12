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

    void Awake()
    {   // [7] Enemy : 1) 컴포넌트를 초기화 하고 속력 값을 초기화 한다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        // [9] Spawn Upgrade : 2) 사이드에서 탄생한 적은 아래로만 내려가서는 않되기 때문에 생성과 함께 속도의 초기화는 이제 필요 없다. -> GameManager
    }

    // [10] Enemy Bullet : 1) 발사 함수와 재장선 함수를 매 프레임마다 호출한다.
    void Update()
    {   
        Fire();
        Reload();
    }

    void Fire()
    {   
        if(curShotDelay < maxShotDelay) return;

        // [10] Enemy Bullet : 2) 총알은 S타입의 적과 L타입의 적만이 발사 한다. -> GameManager
        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            // [10] Enemy Bullet : 5) 방향 벡터에 플레이어로 향하는 방향을 저장한다.
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {   // [10] Enemy Bullet : 6) L타입은 두발의 다른 총알을 발사 한다.
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);

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
    void OnHit(int dmg)
    {   // [7] Enemy : 3) 현재 체력에서 데미지를 빼기 연산한다.
        health -= dmg;
        // [7] Enemy : 5) 피격 당했다면 스프라이트 렌더러를 이용하여 스프라이트를 교체한다.
        spriteRenderer.sprite = sprites[1];
        // [7] Enemy : 7) Invoke()를 통해 스프라이트를 되돌린다.
        Invoke("ReturnSprite", 0.2f);
        // [7] Enemy : 4) 만약 체력이 0 이하로 떨어지면 오브젝트는 파괴된다.
        if (health <= 0) 
        {   // [13] UI On : 1) 플레이어의 로직에 접근하여 점수를 + 연산한다. -> GameManager
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            Destroy(gameObject);
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
        if(other.gameObject.tag == "BoarderBullet") Destroy(gameObject);
        // [7] Enemy : 10) 충돌한 콜라이더가 플레이어 총알이라면?
        if(other.gameObject.tag == "PlayerBullet")
        {   // [7] Enemy : 11) OnHit()함수에 데미지를 매개변수로 전달하기 위해 Bullet 스크립트에 속성을 추가해 준다. -> Bullet
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            // [7] Enemy : 13) 총을 관통형이 아니기 때문에 제거한다. -> GameManager
            Destroy(other.gameObject);
        }
    }
}
