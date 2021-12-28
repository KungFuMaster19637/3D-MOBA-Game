using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerlinAbilityManager : MonoBehaviour
{
    NavMeshAgent agent;
    HeroCombat heroCombatScript;
    PlayerStats playerStatsScript;
    Animator anim;
    Movement movement;

    MerlinAbilities merlinAbilities;

    [Header("Ability Effects")]
    public ParticleSystem energyDrain;
    public ParticleSystem spikeTerrain;
    public GameObject coneDetection;
    public List<GameObject> enemiesDetected = new List<GameObject>();
    public ParticleSystem meteorShower;

    [Header("Passive")]
    private int passiveCount;
    [Header("Ability 1")]
    public float duration1;
    public float totalDuration1;
    public float attackBuff;

    private bool ability1Pressed = false;
    private Vector3 objectScale;

    [Header("Ability 2")]
    private bool ability2Pressed = false;

    [Header("Ability 3")]
    public float spikeTerrainDamage;
    public float spikeTerrainMultiplier;
    private bool ability3Pressed = false;

    [Header("Ability 4")]
    public float meteorShowerDamage;
    public float meteorShowerMultiplier;
    public float meteorShowerRange;
    
    private bool ability4Pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
        playerStatsScript = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        merlinAbilities = GetComponent<MerlinAbilities>();

        objectScale = new Vector3(0.25f, 0.25f, 0.25f);
        ResetPrefabScaling();
    }

    // Update is called once per frame
    void Update()
    {
        if (ability1Pressed)
        {
            duration1 += Time.deltaTime;
        }
        if (duration1 >= totalDuration1)
        {
            Debug.Log("deactivating");
            DeactivateAbility1();
        }
    }

    private void ResetPrefabScaling()
    {
        heroCombatScript.projPrefab.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        heroCombatScript.projPrefab.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        heroCombatScript.projPrefab.transform.GetChild(1).localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    private float SpellCalculator(float incomingDamage, GameObject enemy)
    {
        float totalDamage;
        totalDamage = 100 / (100 + enemy.GetComponent<EnemyStats>().defence) * incomingDamage;
        Debug.Log("Spell damage: " + totalDamage);
        return totalDamage;
    }
    public void ActivateAbility1()
    {
        ability1Pressed = true;
        playerStatsScript.attackDamage += attackBuff;

        heroCombatScript.projPrefab.transform.localScale += objectScale;
        heroCombatScript.projPrefab.transform.GetChild(0).localScale += objectScale;
        heroCombatScript.projPrefab.transform.GetChild(1).localScale += objectScale;
    }

    public void DeactivateAbility1()
    {
        ability1Pressed = false;
        playerStatsScript.attackDamage -= attackBuff;
        duration1 = 0;

        //heroCombatScript.projPrefab.transform.localScale -= objectScale;
        //heroCombatScript.projPrefab.transform.GetChild(0).localScale -= objectScale;
        //heroCombatScript.projPrefab.transform.GetChild(1).localScale -= objectScale;

        ResetPrefabScaling();

    }
    public void ActivateAbility2()
    {
        Instantiate(energyDrain);
    }
    public void ActivateAbility3()
    {
        Quaternion spikeRotation = Quaternion.LookRotation(merlinAbilities.mousePosition - transform.position);
        StartCoroutine(SpikeTerrainInterval(spikeRotation));

    }
    public void ActivateAbility4()
    {
        Instantiate(meteorShower, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);
        StartCoroutine(MeteorShowerInterval());
    }
    public void SpikeTerrainDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyStats>().health -= SpellCalculator(spikeTerrainDamage + playerStatsScript.spellPower * spikeTerrainMultiplier, enemy);
    }
    private IEnumerator SpikeTerrainInterval(Quaternion rotation)
    {
        anim.SetBool("Skill", true);
        agent.isStopped = true;
        movement.canMove = false;

        merlinAbilities.spellLock = true;

        yield return new WaitForSeconds(1f);

        merlinAbilities.spellLock = false;

        Instantiate(spikeTerrain, transform.position, rotation);
        Instantiate(coneDetection, transform.position, rotation);
        anim.SetBool("Skill", false);
        agent.isStopped = false;
        movement.canMove = true;
    }
    private IEnumerator MeteorShowerInterval()
    {
        anim.SetBool("Skill", true);
        agent.isStopped = true;
        movement.canMove = false;

        merlinAbilities.spellLock = true;

        //Calculating damage in area
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, meteorShowerRange);
        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                for (int i = 0; i < 6; i++)
                {
                    enemy.GetComponent<EnemyStats>().health -= SpellCalculator(meteorShowerDamage + playerStatsScript.spellPower * meteorShowerMultiplier, enemy.gameObject);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        yield return new WaitForSeconds(1f);

        merlinAbilities.spellLock = false;

        anim.SetBool("Skill", false);
        agent.isStopped = false;
        movement.canMove = true;
    }

}
