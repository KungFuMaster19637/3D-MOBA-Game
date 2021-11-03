using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerStats : MonoBehaviour
{
    [Header ("Player Stats")]
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
    
    //private GameObject player;

    [Header ("Components")]
    public GameObject healthBar;
    public GameOver gameOver;

    private Animator anim;
    private NavMeshAgent agent;
    private Movement movement;

    private void Start()
    {
        //heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        //player = GameObject.FindGameObjectWithTag("Player");

        heroCombatScript = GetComponent<HeroCombat>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<Movement>();

        health = maxHealth;
        mana = maxMana;
    }

    private void Update()
    {
        //Development tools, reducing health and mana manually, remove after testing
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

        heroCombatScript.isHeroAlive = false;
        anim.SetBool("Dying", false);

        StartCoroutine(gameOver.PlayerDied());
        yield return null;
    }

}
