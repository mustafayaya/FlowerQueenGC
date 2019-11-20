using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    GameObject player;
    public float attackRange;
    public float attackCooldown = 3f;
    public int health;
    public Object attackPrefab;
    public float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position,transform.position);
        Death();
        if (distance < attackRange)
        {
            Attack();
        }
    }

    public float damage;
    void Death()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void Attack()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown < 0)
        {
            attackCooldown = 1;
            GameObject go = (GameObject)GameObject.Instantiate(attackPrefab);
            go.transform.position = transform.position;
            go.GetComponent<Bullet>().Damage = damage;
            go.GetComponent<Bullet>().speed = ( transform.position - player.transform.position) *attackSpeed *-0.025f;

        }
    }

}
