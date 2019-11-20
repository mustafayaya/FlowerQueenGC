using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float Damage;
    public Vector3 speed;

    public void Update()
    {
        transform.Translate(speed);
    }

    public void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("PlayerController");

            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.health -= Damage;
            Destroy(this.gameObject);
        }
    }
}
