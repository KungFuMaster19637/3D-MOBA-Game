using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{

    private float damage;
    public GameObject target;
    private HeroCombat heroCombatScript;
    private PlayerStats playerStatsScript;

    public bool targetSet;
    public string targetType;
    public float velocity;
    public bool stopProjectile;

    private void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerStatsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damage = playerStatsScript.attackDamage;
        stopProjectile = false;
    }

    void Update()
    {
        if (target)
        {
            if (target == null)
            {
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(target.transform.position.x, target.transform.position.y + 0.5f, target.transform.position.z), velocity * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);


            if (!stopProjectile)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 1f)
                {
                    if (targetType == "Minion")
                    {
                        damage = playerStatsScript.attackDamage;

                        Debug.Log("Player damage: " + damage);
                        target.GetComponent<EnemyStats>().health -= ProjectileDamage(damage);
                        stopProjectile = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private float ProjectileDamage(float incomingDamage)
    {
        float totalDamage;
        totalDamage = 100 / (100 + target.GetComponent<EnemyStats>().defence) * incomingDamage;
        Debug.Log("Projectile damage: " + totalDamage);
        return totalDamage;
    }
}
