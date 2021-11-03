using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour
{
    //Currently not used yet, Work In Progress

    Animator anim;
    RaycastHit hit;
    Movement moveScript;

    [Header("Fireball Ability")]
    public Image fireBallImage;
    public float cooldown = 10f;
    bool isCooldown = false;
    public KeyCode ability2;
    bool canFireball = true;
    public GameObject projPrefab;
    public Transform projSpawnPoint;

    [Header("Fireball Ability Inputs")]
    public Image indicatorRangeCircle;
    //private Vector3 posUp;
    public float maxAbilityDistance;

    [SerializeField]
    public GameObject targetedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        fireBallImage.fillAmount = 0;

        indicatorRangeCircle.GetComponent<Image>().enabled = false;

        moveScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FireballAbility();
    }

    public void FireballAbility()
    {
        if (Input.GetKey(ability2) && isCooldown == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
        }


        //Click on enemy
        if (indicatorRangeCircle.GetComponent<Image>().enabled == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.GetComponent<Targetable>() != null && 
                        hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
                    {
                        targetedEnemy = hit.collider.gameObject;
                    }
                    else if (hit.collider.gameObject.GetComponent<Targetable>() == null)
                    {
                        targetedEnemy = null;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                targetedEnemy = null;
            }
        }

        //Moving and shooting fireball
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) >= maxAbilityDistance)
            {
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = maxAbilityDistance - 0.5f;
            }
            else
            {
                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref moveScript.rotateVelocity,
                    0.075f * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);

                moveScript.agent.SetDestination(transform.position);
                moveScript.agent.stoppingDistance = 0;


                //Spawn Fireball
                if (canFireball)
                {
                    isCooldown = true;
                    fireBallImage.fillAmount = 1;

                    StartCoroutine(FireBall());
                }
            }
        }

        //Ability goes to Cooldown
        if (isCooldown)
        {
            fireBallImage.fillAmount -= 1 / cooldown * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;

            if (fireBallImage.fillAmount <= 0)
            {
                fireBallImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    IEnumerator FireBall()
    {
        canFireball = false;
        anim.SetBool("Fireball", true);

        yield return new WaitForSeconds(1.5f);

        if (targetedEnemy == null)
        {
            anim.SetBool("Fireball", false);
            canFireball = true;
        }
    }

    public void FireBallAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                SpawnRangedProj("Minion", targetedEnemy);
            }
        }

        targetedEnemy = null;
        canFireball = true;
    }

    void SpawnRangedProj(string typeOfEnemy, GameObject targetedEnemyObj)
    {
        Instantiate(projPrefab, projSpawnPoint.transform.position, Quaternion.identity);

        if (typeOfEnemy == "Minion")
        {
            projPrefab.GetComponent<RangedProjectile>().targetType = typeOfEnemy;

            projPrefab.GetComponent<RangedProjectile>().target = targetedEnemyObj;
            projPrefab.GetComponent<RangedProjectile>().targetSet = true;
        }
    }
}
