using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    public enum Direction { N, E, S, W };
    public float length = 3f;
    public float moveSpeed = 7f, tempSpeed;
    public KeyCode up, down, left, right;
    public Direction dir;
    public Vector3 direction;
    public int weight, tempWeight;
    

    public PossessManager p;

    // Use this for initialization
    void Start () {
        p = GetComponent<PossessManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if(p.isPossesing)
        {
            tempSpeed = p.posses.moveSpeed;
            tempWeight = p.posses.weight;
        }
        else
        {
            tempWeight = weight;
            tempSpeed = moveSpeed;
        }

        if (Input.GetKey(up))
        {
            dir = Direction.N;
            direction = new Vector3(this.transform.position.x, this.transform.position.y + length, 0);
            transform.Translate(Vector2.up * tempSpeed * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            dir = Direction.S;
            direction = new Vector3(this.transform.position.x, this.transform.position.y + -length, 0);
            transform.Translate(Vector2.down * tempSpeed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            dir = Direction.W;
            transform.Translate(Vector2.left * tempSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + -length, this.transform.position.y, 0);
        }
        if (Input.GetKey(right))
        {
            dir = Direction.E;
            transform.Translate(Vector2.right * tempSpeed * Time.deltaTime);
            direction = new Vector3(this.transform.position.x + length, this.transform.position.y, 0);
        }
    }
}

