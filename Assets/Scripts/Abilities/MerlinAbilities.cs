using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerlinAbilities : MonoBehaviour
{

    /*
    Ability names:
    Passive: Mana Reload: Every 3rd ability, restores health to Merlin
    Ability 1: Fireball: The fireballs of normal attacks becomes bigger and stronger
    Ability 2: Energy Drain: Damage all enemies in the area and restores mana based on enemies hit
    Ability 3: Spike Terrain: Summon a spiky terrain underneath and stun enemies for 1 second
    Ability 4: Meteor Shower: Deals massive amount of damage in massive aoe
    */
    MerlinAbilityManager abilityManager;
    PlayerStats statsScript;

    public Vector3 mousePosition;
    public Transform player;
    public bool spellLock;

    [Header("Ability 1")]

    public Image abilityImage1;
    public float abilityMana1;
    public float cooldown1;
    public KeyCode ability1;

    private bool isCooldown1 = false;

    [Header("Ability 2")]

    public Image abilityImage2;
    public float abilityMana2;
    public float cooldown2;
    public KeyCode ability2;

    private bool isCooldown2 = false;
    private bool toggle2 = false;

    [Header("Ability 2 Components")]
    public Image skillshot2;
    public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 posUp;
    public float maxAbility2Distance;

    [Header("Ability 3")]

    public Image abilityImage3;
    public float abilityMana3;
    public float cooldown3;
    public KeyCode ability3;

    private bool isCooldown3 = false;
    private bool toggle3 = false;

    [Header("Ability 3 Components")]
    public Canvas ability3Canvas;
    public Image skillshot3;

    [Header("Ability 4")]

    public Image abilityImage4;
    public float abilityMana4;
    public float cooldown4;
    public KeyCode ability4;

    private bool isCooldown4 = false;
    private bool toggle4 = false;

    [Header("Ability4")]
    public Canvas ability4Canvas;
    public Image skillshot4;

    void Start()
    {
        //Start game with cooldowns at 0
        spellLock = false;
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;

        skillshot2.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
        skillshot3.GetComponent<Image>().enabled = false;
        skillshot4.GetComponent<Image>().enabled = false;

        abilityManager = GetComponent<MerlinAbilityManager>();
        statsScript = GetComponent<PlayerStats>();
    }

    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        Ability4();


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ability 2 inputs (Doesn't do anything)
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    if (hit.collider.gameObject != gameObject)
        //    {
        //        posUp = new Vector3(hit.point.x, 10f, hit.point.z);
        //        position = hit.point;
        //    }
        //    else
        //    {
        //        Debug.Log("hovering over player");
        //    }
        //}

        //Ability 3 inputs
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Player"))
            {
                mousePosition = new Vector3(0f, 0f, 0f);
            }
            else
            {
                mousePosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }

        //Ability 2 canvas inputs
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxAbility2Distance);
        var hitPosDir = (hit.point - transform.position).normalized;
        var newHitPos = transform.position + hitPosDir * distance;
        //ability2Canvas.transform.position = newHitPos;

        ability2Canvas.transform.position = new Vector3(newHitPos.x, 0.6f, newHitPos.z);

        //Ability 3 canvas inputs
        Quaternion transRot = Quaternion.LookRotation(mousePosition - player.transform.position);
        transRot.eulerAngles = new Vector3(90, transRot.eulerAngles.y, 0);
        ability3Canvas.transform.rotation = Quaternion.Lerp(transRot, ability3Canvas.transform.rotation, 0f);


    }

    void Ability1()
    {
        if (Input.GetKey(ability1) && !isCooldown1 && abilityMana1 <= statsScript.mana)
        {
            //Disable all other UI


            abilityManager.ActivateAbility1();
            statsScript.mana -= abilityMana1;
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }

        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }

    void Ability2()
    {
        if (!spellLock)
        {
            if (Input.GetKey(ability2) && !isCooldown2 && abilityMana2 <= statsScript.mana)
            {
                indicatorRangeCircle.GetComponent<Image>().enabled = true;
                skillshot2.GetComponent<Image>().enabled = true;

                //Disable all other UI
                toggle3 = false;
                toggle4 = false;
            }

            if (Input.GetKeyDown(ability2) && !isCooldown2 && abilityMana2 <= statsScript.mana)
            {
                toggle2 = !toggle2;
            }
            if (toggle2)
            {
                indicatorRangeCircle.GetComponent<Image>().enabled = true;
                skillshot2.GetComponent<Image>().enabled = true;
            }
            if (!toggle2)
            {
                indicatorRangeCircle.GetComponent<Image>().enabled = false;
                skillshot2.GetComponent<Image>().enabled = false;
            }



            if (skillshot2.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
            {
                abilityManager.ActivateAbility2();
                statsScript.mana -= abilityMana2;
                isCooldown2 = true;
                toggle2 = false;
                abilityImage2.fillAmount = 1;
            }
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            skillshot2.GetComponent<Image>().enabled = false;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Ability3()
    {
        if (!spellLock)
        {
            if (Input.GetKey(ability3) && !isCooldown3 && abilityMana3 <= statsScript.mana)
            {
                skillshot3.GetComponent<Image>().enabled = true;

                //Disable all other UI
                toggle2 = false;
                toggle4 = false;

            }

            if (Input.GetKeyDown(ability3) && !isCooldown3 && abilityMana3 <= statsScript.mana)
            {
                toggle3 = !toggle3;
            }
            if (toggle3)
            {
                skillshot3.GetComponent<Image>().enabled = true;
            }
            if (!toggle3)
            {
                skillshot3.GetComponent<Image>().enabled = false;
            }

            if (skillshot3.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
            {
                abilityManager.ActivateAbility3();
                statsScript.mana -= abilityMana3;
                isCooldown3 = true;
                toggle3 = false;
                abilityImage3.fillAmount = 1;
            }
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            skillshot3.GetComponent<Image>().enabled = false;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    void Ability4()
    { 
        if (!spellLock)
        {
            if (Input.GetKey(ability4) && !isCooldown4 && abilityMana4 <= statsScript.mana)
            {
                skillshot4.GetComponent<Image>().enabled = true;
                //Disable all other UI
                toggle2 = false;
                toggle3 = false;
            }

            if (skillshot4.GetComponent<Image>().enabled == true && Input.GetMouseButton(0))
            {
                abilityManager.ActivateAbility4();
                statsScript.mana -= abilityMana4;
                isCooldown4 = true;
                toggle4 = false;
                abilityImage4.fillAmount = 1;
            }

            if (Input.GetKeyDown(ability4) && !isCooldown4 && abilityMana4 <= statsScript.mana)
            {
                toggle4 = !toggle4;
            }
            if (toggle4)
            {
                skillshot4.GetComponent<Image>().enabled = true;
            }
            if (!toggle4)
            {
                skillshot4.GetComponent<Image>().enabled = false;
            }
        }


        if (isCooldown4)
        {
            abilityImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;

            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
}
