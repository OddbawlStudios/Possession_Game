﻿using System.Collections;
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

    public KeyCode up, down, left, right, possess, unpossess, atk1, atk2;

    // Use this for initialization
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isPossesing)
        {
            hit.collider.gameObject.transform.position = this.transform.position;
        }

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

        if (isPossesing)
        {
            if (Input.GetKeyDown(atk1))
            {

            }
            if (Input.GetKeyDown(atk2))
            {

            }
        }
        else
        {
            if (Input.GetKeyDown(atk1))
            {
                Debug.Log("attacking as a ghost, atk1");
            }
            if (Input.GetKeyDown(atk2))
            {
                Debug.Log("attacking as a ghost, atk2");
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
            sr.color = purple;
        }
        if (Input.GetKeyDown(possess) && objInSight && !isPossesing)
        {
            hit.collider.gameObject.transform.SetParent(this.transform);
            hit.collider.gameObject.transform.position = this.transform.position;
            sr.enabled = false;
            isPossesing = true;
            bx.enabled = false;
            moveSpeed = hit.collider.gameObject.GetComponent<MoveScript>().moveSpeed;
        }
        if (Input.GetKeyDown(unpossess))
        {
            transform.DetachChildren();
            isPossesing = false;
            sr.enabled = true;
            bx.enabled = true;
            moveSpeed = 7f;
        }
    }
}