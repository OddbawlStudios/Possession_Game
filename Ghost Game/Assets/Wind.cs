using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    [SerializeField]
    private AreaEffector2D ae;
    private float speed;
    [SerializeField]
    private float xMove, yMove;
    private Rigidbody2D rb;

    public void OnTriggerEnter2D(Collider2D col)
    {
        /*if(col.gameObject.GetComponent<Rigidbody2D>().mass > 10)
        {
            ae.enabled = false;
        }
        else
        {
            ae.enabled = true;
        }*/
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        Moving move;
        Transform t;
        Vector3 vec;
        t = col.GetComponent<Transform>();
        rb = col.GetComponent<Rigidbody2D>();
        vec = new Vector3(xMove * Time.deltaTime,yMove * Time.deltaTime, 0f);
        if (rb.mass <= 4)
        {
            t.position += vec;
        }
        /*
        Debug.Log("Player is in trigger");
        Vector3 position = transform.position;
        Vector3 targetPosition = col.transform.position;
        Vector3 direction = targetPosition - position;
        direction.Normalize();
        int moveSpeed = 10;
        col.transform.position += direction * moveSpeed * Time.deltaTime;
        */
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Transform t;
        Vector3 vec;
        t = col.gameObject.GetComponent<Transform>();
        vec = new Vector3(t.position.x, t.position.y, 0f);
        t.position = vec;
    }

}
