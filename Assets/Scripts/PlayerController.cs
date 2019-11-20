using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int health = 5;
    public float mana = 10;

    public float manaRecover = 1;
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

    private bool isGrounded;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;

    public Text healthText;
    float _mana;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        _mana = mana;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
            
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
        ManaRecover();
        Shield();
            }

    public Slider manaSlider;

    public void ManaRecover()
    {
        _mana = Mathf.Clamp(_mana + manaRecover * Time.deltaTime,0,mana);
    
    }

    private void UIHandler()
    {
        healthText.text = health.ToString();
        manaSlider.value = _mana;
        manaSlider.minValue = 0;
        manaSlider.maxValue = mana;
    }

    private void Movement()
    {
        if(horizontal > 0.1f || horizontal < 0.1f)
        {
            transform.Translate(new Vector2(MovementSpeed * 0.5f * horizontal *-Mathf.Sign(rotation),0 ));
        }
    }

    float jumpCooldown = 0.8f;
    private float attackCooldown;

    public void OnCollisionStay2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

     public void OnCollisionExit2D(Collision2D collision2D)
    {

        if (collision2D.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        jumpCooldown -= Time.deltaTime;
        if (vertical > 0.1f)
        {
            if (jumpCooldown < 0 && isGrounded)
            {
        Animator.SetTrigger("jump");
                jumpCooldown = 0.8f;
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
            if (Input.GetKeyDown(KeyCode.Space) && _mana > 2)
            {
                attackCooldown = .25f;
                Animator.SetTrigger("attack");

                GameObject go = (GameObject)GameObject.Instantiate(attackPrefab);
                go.transform.position = transform.position;
                go.GetComponent<flowerbullet>().Damage = damage;
                go.GetComponent<flowerbullet>().speed = (transform.right * 20) * -0.025f;
                _mana -= 2;
            }
        

        }

    }

    public GameObject shield;
    private float shieldCooldown;

    public void Shield()
    {


        shieldCooldown -= Time.deltaTime;

        if (shieldCooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && _mana > 4)
            {
                shieldCooldown = .5f;

                shield.SetActive(true);
                _mana -= 4;
            }


        }

    }


   public IEnumerator GetHit()
    {
        var sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        Animator.SetTrigger("gethit");
        foreach (SpriteRenderer s in sprites)
        {
            s.color = Color.red;

        }
        yield return new WaitForSeconds(0.2f);
        foreach (SpriteRenderer s in sprites)
        {
            s.color = Color.white;

        }

    }
}
