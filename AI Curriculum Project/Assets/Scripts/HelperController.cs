using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperController : MonoBehaviour
{
    //State for our Finite State Machine
    public enum State
    {
        follow,
        pickup,
        attack,
        newState
    };

    public GameObject player;
    public float targetDistance;
    public float allowedDistance = 1f;
    public float speed;
    public float lookRadius = 5f;
    public RaycastHit hit;
    public int hitCount = 3;

    private Animator anim;
    private State currentState = State.follow;
    private Collider[] objectsAround;
    private GameObject currentPickup;
    private GameObject currentEnemy;
    private int currentHitCount = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        LookAround();

        switch(currentState)
        {
            case State.follow:
                FollowUpdate();
                break;
            case State.pickup:
                PickupUpdate();
                break;
            case State.attack:
                AttackUpdate();
                break;
            case NewStateUpdate();
                break;
        }
    }

    void FollowUpdate()
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            targetDistance = hit.distance;
            if (targetDistance >= allowedDistance)
            {
                speed = 0.03f;
                anim.Play("walk");
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            }
            else
            {
                speed = 0f;
            }
        }
    }

    void PickupUpdate()
    {
        transform.LookAt(currentPickup.transform);
        speed = 0.03f;
        anim.Play("walk");
        transform.position = Vector3.MoveTowards(transform.position, currentPickup.transform.position, speed);
    }

    void AttackUpdate()
    {
        transform.LookAt(currentEnemy.transform);
        speed = 0.03f;
        anim.Play("walk");
        transform.position = Vector3.MoveTowards(transform.position, currentEnemy.transform.position, speed);
    }

    void NewStateUpdate()
    {

    }

    void LookAround()
    {
        objectsAround = Physics.OverlapSphere(GetComponent<Transform>().position, lookRadius);
        for (int i = 0; i < objectsAround.Length; ++i)
        {
            if (objectsAround[i].gameObject.CompareTag("pickup"))
            {
                currentState = State.pickup;
                currentPickup = objectsAround[i].gameObject;
                break;  
            }

            if (objectsAround[i].gameObject.CompareTag("enemy"))
            {
                currentState = State.attack;
                currentEnemy = objectsAround[i].gameObject;
                break;
            }
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("pickup"))
        {
            Destroy(col.gameObject);
            currentState = State.follow;
        }

        if (col.gameObject.CompareTag("enemy"))
        {
            Debug.Log("attack!");
            anim.Play("attack");
            currentHitCount++;
            if (currentHitCount == (hitCount)) //Enemy is out of HP!
            {
                Destroy(col.gameObject);

                currentHitCount = 0;
                currentState = State.follow;
            }
        }
    }
}
