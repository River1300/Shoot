using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   // [7] Enemy : 필요 속성(속도, 체력, 스프라이트 배열, 스프라이트 렌더러, 리지드바디)
    public float speed;
    public int health;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {   // [7] Enemy : 1) 컴포넌트를 초기화 하고 속력 값을 초기화 한다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
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
        if (health <= 0) Destroy(gameObject);
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
