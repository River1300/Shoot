using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {   // [40] Explosion : 2) 객체가 활성화 된 후에 일정 시간있다 비활성화 시킨다. -> GameManager
        Invoke("Disable", 2f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    // [40] Explosion : 1) 폭발 애니매이션을 출력할 함수를 만든다.
    public void StartExplosion(string type)
    {
        anim.SetTrigger("OnExplosion");

        switch(type)
        {
            case "S":
                transform.localScale = Vector3.one * 0.7f;
                break;
            case "M":
            case "P":
                transform.localScale = Vector3.one * 1f;
                break;
            case "L":
                transform.localScale = Vector3.one * 2f;
                break;
            case "B":
                transform.localScale = Vector3.one * 5f;
                break;
        }
    }
}
