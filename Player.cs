using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   // [1] Player Move : 필요 속성(속도, 방향, 위치)
    public float speed;

    void Update()
    {   // [1] Player Move : 1) 수직/수평 입력값을 받아서 저장한 뒤 방향으로 사용
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // [1] Player Move : 2) 미래의 위치는 전달받은 방향으로 속도와 시간을 곱한 값
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        // [1] Player Move : 3) 현재 위치에 미래의 위치를 더하여 이동
        transform.position = curPos * nextPos;
    }
}
