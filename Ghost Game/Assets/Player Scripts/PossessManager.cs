using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessManager : MonoBehaviour {

    public Moving m;
    public RaycastHit2D hit;
    public bool objInSight = false;
    public bool isPossesing;
    public MoveScript posses;
    public KeyCode pose, unp;
    private BoxCollider2D b;
    private SpriteRenderer sr;
    [SerializeField]
    private Vector3 oldPos;

    public int dmod, tempDmod;

    // Use this for initialization
    void Start () {
        m = GetComponent<Moving>();
        posses = GetComponent<MoveScript>();
        b = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(pose) && objInSight && !isPossesing)
        {
            posses = hit.collider.gameObject.GetComponent<MoveScript>();
            Possess();
        }
        if(Input.GetKeyDown(unp))
        {
            Unpossess();
        }


        if (isPossesing)
        {
            Possess();
        }
        else
        {

        }

	}

    public void FixedUpdate()
    {
            Debug.DrawLine(this.transform.position, m.direction, Color.cyan);
            if (Physics2D.Linecast(this.transform.position, m.direction, 1 << LayerMask.NameToLayer("obj")))
            {
                hit = Physics2D.Linecast(this.transform.position, m.direction, 1 << LayerMask.NameToLayer("obj"));
                objInSight = true;
            }
            else
            {
                objInSight = false;
            }
    }

    public void Possess()
    {
        isPossesing = true;
        b.enabled = false;
        sr.enabled = false;
        posses.transform.SetParent(this.transform);
        posses.transform.position = this.transform.position;
        tempDmod = posses.DamageMod;
        posses.box.enabled = false;
    }

    public void Unpossess()
    {
        if(posses)
        {
        posses.Detach();
        }
        isPossesing = false;
        b.enabled = true;
        sr.enabled = true;
        tempDmod = dmod;
        posses = null;
    }

}
