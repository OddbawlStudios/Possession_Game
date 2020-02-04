using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public enum State {Patrol, Follow, Attack, LookOut };
    public State curState;
    private int Health;
    public float fieldOfView = 110f;
    private bool playerInSight = false;
    public Transform target;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Targeted();
        switch (curState)
        {
            case State.Patrol:
                break;
            case State.Follow:
                break;
            case State.Attack:
                break;
            case State.LookOut:
                break;
        }
    }

    public void Hurt(int dmg)
    {
        if(Health >= 0)
        {
        Health -= dmg;
        }else
        {
            Die();
        }
    }

    private void Die()
    {
        //this kills the enemy
    }

    private void LookingOut()
    {
        
    }

    private void Patrolling()
    {

    }

    private void Attacking()
    {

    }

    private void Following()
    {

    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector2 fov1 = Quaternion.AngleAxis(fieldOfView, transform.forward) * transform.up * radius;
        Vector2 fov2 = Quaternion.AngleAxis(-fieldOfView, transform.forward) * transform.up * radius;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, fov1);
        Gizmos.DrawRay(transform.position, fov2);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position,(target.position).normalized * radius);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.up * radius);
    }
    */
    public Vector2 DirfromAngle(float angle, bool isGlobalAngle)
    {
        if(!isGlobalAngle)
        {
            angle += -transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad),0f);
    }

    void Targeted()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, dirToTarget) < fieldOfView / 2)
        {
            float distToTarget = Vector3.Distance(transform.position, target.position);

            if(!Physics.Raycast(transform.position, dirToTarget, distToTarget))
            {
                Debug.Log("What is this suppose to do?");
            }
        }
    }
}
