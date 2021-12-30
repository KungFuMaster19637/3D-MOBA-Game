using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArthurAbilityManager : MonoBehaviour
{
    //Movement moveScript;
    NavMeshAgent agent;
    PlayerStats statsScript;
    Movement movement;

    [Header("Ability Effects")]
    public ParticleSystem healingBase;

    [Header("Passive")]
    public float healthGain;
    public float manaGain;

    [Header("Ability 1")]
    public float movementSpeedBuff;
    public float duration1 = 0;
    public float totalDuration1;
    public float attackBuff;
    private float attackBuffExtra;
    private bool ability1Pressed = false;


    [Header("Ability 2")]
    public float healAmount;
    public float regenerationBuff;
    public float healRange;
    public float duration2 = 0;
    public float totalDuration2;
    private bool ability2Pressed = false;

    [Header ("Ability 3")]
    public bool damageBlocked = false;
    public float duration3 = 0;
    public float totalDuration3;
    private bool ability3Pressed = false;

    [Header("Ability 4")]
    public float attackSpeedBuff;
    public float attackRangeBuff;
    public float duration4 = 0;
    public float totalDuration4;
    private bool ability4Pressed = false;

    private Animator anim;



    private void Start()
    {
        statsScript = GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }


    private void Update()
    {
        //Ability Timers

        //Ability 1 pressed
        if (ability1Pressed)
        {
            duration1 += Time.deltaTime;
        }
        if (duration1 >= totalDuration1)
        {
            DeactivateAbility1();
        }

        //Ability 2 pressed
        if (ability2Pressed)
        {
            duration2 += Time.deltaTime;
        }
        if (duration2 >= totalDuration2)
        {
            DeactivateAbility2();
        }

        //Ability 3 pressed
        if (ability3Pressed)
        {
            duration3 += Time.deltaTime;
        }
        if (duration3 >= totalDuration3)
        {
            DeactivateAbility3();
        }

        //Ability 4 pressed
        if (ability4Pressed)
        {
            duration4 += Time.deltaTime;
        }
        if (duration4 >= totalDuration4)
        {
            DeactivateAbility4();
        }

    }

    public void ArthurPassive()
    {
        if (statsScript.health + healthGain > statsScript.maxHealth)
        {
            statsScript.health = statsScript.maxHealth;
        }
        else if (statsScript.health + healthGain <= statsScript.maxHealth)
        {
            statsScript.health += healthGain;
        }

        if (statsScript.mana + manaGain > statsScript.maxMana)
        {
            statsScript.mana = statsScript.maxMana;
        }
        else if (statsScript.mana + manaGain <= statsScript.maxMana)
        {
            statsScript.mana += manaGain;
        }
    }

    //Abiity 1
    public void ActivateAbility1()
    {
        //Have to implement this so it doesn't become permanently buffed in some cases
        attackBuffExtra = statsScript.attackDamage * 0.1f;
        attackBuff += attackBuffExtra;

        ability1Pressed = true;
        statsScript.attackDamage += attackBuff;
        agent.speed += movementSpeedBuff;
    }

    public void DeactivateAbility1()
    {
        ability1Pressed = false;
        statsScript.attackDamage -= attackBuff;
        agent.speed -= movementSpeedBuff;
        duration1 = 0;

        attackBuff -= attackBuffExtra;

    }

    //Ability 2
    public void ActivateAbility2()
    {
        Instantiate(healingBase, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z), gameObject.transform.rotation);

        healAmount += (statsScript.spellPower * 0.5f);

        Collider[] alliesInRange = Physics.OverlapSphere(transform.position, healRange);
        foreach (Collider ally in alliesInRange)
        {
            if (ally.CompareTag("Ally"))
            { 
                ally.GetComponent<PlayerStats>().health += healAmount;
            }
        }
        ability2Pressed = true;

        if (statsScript.health + healAmount > statsScript.maxHealth)
        {
            statsScript.health = statsScript.maxHealth;
        }
        else if (statsScript.health + healAmount <= statsScript.maxHealth)
        {
            statsScript.health += healAmount;
        }
        //statsScript.health += healAmount;
        statsScript.healthRegeneration += regenerationBuff;
    }

    public void DeactivateAbility2()
    {
        ability2Pressed = false;
        statsScript.healthRegeneration -= regenerationBuff;
        duration2 = 0;
    }

    public void ActivateAbility3()
    {
        ability3Pressed = true;
        damageBlocked = true;
    }

    public void DeactivateAbility3()
    {
        Debug.Log("deactivating ability 3");
        ability3Pressed = false;
        damageBlocked = false;
        duration3 = 0;
    }

    public void ActivateAbility4()
    {
        StartCoroutine(UltimateAnimation());
        ability4Pressed = true;
        anim.SetFloat("AttackSpeed", attackSpeedBuff);
        statsScript.attackRange += attackRangeBuff;
    }
    public void DeactivateAbility4()
    {
        Debug.Log("deactivating ability 4");
        ability4Pressed = false;
        anim.SetFloat("AttackSpeed", statsScript.attackSpeed);
        statsScript.attackRange -= attackRangeBuff;
        duration4 = 0;
    }


    private IEnumerator UltimateAnimation()
    {
        //Stop walking and cast ultimate
        anim.SetBool("Ultimate", true);
        agent.isStopped = true;
        movement.canMove = false;
        yield return new WaitForSeconds(2f);

        //Start walking again
        anim.SetBool("Ultimate", false);
        agent.isStopped = false;
        movement.canMove = true;
    }
}
