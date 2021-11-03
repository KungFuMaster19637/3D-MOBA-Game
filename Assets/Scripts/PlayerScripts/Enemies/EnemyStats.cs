using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDamage;
    //public float attackSpeed;
    public float attackTime;
    public float attackRange;
    public float defence;

    private GameObject player;
    private bool giveExpOnce;

    //Death variables
    public GameObject healthBar;
    private Animator anim;
    private NavMeshAgent agent;
    private HeroCombat heroCombat;

    public float expValue;

    /*
    Enemy monster stats:
    Blueboar:
    Health: 50
    Attack Damage: 20
    Attack Time: 2
    Attack Range: 2.5
    Defence: 20
    Exp Value: 4
    Return Range: 12
    Aggro Range: 8

    Dragonsouleater:
    Health: 100
    Attack Damage: 60 
    Attack Time: 2
    Attack Range: 3
    Defence: 50
    Exp Value: 8
    Return Range: 10
    Aggro Range: 7

    Skeleton:
    Health: 150
    Attack Damage: 80
    Attack Time: 1
    Attack Range: 1.5
    Defence: 25
    Exp Value: 7
    Return Range: 8
    Aggro Range: 6

    Dragonboss:???
    Health: 150 ????
    Attack Damage: 80
    Attack Time: 1
    Attack Range: 1.5
    Defence: 25
    Exp Value: 7
    Return Range: 8
    Aggro Range: 6

    */
    private void Start()
    {
        //heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        heroCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        giveExpOnce = false;
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (!giveExpOnce)
            {
                player.GetComponent<LevelUpStats>().SetExperience(expValue);
                MoneyDisplay.AddMoney(5);
                giveExpOnce = true;
            }
            heroCombat.targetedEnemy = null;
            StartCoroutine(PlayDeathAnimation());
            //heroCombatScript.performMeeleeAttack = false;

            //player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        anim.SetBool("IsDying", true);
        healthBar.SetActive(false);
        agent.isStopped = true;

        yield return new WaitForSeconds(2f);
        anim.SetBool("IsDying", false);
        Destroy(gameObject);
    }

}
