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

    public bool wheatQuestCompleted = false;
    public bool ironQuestCompleted = false;
    public GameObject dragonQuestActivate;

    public GameObject player;

    public GameObject questWindow;
    public GameObject notificationSign;

    public ItemDisplay itemDisplay;

    private int wheatNotificationCount = 0;
    private int ironNotificationCount = 0;

    private void Update()
    {
        //Notification sign turn on/off
        if (wheatQuest.oneTimeNotification || ironQuest.oneTimeNotification || dragonQuest.oneTimeNotification)
        {
            notificationSign.SetActive(true);
        }
        else
        {
            notificationSign.SetActive(false);
        }

        if (wheatQuest.completed && ironQuest.completed)
        {
            dragonQuestActivate.SetActive(true); 
            if (!dragonQuest.isActive)
            {
                dragonQuest.oneTimeNotification = true;
            }
            else
            {
                dragonQuest.oneTimeNotification = false;
            }
        }
        CheckWheat();
        CheckIron();

    }
    //Notification
    public void NotificationWheatQuest()
    {
        if (wheatNotificationCount > 0)
        {
            wheatQuest.oneTimeNotification = false;
        }
        else
        {
            wheatQuest.oneTimeNotification = true;
            wheatNotificationCount++;
        }
    }

    public void NotificationIronQuest()
    {
        if (ironNotificationCount > 0)
        {
            ironQuest.oneTimeNotification = false;
        }
        else
        {
            ironQuest.oneTimeNotification = true;
            ironNotificationCount++;
        }
    }

    //Needed special code to activate
    //public void NotificationDragonQuest()
    //{
    //    if (dragonNotificationCount > 0)
    //    {
    //        dragonQuest.oneTimeNotification = false;
    //    }
    //    else
    //    {
    //        dragonQuest.oneTimeNotification = true;
    //        dragonNotificationCount++;
    //    }
    //}


    //Activation 
    public void ActivateWheatQuest()
    {
        wheatQuest.isActive = true;
        wheatQuest.oneTimeNotification = false;
    }
    public void ActivateIronQuest()
    {
        ironQuest.isActive = true;
        ironQuest.oneTimeNotification = false;

    }
    public void ActivateDragonQuest()
    {
        dragonQuest.isActive = true;
        dragonQuest.oneTimeNotification = false;
    }

    //Quest Progress
    public void WheatAcquired()
    {
        wheatQuest.wheatAmount++;
        //CheckWheat();
    }
    public void IronAcquired()
    {
        ironQuest.ironAmount++;
        //CheckIron();
    }

    public void DragonSlain()
    {
        dragonQuest.isDragonSlain = true;
    }

    //Checking quest completion
    public void CheckWheat()
    {
        if (wheatQuest.wheatAmount >= wheatQuest.wheatRequired && wheatQuest.isActive)
        {
            wheatQuest.completed = true;
        }
    }
    public void CheckIron()
    {
        if (ironQuest.ironAmount >= ironQuest.ironRequired && ironQuest.isActive)
        {
            ironQuest.completed = true;
        }
    }
    public void CheckDragon()
    {
        if (dragonQuest.isDragonSlain == true)
        {
            dragonQuest.completed = true;
        }
    }

    //Claim Quest rewards

    public void ClaimWheatReward()
    {
        player.GetComponent<LevelUpStats>().SetExperience(wheatQuest.experienceReward);
        MoneyDisplay.AddMoney(wheatQuest.moneyReward);
        for (int i = 0; i < 4; i++)
        {
            itemDisplay.UseItem(5);
        }
        wheatQuestCompleted = true;
    }

    public void ClaimIronReward()
    {
        Debug.Log("getting reward");
        player.GetComponent<LevelUpStats>().SetExperience(ironQuest.experienceReward);
        MoneyDisplay.AddMoney(ironQuest.moneyReward);
        for (int i = 0; i < 10; i++)
        {
            itemDisplay.UseItem(6);
        }
        ironQuestCompleted = true;
    }

    public void ClaimDragonReward()
    {

    }
}
