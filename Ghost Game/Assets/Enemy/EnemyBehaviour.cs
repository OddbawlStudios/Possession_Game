using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public enum State { Patrol, Follow, Attack, LookOut, IsPossessed };
    public State curState;
    private int Health;
    public float fieldOfView = 110f;
    private bool playerInSight = false;
    public Transform target;
    public float radius;
    public bool isPossessed = false;

    private float step;
    private float speed = 5f;
    public Transform enemy;
    private Vector3 pos1, pos2, pos3, pos4;
    private int posLocation;
    private int distX = 4, distY = 4;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        pos1 = enemy.position;
        pos2 = new Vector3(enemy.position.x + distX, pos1.y, 0f);
        pos3 = new Vector3(pos2.x, pos2.y + distY, 0f);
        pos4 = new Vector3(pos3.x - distX, pos3.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPossessed)
        {
            curState = State.IsPossessed;
        }else
        {
            curState = State.Patrol;
        }
        Targeted();
        switch (curState)
        {
            case State.Patrol:
                Patrolling();
                break;
            case State.Follow:
                break;
            case State.Attack:
                break;
            case State.LookOut:
                break;
            case State.IsPossessed:
                Position(enemy, player.gameObject.transform.position, isPossessed);
                break;
        }
    }

    public void Hurt(int dmg)
    {
        if (Health >= 0)
        {
            Health -= dmg;
        } else
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
        step = speed * Time.deltaTime;
        if (posLocation == 0)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, pos2, step);
            if (Vector3.Distance(enemy.position, pos2) < .02f)
            {
                posLocation = 1;
                enemy.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }
        if (posLocation == 1)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, pos3, step);
            if (Vector3.Distance(enemy.position, pos3) < .02f)
            {
                posLocation = 2;
                enemy.localRotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }
        if (posLocation == 2)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, pos4, step);
            if (Vector3.Distance(enemy.position, pos4) < .02f)
            {
                posLocation = 3;
                enemy.localRotation = Quaternion.Euler(0f, 0f, 270f);
            }
        }
        if (posLocation == 3)
        {
            enemy.position = Vector3.MoveTowards(enemy.position, pos1, step);
            if (Vector3.Distance(enemy.position, pos1) < .02f)
            {
                posLocation = 0;
                enemy.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
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
        if (!isGlobalAngle)
        {
            angle += -transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0f);
    }

    void Targeted()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        if (Vector3.Angle(transform.right, dirToTarget) < fieldOfView / 2)
        {
            float distToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, dirToTarget, distToTarget))
            {
                Debug.Log("What is this suppose to do?");
            }
        }
    }

    public Transform Position(Transform go, Vector3 possessor, bool possessed)
    {
        if(possessed)
        {
            go.position = possessor;
            enemy.position = possessor;
        }
        else
        {
            go.position = enemy.position;
            enemy.position = pos1;
            curState = State.Patrol;
        }
        return go;
    }
}
