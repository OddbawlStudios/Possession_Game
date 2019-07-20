using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int curHP, maxHP;
    public bool isDead;
    public int Dmg;
    public PossessManager pos;
    public int tempHP, tempMax;

    // Use this for initialization
    void Start () {
        pos = GetComponent<PossessManager>();
        maxHP = 20;
        curHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(pos.isPossesing)
        {
            tempHP = pos.posses.curhealth;
            tempMax = pos.posses.maxHealth;
        }
        else
        {
            tempHP = curHP;
            tempMax = maxHP;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            DamagePlayer(5);
        }

	}

    public int DamagePlayer(int dmg)
    {
        if (isDead)
        {
            Debug.Log("Game Over");
        }
        else
        {
            if (pos.isPossesing)
            {
                Dmg = dmg - pos.dmod;
                if (Dmg < 1)
                {
                    Dmg = 1;
                }
                pos.hit.collider.gameObject.GetComponent<MoveScript>().curhealth = pos.hit.collider.gameObject.GetComponent<MoveScript>().curhealth -= Dmg;
                Mathf.Clamp(Dmg, 1, Mathf.Infinity);
            }
            else
            {
                curHP -= dmg;
            }
        }

        return curHP;
    }

    public void HealPlayer()
    {

    }

    public void Dead()
    {
        if (curHP <= 0)
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

}
