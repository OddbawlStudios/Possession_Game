using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour {

    public enum direction { N,E,S,W};
    public direction Direction;
    public GameObject go;
    public int force = 10;
    public Vector2 vec;
    public PlayerController p;

	// Use this for initialization
	void Start () {
        p = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(Direction)
        {
            case direction.N:
                vec = new Vector2(0, force);
                break;
            case direction.E:
                vec = new Vector2(force, 0);
                break;

            case direction.S:
                vec = new Vector2(0, -force);
                break;

            case direction.W:
                vec = new Vector2(-force, 0);
                break;
        }
	}

    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p = collision.gameObject.GetComponent<PlayerController>();
            if (p.weight <= 30)
            {
                force = 10;
                p.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }*/

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p = collision.gameObject.GetComponent<PlayerController>();
            if(p.weight <= 30)
            {
                p.GetComponent<Rigidbody2D>().AddForce(vec * Time.deltaTime);
            }
        }
    }

    /*public void OnTriggerExit2D(Collider2D collision)
    {
        p.gameObject.GetComponent<Rigidbody2D>().velocity.Set(0f, 0f);
    }*/


}
