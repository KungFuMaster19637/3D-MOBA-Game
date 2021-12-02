using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FemaleNPC2 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    //Fix to right locations in the world
    private float minX = -104.6f;
    private float maxX = -106.5f;
    private float minZ = -4.2f;
    private float maxZ = -8f; 


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(MoveNPC());

    }

    IEnumerator MoveNPC()
    {
        anim.SetBool("IsWalking", true);

        Vector3 randoDir = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        agent.SetDestination(randoDir);

        yield return new WaitUntil(()=> agent.remainingDistance < 0.01);

        anim.SetBool("IsWalking", false);

        yield return new WaitForSeconds(8f);
        StartCoroutine(MoveNPC());
    }
}
