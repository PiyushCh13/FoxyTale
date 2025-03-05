using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] float moveDistanceX, time;
    [SerializeField] float moveDistanceY;
    void Start()
    {
        Vector3 distancetoMove = new Vector3 (transform.position.x + moveDistanceX, transform.position.y + moveDistanceY, transform.position.z);
        transform.DOMove(distancetoMove, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = GameObject.Find("Player").transform;
        }
    }
}
