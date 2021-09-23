using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyCombat : MonoBehaviour
{
    //AI targetting
    public GameObject targetedPlayer;
    public Vector3 originalPosition;
    public float returnRange;
    public float aggroRange;
    private bool isReturning;
    private float rotateVelocity = 0.0f;
    private float rotateSpeedForAttack = 0.075f;

    private EnemyStats enemyStatsScript;
    private NavMeshAgent agent;

    RaycastHit hit;
    private float returnPosition = 0;
    private float playerDistance = 0;

    [Header("Enemy Animation")]
    private Animator anim;
    public bool attackIdle = false;
    public bool performMeeleeAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        targetedPlayer = GameObject.FindGameObjectWithTag("Player");
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
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

        //If player is alive, get these stats
        if (targetedPlayer != null)
        {
            playerDistance = Vector3.Distance(transform.position, targetedPlayer.transform.position);
        }

        //if (Input.GetKeyDown("k"))
        //{
        //    Debug.Log(returnPosition);
        //    Debug.Log("Player distance: " + playerDistance);
        //}
        if (returnPosition < 0.4)
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsInRange", false);
            isReturning = false;
        }
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, aggroRange);
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
                    if (playerDistance <= enemyStatsScript.attackRange)
                    {
                        anim.SetBool("IsAttacking", true);
                        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedPlayer.transform.position - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                        transform.eulerAngles = new Vector3(0, rotationY, 0);

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
            if (returnPosition >= returnRange || playerDistance > aggroRange) 
            {
                isReturning = true;
                agent.SetDestination(originalPosition);
                agent.stoppingDistance = 0;
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
