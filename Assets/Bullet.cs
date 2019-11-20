using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Damage;
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
            playerController.StartCoroutine(playerController.GetHit());
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged")
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject);

        }
    }
}
