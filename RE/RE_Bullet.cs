using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RE_Bullet : MonoBehaviour
{
    public int dmg;

    public bool isRotate;

    void Update()
    {
        if(isRotate)
            transform.Rotate(Vector3.forward * 10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "BoarderBullet")
        {
            gameObject.SetActive(false);
        }
    }
}