using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCombat : MonoBehaviour
{
    [Header("Boss AI")]
    public GameObject targetedPlayer;
    public Vector3 originalPosition;
    public float returnRange;
    public float aggroRange;
    private float rotateVelocity = 0.0f;
    private float rotateSpeedForAttack = 0.015f;
    private bool wakeUpOnce = false;
    private float flameAttackCounter = 0;

    private EnemyStats enemyStatsScript;
    private NavMeshAgent agent;

    //RaycastHit hit;
    private float returnPosition = 0;
    private float playerDistance = 0;

    [Header("Enemy Animation")]
    private Animator anim;
    public bool attackIdle = false;
    public bool performMeeleeAttack = false;
    public bool performFlameAttack = false;


    void Start()
    {
        enemyStatsScript = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        targetedPlayer = GameObject.FindGameObjectWithTag("Player");
        originalPosition = gameObject.transform.position;
    }

    void Update()
    {
        returnPosition = Vector3.Distance(originalPosition, transform.position);

        //If player is dead
        if (targetedPlayer == null)
        {
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
            Debug.Log("Player distance: " + playerDistance);
        }

        Collider[] playersInRange = Physics.OverlapSphere(transform.position, aggroRange);

        if (playerDistance > enemyStatsScript.attackRange)
        {
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", true);
        }
        //Check if player is in range
        foreach (Collider player in playersInRange)
        {
            if (player.CompareTag("Player"))
            {
                if (wakeUpOnce == false)
                {
                    StartCoroutine(DragonWakeUp());
                }
                //Check if enemy can attack player
                if (playerDistance <= enemyStatsScript.attackRange)
                {
                    anim.SetBool("IsAttacking", true);
                    anim.SetBool("IsIdle", false);

                    Quaternion rotationToLookAt = Quaternion.LookRotation(targetedPlayer.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                    ref rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    //Attack player
                    if (performMeeleeAttack && flameAttackCounter < 5)
                    {
                        Debug.Log(flameAttackCounter);
                        flameAttackCounter++;
                        StartCoroutine(MeeleeAttackInterval());
                    }
                    else if (flameAttackCounter == 5)
                    {
                        anim.SetBool("IsAttacking", false);
                        anim.SetBool("FlameAttack", true);
                        flameAttackCounter = 0;
                        StartCoroutine(FlameAttackInterval());
                    }
                }
                if (playerDistance > enemyStatsScript.attackRange)
                {
                    Debug.Log("disengaging");
                    anim.SetBool("IsAttacking", false);
                    anim.SetBool("IsIdle", true);
                }
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

    public void MeeleeAttackPlayer()
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

    public void FlameAttackPlayer()
    {
        if (targetedPlayer != null)
        {
            if (!targetedPlayer.GetComponent<ArthurAbilityManager>().damageBlocked)
            {
                //Dmg needs to be determined
                //targetedPlayer.GetComponent<PlayerStats>().health -= DamageCalculator(enemyStatsScript.attackDamage);
            }
        }
        performFlameAttack = true;
    }

    IEnumerator DragonWakeUp()
    {
        anim.SetBool("IsScreaming", true);
        yield return new WaitForSeconds(3.5f);
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsScreaming", false);
        wakeUpOnce = true;
        yield return null;
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

    IEnumerator FlameAttackInterval()
    {
        performFlameAttack = false;
        yield return new WaitForSeconds(2.5f);
        if (targetedPlayer == null)
        {
            performFlameAttack = true;
        }
        anim.SetBool("FlameAttack", false);
        anim.SetBool("IsAttacking", true);

        Debug.Log("flame att inc");
        yield return null;
    }
}
