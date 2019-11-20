using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<flowerbullet>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

    float leftTime; 

    public void Awake()
    {
        leftTime = 2;
    }

    public void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime < 0)
        {
            gameObject.SetActive(false);
            leftTime = 2;

        }
    }
}
