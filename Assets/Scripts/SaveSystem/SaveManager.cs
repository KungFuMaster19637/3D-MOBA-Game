using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private Transform playerPosition;

    private LevelUpStats levelUp;
    private ItemDisplay inventory;
    //private MoneyDisplay money; Is a static
    private QuestGiver questGiver;

    private GameObject[] enemies;
    //private EnemyStats[] enemyStats;
    private Transform enemyPosition;

    private GameObject[] npcs;
    private Transform npcPosition;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        levelUp = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelUpStats>();
        inventory = GameObject.FindGameObjectWithTag("ItemDisplay").GetComponent<ItemDisplay>();
        //money = GameObject.FindGameObjectWithTag("MoneyDisplay").GetComponent<MoneyDisplay>();
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();

        //enemies = GameObject.FindGameObjectsWithTag("Enemy");

        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }

    //private void Update()
    //{
    //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //}

    private SaveData CreateSaveDataGameObject()
    {
        SaveData saveData = new SaveData
        {
            maxHealth = playerStats.maxHealth,
            currentHealth = playerStats.health,
            healthRegeneration = playerStats.healthRegeneration,

            maxMana = playerStats.maxMana,
            currentMana = playerStats.mana,
            manaRegeneration = playerStats.manaRegeneration,

            attackDamage = playerStats.attackDamage,
            attackSpeed = playerStats.attackSpeed,
            attackTime = playerStats.attackTime,
            attackRange = playerStats.attackRange,

            spellPower = playerStats.spellPower,
            defence = playerStats.defence,

            playerPositionX = playerPosition.transform.position.x,
            playerPositionY = playerPosition.transform.position.y,
            playerPositionZ = playerPosition.transform.position.z,

            playerExp = levelUp.expBarImage.fillAmount,
            playerLevel = levelUp.level,

            moneyAmount = MoneyDisplay.moneyAmount,

            wheatQuestActive = questGiver.wheatQuest.isActive,
            wheatQuestCompleted = questGiver.wheatQuest.completed,

            ironQuestActive = questGiver.ironQuest.isActive,
            ironQuestCompleted = questGiver.ironQuest.completed,

        };

        //Inventory Items Amount
        for (int i = 0; i < inventory.itemAmount.Length; i++)
        {
            saveData.inventoryAmount[i] = inventory.itemAmount[i];
        }


        //Enemy Positions
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            saveData.enemyMaxHealth.Add(enemy.GetComponent<EnemyStats>().maxHealth);
            saveData.enemyCurrentHealth.Add(enemy.GetComponent<EnemyStats>().health);

            saveData.enemyPositionX.Add(enemy.GetComponent<Transform>().position.x);
            saveData.enemyPositionY.Add(enemy.GetComponent<Transform>().position.y);
            saveData.enemyPositionZ.Add(enemy.GetComponent<Transform>().position.z);
        }

        //NPC Positions
        foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            saveData.npcPositionX.Add(npc.GetComponent<Transform>().position.x);
            saveData.npcPositionY.Add(npc.GetComponent<Transform>().position.y);
            saveData.npcPositionZ.Add(npc.GetComponent<Transform>().position.z);
        }

        return saveData;
    }
    public void SaveToXML()
    {
        SaveData saveData = CreateSaveDataGameObject();
        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("Save"); //<Save>...elements...</Save>

        root.SetAttribute("FileName", "File01");//OPTIONAL

        //Player Info
        XmlElement playerElement = xmlDocument.CreateElement("Player");

        //Player Health
        XmlElement maxHealthElement = xmlDocument.CreateElement("MaxHealth");
        XmlElement currentHealthElement = xmlDocument.CreateElement("CurrentHealth");
        XmlElement healthRegenerationElement = xmlDocument.CreateElement("HealthRegeneration");

        maxHealthElement.InnerText = saveData.maxHealth.ToString();
        currentHealthElement.InnerText = saveData.currentHealth.ToString();
        healthRegenerationElement.InnerText = saveData.healthRegeneration.ToString();

        playerElement.AppendChild(maxHealthElement);
        playerElement.AppendChild(currentHealthElement);
        playerElement.AppendChild(healthRegenerationElement);

        //Player Mana
        XmlElement maxManaElement = xmlDocument.CreateElement("MaxMana");
        XmlElement currentManaElement = xmlDocument.CreateElement("CurrentMana"); 
        XmlElement manaRegenerationElement = xmlDocument.CreateElement("ManaRegeneration");

        maxManaElement.InnerText = saveData.maxMana.ToString();
        currentManaElement.InnerText = saveData.currentMana.ToString();
        manaRegenerationElement.InnerText = saveData.manaRegeneration.ToString();

        playerElement.AppendChild(maxManaElement);
        playerElement.AppendChild(currentManaElement);
        playerElement.AppendChild(manaRegenerationElement);

        //Player Attack 
        XmlElement attackDamageElement = xmlDocument.CreateElement("AttackDamage");
        XmlElement attackSpeedElement = xmlDocument.CreateElement("AttackSpeed");
        XmlElement attackTimeElement = xmlDocument.CreateElement("AttackTime");
        XmlElement attackRangeElement = xmlDocument.CreateElement("AttackRange");

        attackDamageElement.InnerText = saveData.attackDamage.ToString();
        attackSpeedElement.InnerText = saveData.attackSpeed.ToString();
        attackTimeElement.InnerText = saveData.attackTime.ToString();
        attackRangeElement.InnerText = saveData.attackRange.ToString();

        playerElement.AppendChild(attackDamageElement);
        playerElement.AppendChild(attackSpeedElement);
        playerElement.AppendChild(attackTimeElement);
        playerElement.AppendChild(attackRangeElement);

        //Player Position
        XmlElement playerPosXElement = xmlDocument.CreateElement("PlayerPosX");
        XmlElement playerPosYElement = xmlDocument.CreateElement("PlayerPosY");
        XmlElement playerPosZElement = xmlDocument.CreateElement("PlayerPosZ");

        playerPosXElement.InnerText = saveData.playerPositionX.ToString();
        playerPosYElement.InnerText = saveData.playerPositionY.ToString();
        playerPosZElement.InnerText = saveData.playerPositionZ.ToString();

        playerElement.AppendChild(playerPosXElement);
        playerElement.AppendChild(playerPosYElement);
        playerElement.AppendChild(playerPosZElement);

        //Player Level
        XmlElement playerExpElement = xmlDocument.CreateElement("PlayerExp");
        XmlElement playerLevelElement = xmlDocument.CreateElement("PlayerLevel");

        playerExpElement.InnerText = saveData.playerExp.ToString();
        playerLevelElement.InnerText = saveData.playerLevel.ToString();

        playerElement.AppendChild(playerExpElement);
        playerElement.AppendChild(playerLevelElement);

        root.AppendChild(playerElement);

        //Money
        XmlElement moneyElement = xmlDocument.CreateElement("Money");

        moneyElement.InnerText = saveData.moneyAmount.ToString();

        root.AppendChild(moneyElement);

        //Inventory
        XmlElement inventoryElement = xmlDocument.CreateElement("Inventory");

        //Inventory Items
        XmlElement healthPotionElement = xmlDocument.CreateElement("HealthPotion");
        XmlElement manaPotionElement = xmlDocument.CreateElement("ManaPotion");
        XmlElement strengthPotionElement = xmlDocument.CreateElement("StregnthPotion");
        XmlElement defencePotionElement = xmlDocument.CreateElement("DefenPotion");
        XmlElement spellPotionElement = xmlDocument.CreateElement("SpellPotion");
        XmlElement wheatElement = xmlDocument.CreateElement("Wheat");
        XmlElement ironElement = xmlDocument.CreateElement("Iron");
        XmlElement fowlElement = xmlDocument.CreateElement("Fowl");
        XmlElement magicalDonutElement = xmlDocument.CreateElement("MagicalDonut");
        XmlElement riskyPotionElement = xmlDocument.CreateElement("RiskyPotion");
        XmlElement ringOfManaElement = xmlDocument.CreateElement("RingOfMana");
        XmlElement holyOrbElement = xmlDocument.CreateElement("HolyOrb");

        healthPotionElement.InnerText = saveData.inventoryAmount[0].ToString();
        manaPotionElement.InnerText = saveData.inventoryAmount[1].ToString();
        strengthPotionElement.InnerText = saveData.inventoryAmount[2].ToString();
        defencePotionElement.InnerText = saveData.inventoryAmount[3].ToString();
        spellPotionElement.InnerText = saveData.inventoryAmount[4].ToString();
        wheatElement.InnerText = saveData.inventoryAmount[5].ToString();
        ironElement.InnerText = saveData.inventoryAmount[6].ToString();
        fowlElement.InnerText = saveData.inventoryAmount[7].ToString();
        magicalDonutElement.InnerText = saveData.inventoryAmount[8].ToString();
        riskyPotionElement.InnerText = saveData.inventoryAmount[9].ToString();
        ringOfManaElement.InnerText = saveData.inventoryAmount[10].ToString();
        holyOrbElement.InnerText = saveData.inventoryAmount[11].ToString();

        inventoryElement.AppendChild(healthPotionElement);
        inventoryElement.AppendChild(manaPotionElement);
        inventoryElement.AppendChild(strengthPotionElement);
        inventoryElement.AppendChild(defencePotionElement);
        inventoryElement.AppendChild(spellPotionElement);
        inventoryElement.AppendChild(wheatElement);
        inventoryElement.AppendChild(ironElement);
        inventoryElement.AppendChild(fowlElement);
        inventoryElement.AppendChild(magicalDonutElement);
        inventoryElement.AppendChild(riskyPotionElement);
        inventoryElement.AppendChild(ringOfManaElement);
        inventoryElement.AppendChild(holyOrbElement);

        root.AppendChild(inventoryElement);

        //Quest
        XmlElement questElement = xmlDocument.CreateElement("Quest");

        XmlElement wheatQuestActiveElement = xmlDocument.CreateElement("WheatQuestActive");
        XmlElement wheatQuestCompletedElement = xmlDocument.CreateElement("WheatQuestCompleted");
        XmlElement ironQuestActiveElement = xmlDocument.CreateElement("IronQuestActive");
        XmlElement ironQuestCompletedElement = xmlDocument.CreateElement("IronQuestCompleted");

        wheatQuestActiveElement.InnerText = saveData.wheatQuestActive.ToString();
        wheatQuestCompletedElement.InnerText = saveData.wheatQuestCompleted.ToString();
        ironQuestActiveElement.InnerText = saveData.ironQuestActive.ToString();
        ironQuestCompletedElement.InnerText = saveData.ironQuestCompleted.ToString();

        questElement.AppendChild(wheatQuestActiveElement);
        questElement.AppendChild(wheatQuestCompletedElement);
        questElement.AppendChild(ironQuestActiveElement);
        questElement.AppendChild(ironQuestCompletedElement);

        root.AppendChild(questElement);

        //Enemy Info
        XmlElement enemyElement,enemyMaxHealthElement, enemyCurrentHealthElement, enemyPosXElement, enemyPosYElement, enemyPosZElement;

        for (int i = 0; i < saveData.enemyPositionX.Count; i++)
        {
            enemyElement = xmlDocument.CreateElement("Enemy");
            enemyMaxHealthElement = xmlDocument.CreateElement("EnemyMaxHealth");
            enemyCurrentHealthElement = xmlDocument.CreateElement("EnemyCurrentHealth");
            enemyPosXElement = xmlDocument.CreateElement("EnemyPositionX");
            enemyPosYElement = xmlDocument.CreateElement("EnemyPositionY");
            enemyPosZElement = xmlDocument.CreateElement("EnemyPositionZ");

            enemyMaxHealthElement.InnerText = saveData.enemyMaxHealth[i].ToString();
            enemyCurrentHealthElement.InnerText = saveData.enemyCurrentHealth[i].ToString();
            enemyPosXElement.InnerText = saveData.enemyPositionX[i].ToString();
            enemyPosYElement.InnerText = saveData.enemyPositionY[i].ToString();
            enemyPosZElement.InnerText = saveData.enemyPositionZ[i].ToString();

            enemyElement.AppendChild(enemyMaxHealthElement);
            enemyElement.AppendChild(enemyCurrentHealthElement);
            enemyElement.AppendChild(enemyPosXElement);
            enemyElement.AppendChild(enemyPosYElement);
            enemyElement.AppendChild(enemyPosZElement);

            root.AppendChild(enemyElement);
        }
        //NPC Info
        XmlElement npcElement, npcPosXElement, npcPosYElement, npcPosZElement;

        for (int i = 0; i < saveData.npcPositionX.Count; i++)
        {
            npcElement = xmlDocument.CreateElement("NPC");
            npcPosXElement = xmlDocument.CreateElement("NPCPositionX");
            npcPosYElement = xmlDocument.CreateElement("NPCPositionY");
            npcPosZElement = xmlDocument.CreateElement("NPCPositionZ");

            npcPosXElement.InnerText = saveData.npcPositionX[i].ToString();
            npcPosYElement.InnerText = saveData.npcPositionY[i].ToString();
            npcPosZElement.InnerText = saveData.npcPositionZ[i].ToString();

            npcElement.AppendChild(npcPosXElement);
            npcElement.AppendChild(npcPosYElement);
            npcElement.AppendChild(npcPosZElement);

            root.AppendChild(npcElement);
        }


    }


    public void LoadFromXML()
    {

    }
}
