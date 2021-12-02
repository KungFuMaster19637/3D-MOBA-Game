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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(point1.position);

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, point1.position) < 0.2f)
        {
            agent.SetDestination(point2.position);
        }
        if (Vector3.Distance(transform.position, point2.position) < 0.2f)
        {
            agent.SetDestination(point3.position);
        }
        if (Vector3.Distance(transform.position, point3.position) < 0.2f)
        {
            agent.SetDestination(point4.position);
        }
        if (Vector3.Distance(transform.position, point4.position) < 0.2f)
        {
            agent.SetDestination(point1.position);
        }
    }

}
