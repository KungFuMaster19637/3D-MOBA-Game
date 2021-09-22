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
