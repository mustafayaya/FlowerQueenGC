using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float health = 5;
    public float horizontal
    {
        get { return Input.GetAxis("Horizontal"); }
    }
    public float vertical
    {
        get { return Input.GetAxis("Vertical"); }
    }
    public float Space
    {
        get { return Input.GetAxis("Space"); }
    }
    public float MovementSpeed = 1f;
    public float JumpForce = 2f;
    private float rotation = 1;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;

    public Text healthText;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        line = GetComponent<LineRenderer>();
            }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animator.SetFloat("speed",horizontal);
        Rotation();
        Jump();
        UIHandler();
        Attack();
    }

    private void UIHandler()
    {
        healthText.text = health.ToString();
    }

    private void Movement()
    {
        if(horizontal > 0.1f || horizontal < 0.1f)
        {
            transform.Translate(new Vector2(MovementSpeed * 0.5f * horizontal *-Mathf.Sign(rotation),0 ));
        }
    }

    float jumpCooldown = 2f;
    private float attackCooldown;

    private void Jump()
    {
        jumpCooldown -= Time.deltaTime;
        if (vertical > 0.1f)
        {
            if (jumpCooldown < 0)
            {
                jumpCooldown = 2;
                Rigidbody2D.AddForce(new Vector2(0,JumpForce*200),ForceMode2D.Force);
            }
        }
    }

    private void Rotation()
    {
        if (horizontal > 0.1f || horizontal < 0.1f)
        {
            rotation = horizontal / horizontal * Mathf.Sign(horizontal); ;
        }

        if (rotation == -1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (rotation == 1)
        {
            transform.rotation = new Quaternion(0, 180, 0,0);
        }
    }
    public float damage = 2;
    public GameObject attackPrefab;
    public void Attack()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        attackCooldown -= Time.deltaTime;

        if (attackCooldown < 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attackCooldown = 3;
                GameObject go = (GameObject)GameObject.Instantiate(attackPrefab);
                go.transform.position = transform.position;
                go.GetComponent<Bullet>().Damage = damage;
                go.GetComponent<Bullet>().speed = (transform.position - transform.right * 50) * -0.025f;
            }
        

        }

    }
}
