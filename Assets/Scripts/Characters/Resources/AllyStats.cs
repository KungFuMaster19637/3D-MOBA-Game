using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllyStats : PlayerStats
{
    [SerializeField] private TMP_Text childDialogue;

    private string childPain;
    private string childHealed;

    protected override void Start()
    {
        childPain = "Someone please heal me";
        childHealed = "Thank you very much";
    }
    protected override void Update()
    {
        childDialogue.text = childPain;
        if (health >= maxHealth)
        {
            childDialogue.text = childHealed;
        }
    }
}
