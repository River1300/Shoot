using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;
    // [21] Scrolling : 필요 속성(배경 스프라이트의 위치를 담을 배열, 시작 인덱스, 끝 인덱스, 카메라 size)
    public Transform[] sprites;
    public int startIndex;
    public int endIndex;
    float viewHeight;

    void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {   
        Move();
        Scrolling();
    }

    void Move()
    {   // [20] Background Move : 1) 배경을 매 프레임 당 아래로 내린다.
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {   // [21] Scrolling : 1) 가장 밑에 있는 인덱스의 위치가 카메라 제한 사이즈 밑까지 넘어갈 경우 스프라이트의 위치를 바꾼다.
        if(sprites[endIndex].position.y < viewHeight * (-1))
        {   // [21] Scrolling : 2) 스프라이트의 위치를 바꾸기 위해서는 먼저 두 개의 위치값을 알아야 한다.
            Vector3 upSpritePos = sprites[startIndex].localPosition;
            Vector3 downSpritePos = sprites[endIndex].localPosition;
            // [21] Scrolling : 3) 밑에 있는 스프라이트를 위에 있는 스프라이트, 위에 붙인다.
            //                : 원래 위치 2,1,0 => 바뀐 위치 0,2,1 그러면 0이 startIndex가 되야 하고 1이 endIndex가 되어야 한다.
            sprites[endIndex].transform.localPosition = upSpritePos + Vector3.up * viewHeight;
            // [21] Scrolling : 4) 시작 인덱스와 끝 인덱스에 들어갈 값을 바꿔 준다. -> ObjectManager
            int tempIndex = (startIndex - 1 < 0) ? sprites.Length - 1 : startIndex - 1;
            startIndex = endIndex;
            endIndex = tempIndex;
        }
    }
}
