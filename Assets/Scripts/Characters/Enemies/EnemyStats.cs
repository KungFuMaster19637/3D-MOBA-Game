using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header ("Enemy Stats")]
    public float maxHealth;
    public float health;
    public float attackDamage;
    public float attackTime;
    public float attackRange;
    public float defence;
    public float expValue;
    public bool isDead;

    [Header ("Components")]
    public GameObject healthBar;

    public bool giveExpOnce;
    public bool dieOnce;

    private GameObject player;
    public Animator anim;
    private NavMeshAgent agent;
    protected HeroCombat heroCombat;

    [Header ("DragonBoss")]
    [SerializeField] private QuestGiver dragonSlayer;
    [SerializeField] private DragonBossSounds dragonBossSounds;


    /*
    Enemy monster stats:

    Blueboar:
    Health: 200
    Attack Damage: 20
    Attack Time: 2
    Attack Range: 2.5
    Defence: 10
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
    Health: ???
    Attack Damage: ???
    Attack Time: ???
    Attack Range: ???
    Defence: ???
    Exp Value: ???
    Return Range: ???
    Aggro Range: ???

    */
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heroCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        isDead = false;

        dieOnce = false;
        giveExpOnce = false;
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            if (!giveExpOnce)
            {
                Debug.Log("Exp gained: " + expValue);
                player.GetComponent<LevelUpStats>().SetExperience(expValue);
                MoneyDisplay.AddMoney(5);
                giveExpOnce = true;
            }
            if (this.CompareTag("Boss"))
            {
                dragonSlayer.dragonQuest.isDragonSlain = true;
                StartCoroutine(dragonBossSounds.PlayDragonDead());
                StartCoroutine(PlayBossDeathAnimation());


            }
            if (this.CompareTag("Enemy") && !dieOnce)
            {
                dieOnce = true;
                StartCoroutine(PlayDeathAnimation());
            }
        }
    }

    protected virtual IEnumerator PlayDeathAnimation()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<EnemyCombat>().enabled = false;
        agent.enabled = false;
        heroCombat.targetedEnemy = null;
        anim.SetBool("IsDying", true);
        healthBar.SetActive(false);
        //agent.isStopped = true;
        isDead = true;

        yield return new WaitForSeconds(2f);
        //anim.SetBool("IsDying", false);

        gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    private IEnumerator PlayBossDeathAnimation()
    {
        anim.SetBool("IsDying", true);
        healthBar.SetActive(false);

        yield return new WaitForSeconds(3.5f);
        anim.SetBool("IsDying", false);
        Destroy(gameObject);
    }

}
