using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MainController {

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    private Color yellow, blue, red, purple, green;
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
    public bool isDead= false;
    public string move;
    public PauseManager pm;
    public bool inWindZone = false;
    public GameObject windZone;

    // Use this for initialization
    void Start()
    {
        pm = gameObject.GetComponent<PauseManager>();
        curhp = maxHealth;
        curhealth = maxHealth;
        moveSpeed = 7f;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        yellow = new Color();
        red = new Color(1f, 0f, 0f);
        blue = new Color(0f, 0f, 1f);
        green = new Color(0f, 1f, 0f);
        purple = new Color(1f, 0f, 1f);
        sr.color = purple;
        bx = GetComponent<BoxCollider2D>();
        weight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        Moving();
        Dead();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DamageSim();
        }
        
        if (isPossesing)
        {
            PossessingEnemy();
            curhealth = hit.collider.gameObject.GetComponent<MoveScript>().curhealth;
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
            curhealth = curhp;
            if (Input.GetKeyDown(possess) && objInSight && !isPossesing)
            {
                hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                hit.collider.gameObject.transform.SetParent(this.transform);
                hit.collider.gameObject.transform.position = this.transform.position;
                sr.enabled = false;
                isPossesing = true;
                bx.enabled = false;
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

        if (inWindZone)
        {
            this.gameObject.transform.Translate(windZone.GetComponent<WindArea>().direction.x, windZone.GetComponent<WindArea>().direction.y,0);
        }
    }

    public void PossessingEnemy()
    {
        hit.collider.gameObject.transform.position = this.transform.position;
    }

    public void Moving()
    {
        if (Input.GetKey(up))
        {
            direction = new Vector3(this.transform.position.x, this.transform.position.y + length, 0);
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            direction = new Vector3(this.transform.position.x, this.transform.position.y + -length, 0);
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + -length, this.transform.position.y, 0);
        }
        if (Input.GetKey(right))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + length, this.transform.position.y, 0);
        }
    }

    public void Unpossess()
    {
        transform.DetachChildren();
        isPossesing = false;
        sr.enabled = true;
        hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
                hit.collider.gameObject.GetComponent<MoveScript>().curhealth -= dmg;
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

    public void DamageSim()
    {
        DamagePlayer(5);
    }

}
