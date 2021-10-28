using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestGiver : MonoBehaviour
{
    public WheatShortage wheatQuest;
    public IronMiner ironQuest;
    public DragonSlayer dragonQuest;

    public GameObject player;

    public GameObject questWindow;

    //Activation 
    public void ActivateWheatQuest()
    {
        wheatQuest.isActive = true;
    }
    public void ActivateIronQuest()
    {
        ironQuest.isActive = true;
    }
    public void ActivateDragonQuest()
    {
        dragonQuest.isActive = true;
    }

    //Quest Progress
    public void WheatAcquired()
    {
        wheatQuest.wheatAmount++;
        CheckWheat();
    }
    public void IronAcquired()
    {
        ironQuest.ironAmount++;
        CheckIron();
    }

    public void DragonSlain()
    {
        dragonQuest.isDragonSlain = true;
    }

    //Checking quest completion
    public void CheckWheat()
    {
        if (wheatQuest.wheatAmount >= wheatQuest.wheatRequired)
        {
            wheatQuest.completed = true;
        }
    }
    public void CheckIron()
    {
        if (ironQuest.ironAmount >= ironQuest.ironRequired)
        {
            ironQuest.completed = true;
        }
    }
    //public void CheckDragon()
    //{

    //}

    //Claim Quest rewards
    public void ClaimWheatReward()
    {
        player.GetComponent<LevelUpStats>().SetExperience(wheatQuest.experienceReward);
        MoneyDisplay.AddMoney(wheatQuest.moneyReward);
    }

    public void ClaimIronReward()
    {
        Debug.Log("getting reward");
        player.GetComponent<LevelUpStats>().SetExperience(ironQuest.experienceReward);
        MoneyDisplay.AddMoney(ironQuest.moneyReward);
    }

    public void ClaimDragonReward()
    {

    }


    //public void OpenQuestWindow()
    //{
    //    questWindow.SetActive(true);
    //    titleText.text = quest.title;
    //    descriptionText.text = quest.description;
    //    experienceText.text = quest.experienceReward.ToString();
    //    goldText.text = quest.goldReward.ToString();
    //}

    //public void AcceptQuest()
    //{
    //    questWindow.SetActive(false);
    //    quest.isActive = true;
    //    player.playerQuests.Add(quest);
    //}
}
