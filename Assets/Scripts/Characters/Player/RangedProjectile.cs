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

    private void Start()
    {
        stopProjectile = false;
    }

    void Update()
    {
        if (target)
        {
            Debug.Log("target found");
            if (target == null)
            {
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);


            if (!stopProjectile)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 0.2f)
                {
                    if (targetType == "Minion")
                    {
                        Debug.Log("minion hit");
                        target.GetComponent<EnemyStats>().health -= damage;
                        stopProjectile = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
