using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heropPlayer;
    RaycastHit hit;

    private void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                }
                else if(hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                }
            }
        }
    }
}
