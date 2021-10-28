using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragonSlayer : Quest
{
    public bool isDragonSlain;
    private void Start()
    {
        questName = "Dragon Slayer";
        description = "Kill the dragon in the cave";
        experienceReward = 20;
        moneyReward = 50;
    }

    public static void Completed()
    {

    }
}
