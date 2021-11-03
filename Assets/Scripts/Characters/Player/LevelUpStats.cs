using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpStats : MonoBehaviour
{

    public int level = 1;
    public float experience { get; private set; }
    public TMP_Text lvlText;
    //public Text lvlText;
    public Image expBarImage;

    private PlayerStats playerStats;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    public static int ExpNeedToLvlUp(int currentLevel)
    {
        if (currentLevel == 0)
        {
            return 0;
        }
        return (currentLevel * currentLevel + currentLevel) * 5;
    }

    public void SetExperience(float exp)
    {
        experience += exp;

        float expNeeded = ExpNeedToLvlUp(level);
        float previousExperience = ExpNeedToLvlUp(level - 1);

        if (experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExperience = ExpNeedToLvlUp(level - 1);
        }

        expBarImage.fillAmount = (experience - previousExperience) / (expNeeded - previousExperience);

        if (expBarImage.fillAmount == 1)
        {
            expBarImage.fillAmount = 0;
        }
    }

    public void LevelUp()
    {
        level++;
        playerStats.attackDamage += 5;
        playerStats.maxHealth += 30;
        playerStats.healthRegeneration += 3f;
        playerStats.health += 30;
        playerStats.maxMana += 15;
        playerStats.mana += 15;
        playerStats.manaRegeneration += 1.5f;
        playerStats.defence += 5;
        playerStats.spellPower += 5;


        lvlText.text = level.ToString("");
    }
}
