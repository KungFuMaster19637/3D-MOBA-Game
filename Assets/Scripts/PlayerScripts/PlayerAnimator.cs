using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;

    float motionSmoothTime = 0.1f;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float speed = agent.velocity.magnitude / agent.speed;

        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
