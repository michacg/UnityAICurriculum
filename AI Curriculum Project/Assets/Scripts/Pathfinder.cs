using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < 5)
        {
            anim.Play("Hit");
        }
        else
        {
            anim.Play("Run");
            agent.SetDestination(target.transform.position);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            Debug.Log("Pushed!");
            //Have students have something happen.
        }
    }
}
