using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArthurAbilities: MonoBehaviour
{

    /*
    Ability names:
    Passive: Divine Blessing: Every 2nd hit, restore 5 health points
    Ability 1: Charge Forward: Gain movement speed and attack
    Ability 2: Lay On Hands: Heal allies and yourself and get increased health regeneration
    Ability 3: Pridwen: Block all incoming damage
    Ability 4: Excalibur: Increase attack speed and range
    */
    ArthurAbilityManager abilityManager;
    PlayerStats statsScript;

    [Header("Ability 1")]

    public Image abilityImage1;
    public float abilityMana1;
    public float cooldown1;
    private bool isCooldown1 = false;
    public KeyCode ability1;

    [Header("Ability 2")]

    public Image abilityImage2;
    public float abilityMana2;
    public float cooldown2;
    private bool isCooldown2 = false;
    public KeyCode ability2;
    private bool toggle2 = false;

    //Ability 2 input variables
    public Image targetCircle;
    //public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 posUp;
    //public float maxAbility2Distance;

    [Header("Ability 3")]

    public Image abilityImage3;
    public float abilityMana3;
    public float cooldown3;
    private bool isCooldown3 = false;
    public KeyCode ability3;

    [Header("Ability 4")]

    public Image abilityImage4;
    public float abilityMana4;
    public float cooldown4;
    private bool isCooldown4 = false;
    public KeyCode ability4;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;

        targetCircle.GetComponent<Image>().enabled = false;
        abilityManager = GetComponent<ArthurAbilityManager>();
        statsScript = GetComponent<PlayerStats>();
    }

    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        Ability4();
    }

    void Ability1()
    {
        if (Input.GetKey(ability1) && !isCooldown1 && abilityMana1 <= statsScript.mana)
        {
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
        if (Input.GetKeyDown(ability2) && !isCooldown2 && abilityMana2 <= statsScript.mana)
        {
            toggle2 = !toggle2;
        }
        if (toggle2)
        {
            targetCircle.GetComponent<Image>().enabled = true;
            //Disable other UI

        }
        if (!toggle2)
        {
            targetCircle.GetComponent<Image>().enabled = false;
        }



        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            abilityManager.ActivateAbility2();
            statsScript.mana -= abilityMana2;
            isCooldown2 = true;
            toggle2 = false;
            abilityImage2.fillAmount = 1;
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            //Disable UI again
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Ability3()
    {

        if (Input.GetKey(ability3) && !isCooldown3 && abilityMana3 <= statsScript.mana)
        {
            abilityManager.ActivateAbility3();
            statsScript.mana -= abilityMana3;
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    void Ability4()
    {
        if (Input.GetKey(ability4) && !isCooldown4 && abilityMana4 <= statsScript.mana)
        {
            abilityManager.ActivateAbility4();
            statsScript.mana -= abilityMana4;
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
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