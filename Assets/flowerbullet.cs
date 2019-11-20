using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerbullet : MonoBehaviour
{

    public float Damage;
    public Vector3 speed;

    public void Update()
    {
        transform.Translate(speed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.GetComponent<bird>() != null)
        {
            Debug.Log("Enemy");

            bird playerController = collision.gameObject.GetComponent<bird>();
            playerController.health -= (int)Damage;
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged")
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject);

        }
    }
}
