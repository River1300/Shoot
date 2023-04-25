using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RE_Background : MonoBehaviour
{
    public Transform[] sprites;

    public int startIndex;
    public int endIndex;

    public float speed;
    float viewHeight;

    void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if(sprites[endIndex].position.y < viewHeight * (-1))
        {
            Vector3 upSpritePos = sprites[startIndex].localPosition;
            sprites[endIndex].transform.localPosition = upSpritePos + Vector3.up * viewHeight;
            int tempIndex = (startIndex - 1 < 0) ? sprites.Length - 1 : startIndex - 1;
            startIndex = endIndex;
            endIndex = tempIndex;
        }
    }
}