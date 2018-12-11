using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{

    Spider spider;

    private void Start()
    {
        spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        //Debug.Log("Spider fires");

        spider.Attack();
    }

}
