using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest
{

    /*
    Quest names:
    Wheat Shortage: The baker needs some wheat, go collect it somewhere. 
    Iron Miner: The Blacksmith wants you to go out in the wild and look for an iron deposit, and collect some of it
    Dragon Slayer: Defeat the dragon
    */
    public bool oneTimeNotification;
    public bool isActive;
    public string questName;
    public string description;
    public float experienceReward;
    public int moneyReward;
    public bool completed;


}
