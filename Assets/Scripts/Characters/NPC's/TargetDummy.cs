using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : EnemyStats
{
    private bool respawnOnce = true;


    protected override void Update()
    {
        if (health <= 0 && respawnOnce)
        {
            heroCombat.targetedEnemy = null;
            StartCoroutine(PlayDeathAnimation());
            respawnOnce = false;
        }
        
    }

    protected override IEnumerator PlayDeathAnimation()
    {
        anim.SetBool("IsDying", true);
        healthBar.SetActive(false);

        yield return new WaitForSeconds(2f);
        StartCoroutine(RespawnDummy());
    }

    private IEnumerator RespawnDummy()
    {
        anim.SetBool("IsDying", false);

        yield return new WaitForSeconds(1f);

        health = maxHealth;
        healthBar.SetActive(true);
        respawnOnce = true;

        //yield return new WaitForSeconds(3f);
        yield return null;
    }
}
