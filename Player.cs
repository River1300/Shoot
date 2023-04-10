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

    void Update()
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
