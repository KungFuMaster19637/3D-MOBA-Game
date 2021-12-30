using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeCollider : MonoBehaviour
{
    private MerlinAbilityManager abilityManager;

    private void Start()
    {
        abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<MerlinAbilityManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            abilityManager.SpikeTerrainDamage(other.gameObject);
            StartCoroutine(abilityManager.SpikeTerrainStun(other.gameObject));
        }
    }
}
