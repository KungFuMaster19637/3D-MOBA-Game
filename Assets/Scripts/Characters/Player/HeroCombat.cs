using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType
    {
        Meelee,
        Ranged
    }

    public HeroAttackType heroAttackType;

    public float rotateSpeedForAttack;

    public bool basicAttackIdle = false;
    public bool isHeroAlive = true; 
    public bool performMeeleeAttack = false;

    private Movement moveScript;
    private PlayerStats statsScript;
    private PlayerSounds playerSounds;
    private Animator anim;

    public GameObject targetedEnemy;

    //Work in progress, ranged character
    [Header("Ranged Variables")]
    public bool performRangedAttack = true;
    public GameObject projPrefab;
    public Transform projSpawnPoint;

    void Start()
    {
        moveScript = GetComponent<Movement>();
        statsScript = GetComponent<PlayerStats>();
        playerSounds = GetComponent<PlayerSounds>();
        anim = GetComponent<Animator>();

        anim.SetFloat("AttackSpeed", statsScript.attackSpeed);
    }
    void Update()
    {
        /*
        For testing attack speed:
        if (Input.GetKey("c"))
        {
            anim.SetFloat("AttackSpeed", 2f);
        }
        */

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
        }
        if (targetedEnemy != null)
        {
            //Caused ranged errors
            //anim.SetBool("Basic Attack", true);

            if (gameObject.name == "King Arthur")
            {
                if (targetedEnemy.tag == "Boss")
                {
                    statsScript.attackRange = 7f;
                }
                if (targetedEnemy.tag == "Enemy")
                {
                    statsScript.attackRange = 2.5f;
                }
            }
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > statsScript.attackRange)
            {
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = statsScript.attackRange;
            }
            else
            {
                //Meelee
                anim.SetBool("Basic Attack", true);

                if (heroAttackType == HeroAttackType.Meelee)
                {

                    Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    moveScript.agent.SetDestination(transform.position);


                    if (performMeeleeAttack)
                    {
                        StartCoroutine(MeeleeAttackInterval());
                    }
                }

                //Ranged
                if (heroAttackType == HeroAttackType.Ranged)
                {

                    Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                        ref moveScript.rotateVelocity, rotateSpeedForAttack * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    moveScript.agent.SetDestination(transform.position);


                    if (performRangedAttack)
                    {
                        StartCoroutine(RangedAttackInterval());
                    }
                }
            }
        }
        
    }

    public float DamageCalculator(float incomingDamage)
    {
        float totalDamage;
        totalDamage = 100 / (100 + targetedEnemy.GetComponent<EnemyStats>().defence) * incomingDamage;
        return totalDamage;
    }

    public void MeeleeAttack()
    {
        if (targetedEnemy != null)
        {
            playerSounds.AbilitySoundPlayed(2, 0);
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                targetedEnemy.GetComponent<EnemyStats>().health -= DamageCalculator(statsScript.attackDamage);
            }
        }
        performMeeleeAttack = true;
    }

    //Work In Progress
    //Ranged attacks
    public void RangedAttack()
    {
        if (targetedEnemy != null)
        {
            playerSounds.AbilitySoundPlayed(2, 0);
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                SpawnRangedProjectile("Minion", targetedEnemy);
            }
        }
        performRangedAttack = true;
    }

    void SpawnRangedProjectile(string typeOfEnemy, GameObject targetedEnemyObj)
    {
        float dmg = statsScript.attackDamage;


        if (typeOfEnemy == "Minion")
        {
            projPrefab.GetComponent<RangedProjectile>().targetType = typeOfEnemy;
            projPrefab.GetComponent<RangedProjectile>().target = targetedEnemyObj;
            projPrefab.GetComponent<RangedProjectile>().targetSet = true;
        }
        Instantiate(projPrefab, projSpawnPoint.transform.position, Quaternion.Euler(90f, 0f, 0f));
    }


    IEnumerator MeeleeAttackInterval()
    {
        performMeeleeAttack = false;

        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            performMeeleeAttack = true;
        }
    }

    IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;

        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            performRangedAttack = true;
        }
    }
}
