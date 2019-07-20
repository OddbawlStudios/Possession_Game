using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public int maxHealth, curhealth;
    public string at1, at2;
    public PossessManager p;
    public int DamageMod;
    public BoxCollider2D box;
    public float moveSpeed;
    public int weight;

    //this class is to build enemy stats based around enemies themselves
    //meaning they have their own attack strength, health, and movement speed

    public void Start()
    {
        maxHealth = 35;
        curhealth = maxHealth;
        box = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
            HealthManager(); 
    }

    public string Attack1(string attack1)
    {
        //Debug.Log(atk1);
        attack1 = at1;
        return attack1;
    }

    public string Attack2(string attack2)
    {
        //Debug.Log(atk2);
        attack2 = at2;
        return attack2;
    }

    public void HealthManager()
    {
        p = GetComponentInParent<PossessManager>();
        if (curhealth <= 0)
        {
            p.Unpossess();
            Destroy(this.gameObject, .2f);
        }
    }

    public void Detach()
    {
        this.gameObject.transform.SetParent(null);
        box.enabled = true;
    }

}
