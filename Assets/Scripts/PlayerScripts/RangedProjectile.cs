using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{

    public float damage;
    public GameObject target;

    public bool targetSet;
    public string targetType;
    public float velocity;
    public bool stopProjectile;


    void Update()
    {
        if (target)
        {
            if (target == null)
            {
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);

            if (!stopProjectile)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
                {
                    if (targetType == "Minion")
                    {
                        target.GetComponent<EnemyStats>().health -= damage;
                        stopProjectile = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
