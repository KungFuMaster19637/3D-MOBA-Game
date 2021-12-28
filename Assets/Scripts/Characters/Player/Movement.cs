using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{

    public NavMeshAgent agent;

    public bool canMove = true;
    public float rotateMovementSpeed = 0.075f;
    public float rotateVelocity;

    private HeroCombat heroCombatScript;
    [SerializeField] private ParticleSystem clickEffect;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    void Update()
    {
        if (heroCombatScript.targetedEnemy != null)
        {
            if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (canMove)
                {
                    if (hit.collider.tag == "Floor")
                    {
                        Instantiate(clickEffect, hit.point, Quaternion.identity);
                        //Player move to raycastpoint
                        agent.SetDestination(hit.point);
                        heroCombatScript.targetedEnemy = null;
                        agent.stoppingDistance = 0;

                        //Rotation
                        Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                            ref rotateVelocity, rotateMovementSpeed * (Time.deltaTime * 5));
                        transform.eulerAngles = new Vector3(0, rotationY, 0);
                    }
                }
            }
        }
    }
}
