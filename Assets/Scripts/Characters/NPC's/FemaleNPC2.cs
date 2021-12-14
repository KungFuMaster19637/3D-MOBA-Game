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
    private Vector3 walkDistance;
    public static bool enterCity;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enterCity = false;
        StartCoroutine(MoveNPC());

    }
    private void Update()
    {
        if(enterCity)
        {
            StartCoroutine(MoveNPC());
            enterCity = false;
        }
    }

    IEnumerator MoveNPC()
    {
        anim.SetBool("IsWalking", true);

        StartCoroutine(DistanceReroll());

        agent.SetDestination(walkDistance);

        yield return new WaitForSeconds(1.35f);

        anim.SetBool("IsWalking", false);

        yield return new WaitForSeconds(8f);
        StartCoroutine(MoveNPC());
    }

    IEnumerator DistanceReroll()
    {
        walkDistance = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        //Debug.Log("rerolling distance " + Vector3.Distance(walkDistance, transform.position));
        if (Vector3.Distance(walkDistance, transform.position) < 1.8f)
        {
            StartCoroutine(DistanceReroll());
        }
        else
        {
            yield return null;
        }
    }
}
