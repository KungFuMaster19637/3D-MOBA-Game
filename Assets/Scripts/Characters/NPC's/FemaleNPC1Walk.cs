using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FemaleNPC1Walk : MonoBehaviour
{

    private NavMeshAgent agent;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;
    [SerializeField] private Transform point4;

    private bool moveToPos1;
    private bool moveToPos2;
    private bool moveToPos3;
    private bool moveToPos4;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        moveToPos1 = true;
        moveToPos2 = false;
        moveToPos3 = false;
        moveToPos4 = false;
    }

    private void Update()
    {
        if (moveToPos1)
        {
            agent.SetDestination(point1.position);
        }
        if (moveToPos2)
        {
            agent.SetDestination(point2.position);
        }
        if (moveToPos3)
        {
            agent.SetDestination(point3.position);
        }
        if (moveToPos4)
        {
            agent.SetDestination(point4.position);
        }

        if (Vector3.Distance(transform.position, point1.position) < 0.2f)
        {
            moveToPos1 = false;
            moveToPos2 = true;
        }
        if (Vector3.Distance(transform.position, point2.position) < 0.2f)
        {
            moveToPos2 = false;
            moveToPos3 = true;
        }
        if (Vector3.Distance(transform.position, point3.position) < 0.2f)
        {
            moveToPos3 = false;
            moveToPos4 = true;
        }
        if (Vector3.Distance(transform.position, point4.position) < 0.2f)
        {
            moveToPos4 = false;
            moveToPos1 = true;
        }
    }
}
