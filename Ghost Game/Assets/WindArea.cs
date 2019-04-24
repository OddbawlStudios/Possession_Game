using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour {

    public float strength;
    public Vector2 direction;
    public PlayerController p;

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerController>().weight <= 30)
        {
            col.gameObject.transform.Translate(direction.x, direction.y, 0);
        }
    }

}
