﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MainController {

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Vector3 direction;
    RaycastHit2D hit;
    public bool objInSight = false;
    public float length = 3f;
    public bool isPossesing;
    public BoxCollider2D bx;
    private float characterSpeed = 7f;
    public KeyCode up, down, left, right, possess, unpossess, atk1, atk2;
    private int hp = 20;
    private int curhp;
    public int tempHp, tempMax;
    public bool isDead= false;
    public string move;
    public PauseManager pm;
    //public bool inWindZone = false;
    //public GameObject windZone;
    public enum Direction {N,E,S,W};
    public Direction dir;
    public int dMod;
    public int Dmg;

    //ghost info
    private int gHP, gMHP;
    private string gATK1, gATK2;
    private float gMoveSpeed;

    //possess info
    private int pHP, pMHP;
    private string pATK1, pATK2;
    private float pMoveSpeed;

    // Use this for initialization
    void Start()
    {
        pm = gameObject.GetComponent<PauseManager>();
        maxHealth = 20;
        curhp = maxHealth;
        moveSpeed = 7f;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
        weight = 1;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(rb.velocity);
           
        Moving();
        Dead();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DamageSim();
        }
        
        if (isPossesing)
        {
            PossessingEnemy();
            if(Input.GetKeyDown(unpossess))
            {
                Unpossess();
            }

            if (Input.GetKeyDown(atk1))
            {
                move = hit.collider.gameObject.GetComponent<MoveScript>().Attack1(hit.collider.gameObject.GetComponent<MoveScript>().at1);
                Debug.Log("Player used: " + hit.collider.gameObject.GetComponent<MoveScript>().Attack1(hit.collider.gameObject.GetComponent<MoveScript>().at1));
            }
            if (Input.GetKeyDown(atk2))
            {
                move = hit.collider.gameObject.GetComponent<MoveScript>().Attack2(hit.collider.gameObject.GetComponent<MoveScript>().at2);
                Debug.Log("Player used: " + hit.collider.gameObject.GetComponent<MoveScript>().Attack2(hit.collider.gameObject.GetComponent<MoveScript>().at2));
            }
        }
        else
        {
            tempHp = curhp;
            tempMax = maxHealth;
            if (Input.GetKeyDown(possess) && objInSight && !isPossesing)
            {
                hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                hit.collider.gameObject.transform.SetParent(this.transform);
                hit.collider.gameObject.transform.position = this.transform.position;
                sr.enabled = false;
                isPossesing = true;
                //bx.enabled = false;
                moveSpeed = hit.collider.gameObject.GetComponent<MoveScript>().moveSpeed;
                curhealth = hit.collider.gameObject.GetComponent<MoveScript>().curhealth;
                weight = hit.collider.gameObject.GetComponent<MoveScript>().weight;
            }
            if(Input.GetKeyDown(atk1))
            {
                move = at1;
                Debug.Log("Player used: " + at1);
            }
            if (Input.GetKeyDown(atk2))
            {
                move = at2;
                Debug.Log("Player used: " + at2);
            }
        }
    }

    public void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, direction, Color.cyan);
        if (Physics2D.Linecast(this.transform.position, direction, 1 << LayerMask.NameToLayer("obj")))
        {
            hit = Physics2D.Linecast(this.transform.position, direction, 1 << LayerMask.NameToLayer("obj"));
            objInSight = true;
        }
        else
        {
            objInSight = false;
        }

        /*if (inWindZone)
        {
            this.gameObject.transform.Translate(windZone.GetComponent<WindArea>().direction.x, windZone.GetComponent<WindArea>().direction.y,0);
        }*/
    }

    public void PossessingEnemy()
    {
        hit.collider.gameObject.transform.position = this.transform.position;
        dMod = hit.collider.gameObject.GetComponent<MoveScript>().DamageMod;
        tempHp = hit.collider.gameObject.GetComponent<MoveScript>().curhealth;
        tempMax = hit.collider.gameObject.GetComponent<MoveScript>().maxHealth;
    }

    public void Moving()
    {
        if (Input.GetKey(up))
        {
            dir = Direction.N;
            direction = new Vector3(this.transform.position.x, this.transform.position.y + length, 0);
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            dir = Direction.S;
            direction = new Vector3(this.transform.position.x, this.transform.position.y + -length, 0);
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            dir = Direction.W;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + -length, this.transform.position.y, 0);
        }
        if (Input.GetKey(right))
        {
            dir = Direction.E;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + length, this.transform.position.y, 0);
        }
    }

    public void Unpossess()
    {
        hit.collider.gameObject.GetComponent<MoveScript>().Detach();
        hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isPossesing = false;
        sr.enabled = true;
        moveSpeed = characterSpeed;
        curhealth = curhp;
        weight = 1;
    }

    public int DamagePlayer(int dmg)
    {
        if (isDead)
        {
            Debug.Log("Game Over");
        }
        else
        {
            if (isPossesing)
            {
                Dmg = dmg - dMod;
                if(Dmg < 1)
                {
                    Dmg = 1;
                }
                hit.collider.gameObject.GetComponent<MoveScript>().curhealth = hit.collider.gameObject.GetComponent<MoveScript>().curhealth -= Dmg;
                Mathf.Clamp(Dmg, 1, Mathf.Infinity);
            }
            else
            {
                curhp -= dmg;
            }
        }

        return curhp;
    }

    public void Dead()
    {
        if(curhp <= 0)
        {
            isDead = true;
            Debug.Log("player died");
        }
        else
        {
            isDead = false;
            Debug.Log("player has not died");
        }

        
    }

    public int DamageModifier(int dmgmod)
    {
        dMod = dmgmod;
        return dmgmod;
    }

    public void DamageSim()
    {
        DamagePlayer(5);
    }

    private void PossessInfo()
    {
        
    }

    private void GhostInfo()
    {
        gMHP = 20;
        gHP = gMHP;
        gMoveSpeed = 7f;
        //gATK1 =
        //gATK2 = 
    }

}
