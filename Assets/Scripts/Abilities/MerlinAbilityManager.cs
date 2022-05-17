using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerlinAbilityManager : MonoBehaviour
{
    NavMeshAgent agent;
    HeroCombat heroCombatScript;
    PlayerStats statsScript;
    Animator anim;
    Movement movement;

    MerlinAbilities merlinAbilities;
    AudioSource fireballVFX;
    [SerializeField] private AudioClip fireballClip;

    [Header("Ability Effects")]
    public ParticleSystem energyDrain;
    public ParticleSystem spikeTerrain;
    public GameObject coneDetection;
    public List<GameObject> enemiesDetected = new List<GameObject>();
    public ParticleSystem meteorShower;

    [Header("Passive")]
    public int passiveCount;
    public float passiveHealAmount;
    public float passiveHealMultiplier;

    [Header("Ability 1")]
    public float duration1;
    public float totalDuration1;
    public float fireballSpeedBuff;
    public float attackBuff;
    public GameObject ability1Icon;

    private bool ability1Pressed = false;
    private Vector3 objectScale;

    [Header("Ability 2")]
    public float energyDrainHeal;
    public float energyDrainHealMultiplier;
    public float energyDrainDamage;
    public float energyDrainDamageMultiplier;
    public float energyDrainRange;
    private bool ability2Pressed = false;

    [Header("Ability 3")]
    public float spikeTerrainDamage;
    public float spikeTerrainMultiplier;
    public float spikeTerrainStunDuration;
    private bool ability3Pressed = false;

    [Header("Ability 4")]
    public float meteorShowerDamage;
    public float meteorShowerMultiplier;
    public float meteorShowerRange;
    
    private bool ability4Pressed = false;

    void Start()
    {
        fireballVFX = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
        statsScript = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        merlinAbilities = GetComponent<MerlinAbilities>();

        objectScale = new Vector3(0.25f, 0.25f, 0.25f);
        ResetPrefabScaling();
    }

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
        MerlinPassive();
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

    private void MerlinPassive()
    {
        if (passiveCount == 3)
        {
            if (statsScript.health + passiveHealAmount > statsScript.maxHealth)
            {
                statsScript.health = statsScript.maxHealth;
            }
            else if (statsScript.health + passiveHealAmount <= statsScript.maxHealth)
            {
                statsScript.health += passiveHealAmount + statsScript.spellPower * passiveHealMultiplier;
            }
            passiveCount = 0;
        }

    }

    public void FireballVFX()
    {
        fireballVFX.PlayOneShot(fireballClip, 0.6f);
    }
    public void ActivateAbility1()
    {
        ability1Icon.SetActive(true);
        ability1Pressed = true;
        statsScript.attackDamage += attackBuff;
        heroCombatScript.projPrefab.GetComponent<RangedProjectile>().velocity += fireballSpeedBuff;

        heroCombatScript.projPrefab.transform.localScale += objectScale;
        heroCombatScript.projPrefab.transform.GetChild(0).localScale += objectScale;
        heroCombatScript.projPrefab.transform.GetChild(1).localScale += objectScale;

        passiveCount++;
    }

    public void DeactivateAbility1()
    {
        ability1Icon.SetActive(false);
        ability1Pressed = false;
        statsScript.attackDamage -= attackBuff;
        heroCombatScript.projPrefab.GetComponent<RangedProjectile>().velocity -= fireballSpeedBuff;
        duration1 = 0;

        ResetPrefabScaling();

    }
    public void ActivateAbility2()
    {
        Vector3 energyPosition = merlinAbilities.ability2Canvas.transform.position;
        passiveCount++;
        StartCoroutine(EnergyDrainInterval(energyPosition));
    }
    public void ActivateAbility3()
    {
        Quaternion spikeRotation = Quaternion.LookRotation(merlinAbilities.mousePosition - transform.position);
        passiveCount++;
        StartCoroutine(SpikeTerrainInterval(spikeRotation));

    }
    public void ActivateAbility4()
    {
        passiveCount++;
        StartCoroutine(MeteorShowerInterval());
    }

    private IEnumerator EnergyDrainInterval(Vector3 energyPosition)
    {
        anim.SetBool("Skill", true);
        agent.isStopped = true;
        movement.canMove = false;

        merlinAbilities.spellLock = true;

        yield return new WaitForSeconds(1f);

        Instantiate(energyDrain, energyPosition, Quaternion.identity);

        yield return new WaitForSeconds(spikeTerrainStunDuration);

        Collider[] enemiesInRange = Physics.OverlapSphere(energyPosition, energyDrainRange);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyStats>().health -= SpellCalculator(energyDrainDamage + statsScript.spellPower * energyDrainDamageMultiplier, enemy.gameObject);
                statsScript.health += (energyDrainHeal + statsScript.spellPower * energyDrainHealMultiplier); 
            }
        }

        merlinAbilities.spellLock = false;

        anim.SetBool("Skill", false);
        agent.isStopped = false;
        movement.canMove = true;
    }

    public void SpikeTerrainDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyStats>().health -= SpellCalculator(spikeTerrainDamage + statsScript.spellPower * spikeTerrainMultiplier, enemy);
    }

    public IEnumerator SpikeTerrainStun(GameObject enemy)
    {

        float tempSpeed = enemy.GetComponent<NavMeshAgent>().speed;
        enemy.GetComponent<NavMeshAgent>().speed = 0;

        Debug.Log(tempSpeed);
        yield return new WaitForSeconds(1f);
        enemy.GetComponent<NavMeshAgent>().speed = tempSpeed;
        Debug.Log(enemy.GetComponent<NavMeshAgent>().speed + tempSpeed);
        yield return null;
    }
    private IEnumerator SpikeTerrainInterval(Quaternion spikeRotation)
    {
        anim.SetBool("Skill", true);
        agent.isStopped = true;
        movement.canMove = false;

        merlinAbilities.spellLock = true;

        yield return new WaitForSeconds(1f);

        merlinAbilities.spellLock = false;

        Instantiate(spikeTerrain, transform.position, spikeRotation);
        GameObject coneCollider = Instantiate(coneDetection, transform.position, spikeRotation);
        anim.SetBool("Skill", false);
        agent.isStopped = false;
        movement.canMove = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(coneCollider);
    }
    private IEnumerator MeteorShowerInterval()
    {
        anim.SetBool("Skill", true);
        agent.isStopped = true;
        movement.canMove = false;

        merlinAbilities.spellLock = true;

        yield return new WaitForSeconds(1f);

        Instantiate(meteorShower, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);

        //Calculating damage in area
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, meteorShowerRange);
        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                for (int i = 0; i < 6; i++)
                {
                    enemy.GetComponent<EnemyStats>().health -= SpellCalculator(meteorShowerDamage + statsScript.spellPower * meteorShowerMultiplier, enemy.gameObject);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        merlinAbilities.spellLock = false;

        anim.SetBool("Skill", false);
        agent.isStopped = false;
        movement.canMove = true;
    }

}
