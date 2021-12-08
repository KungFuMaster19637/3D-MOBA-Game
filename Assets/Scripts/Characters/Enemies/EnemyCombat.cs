using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyCombat : MonoBehaviour
{
    [Header ("Enemy AI")]
    public GameObject targetedPlayer;
    public Vector3 originalPosition;
    public float returnRange;
    public float aggroRange;
    private bool isReturning;
    private float rotateVelocity = 0.0f;
    private float rotateSpeedForAttack = 0.075f;

    private EnemyStats enemyStatsScript;
    private NavMeshAgent agent;

    //RaycastHit hit;
    private float returnPosition = 0;
    private float playerDistance = 0;

    [Header("Enemy Animation")]
    private Animator anim;
    public bool attackIdle = false;
    public bool performMeeleeAttack = false;


    void Start()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        targetedPlayer = GameObject.FindGameObjectWithTag("Player");
        originalPosition = gameObject.transform.position;
        anim.SetBool("IsIdle", true);

    }

    void Update()
    {
        returnPosition = Vector3.Distance(originalPosition, transform.position);

        //If player is dead
        if (targetedPlayer == null)
        {
            isReturning = true;
            agent.SetDestination(originalPosition);
            agent.stoppingDistance = 0;
            anim.SetBool("IsAttacking", false);
        }

        //If player is alive -> check distance
        if (targetedPlayer != null)
        {
            playerDistance = Vector3.Distance(transform.position, targetedPlayer.transform.position);
        }

        //Distance Testing tool
        if (Input.GetKeyDown("k"))
        {
            Debug.Log(returnPosition);
            //Debug.Log("Player distance: " + playerDistance);
        }

        //If enemy is back to starting position
        if (returnPosition < 0.5)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsInRange", false);
            isReturning = false;
        }
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, aggroRange);

        //Debug.Log(playerDistance);
        //Debug.Log("return position: " + returnPosition);
        //Check if player is in range
        foreach (Collider player in playersInRange)
        {
            if (returnPosition < returnRange && !isReturning)
            {
                if (player.CompareTag("Player"))
                {
                    agent.SetDestination(targetedPlayer.transform.position);
                    anim.SetBool("IsInRange", true);
                    anim.SetBool("IsIdle", false);
                    agent.stoppingDistance = enemyStatsScript.attackRange;

                    //Check if enemy can attack player
                    if (playerDistance <= enemyStatsScript.attackRange)
                    {
                        anim.SetBool("IsAttacking", true);

                        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedPlayer.transform.position - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                        transform.eulerAngles = new Vector3(0, rotationY, 0);

                        //Attack player
                        if (performMeeleeAttack)
                        {
                            StartCoroutine(MeeleeAttackInterval());
                        }
                    }
                    else if (playerDistance > enemyStatsScript.attackRange)
                    {
                        anim.SetBool("IsAttacking", false);
                    }
                }
            }

            //Check is player ran out of aggro distance
            if (returnPosition >= returnRange || playerDistance > aggroRange) 
            {
                isReturning = true;
                agent.SetDestination(originalPosition);
                agent.stoppingDistance = 0.1f;
            }
        }
    }

    public float DamageCalculator(float incomingDamage)
    {
        float totalDamage;
        totalDamage = 100 / (100 + targetedPlayer.GetComponent<PlayerStats>().defence) * incomingDamage;
        Debug.Log("Enemy damage: " + totalDamage);
        return totalDamage;
    }

    public void AttackPlayer()
    {
        if (targetedPlayer != null)
        {
            if (!targetedPlayer.GetComponent<ArthurAbilityManager>().damageBlocked)
            {
                targetedPlayer.GetComponent<PlayerStats>().health -= DamageCalculator(enemyStatsScript.attackDamage);
            }
        }

        performMeeleeAttack = true;
    }

    IEnumerator MeeleeAttackInterval()
    {
        performMeeleeAttack = false;

        yield return new WaitForSeconds(enemyStatsScript.attackTime / ((100 + enemyStatsScript.attackTime) * 0.01f));
        if (targetedPlayer == null)
        {
            performMeeleeAttack = true;
        }
    }
}
