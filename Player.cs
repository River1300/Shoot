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

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {   
        Move();
        Fire();
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
        {
            case 1:
                // [4] Bullet : 3) 먼저 총알A부터 발사해 본다. 공장에서 객체를 만든다.
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                // [4] Bullet : 4) 객체가 만들어 졌다면 해당 객체로 부터 Rigidbody2D 컴포넌트를 받아와 발사한다.
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                // [6] Power : 2) 파워가 2단계일 때는 총알을 두 발씩 발사한다. 이때 총알의 위치가 곂치지 않도록 생성할 때 위치를 조정해 준다.
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                // [6] Power : 3) 파워가 3단계일 때는 총알 두발에 + 큰 총알을 발사한다. 객체를 생성할 때 다른 프리팹을 사용한다.
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

        // [5] Fire Update : 4) 총알을 발사 하였다면 총알 발사 시간을 초기화 한다.
        curShotDelay = 0;
    }

    // [5] Fire Update : 2) 매 프래임 마다 총알 발사 시간의 값이 증가한다.
    void Reload()
    {
        curShotDelay += Time.deltaTime;
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