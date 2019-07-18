using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    [SerializeField]
    private AreaEffector2D ae;


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Rigidbody2D>().mass > 10)
        {
            ae.enabled = false;
        }else
        {
            ae.enabled = true;
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        col.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 20000 * Time.deltaTime);

        /*Debug.Log("Player is in trigger");
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
        col.gameObject.GetComponent<Rigidbody2D>().velocity.Set(0f,0f);
    }

}
