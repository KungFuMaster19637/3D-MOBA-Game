using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float healthRegeneration;
    public float maxMana;
    public float mana;
    public float manaRegeneration;
    public float attackDamage;
    public float attackSpeed;
    public float attackTime;
    public float attackRange;
    public float spellPower;
    public float defence;

    HeroCombat heroCombatScript;
    
    private GameObject player;

    public GameObject healthBar;
    private Animator anim;
    private NavMeshAgent agent;
    private Movement movement;
    public float expValue;

    private void Start()
    {
        //heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        player = GameObject.FindGameObjectWithTag("Player");

        heroCombatScript = GetComponent<HeroCombat>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKey("h"))
        {
            health--;
        }

        if (Input.GetKey("g"))
        {
            mana--;
        }

        //Health regeneration
        if (health > 0 && health < maxHealth)
        {
            health += healthRegeneration * Time.deltaTime;
        }
        if (health <= 0)
        {
            Debug.Log("Player died");
            heroCombatScript.targetedEnemy = null;
            heroCombatScript.performMeeleeAttack = false;
            StartCoroutine(PlayDeathAnimation());
            //player.GetComponent<LevelUpStats>().SetExperience(expValue);
        }


        //Mana regeneration
        if (mana < maxMana)
        {
            mana += manaRegeneration * Time.deltaTime;
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        anim.SetBool("Dying", true);
        healthBar.SetActive(false);
        agent.isStopped = true;
        movement.canMove = false;
        mana = 0;

        yield return new WaitForSeconds(2f);
        heroCombatScript.isHeroAlive = false;
        anim.SetBool("Dying", false);
        Destroy(gameObject);

    }


}
