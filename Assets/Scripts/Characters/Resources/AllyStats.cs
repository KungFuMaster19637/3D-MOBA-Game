using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllyStats : PlayerStats
{
    [SerializeField] private TMP_Text childDialogue;

    private string childPain;
    private string childHealed;
    private bool healedOnce;

    protected override void Start()
    {
        childPain = "Someone please heal me";
        childHealed = "Thank you very much";
        healedOnce = false;
    }
    protected override void Update()
    {
        childDialogue.text = childPain;
        if (health >= maxHealth)
        {
            childDialogue.text = childHealed;
            if (!healedOnce)
            {
                AllyExperience();
                healedOnce = true;
            }
        }
    }

    private void AllyExperience()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelUpStats>().SetExperience(5);
    }
}
