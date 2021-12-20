using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AI;

public class SaveManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameObject player;
    private Transform playerPosition;

    private LevelUpStats levelUp;
    private ItemDisplay inventory;
    //private MoneyDisplay money; Is a static
    private Teleporter teleporter1;
    private Teleporter2 teleporter2;

    [SerializeField] private GameObject castleActive;
    [SerializeField] private GameObject wildActive;
    [SerializeField] private GameObject caveActive;

    private QuestGiver questGiver;

    [SerializeField] private GameObject[] items = new GameObject[27];
    [SerializeField] private GameObject[] enemies = new GameObject[18];
    //private EnemyStats[] enemyStats;
    
    //Enemy types
    [SerializeField] private GameObject enemySkeletonPrefab;
    [SerializeField] private GameObject enemyBlueBoarPrefab;
    [SerializeField] private GameObject enemyDragonSoulEaterPrefab;


    //private GameObject[] npcs;
    //private Transform npcPosition;


    private void Start()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Item").Length);
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");

        levelUp = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelUpStats>();
        inventory = GameObject.FindGameObjectWithTag("ItemDisplay").GetComponent<ItemDisplay>();
        //money = GameObject.FindGameObjectWithTag("MoneyDisplay").GetComponent<MoneyDisplay>();

        teleporter1 = GameObject.FindGameObjectWithTag("Teleporter1").GetComponent<Teleporter>();
        teleporter2 = GameObject.FindGameObjectWithTag("Teleporter2").GetComponent<Teleporter2>();

        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();

        items = GameObject.FindGameObjectsWithTag("Item");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Enemies in scene: " + enemies.Length);

        //npcs = GameObject.FindGameObjectsWithTag("NPC");
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

            isInCastle = teleporter1.isInCastle,
            isInCave = teleporter2.isInCave,

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

        //Items Picked Up
        foreach (GameObject item in items)
        {
            saveData.isPickedUp.Add(item.GetComponent<Item>().isPickedUp);
        }

        //Enemy Positions
        foreach(GameObject enemy in enemies)
        {
            saveData.enemyName.Add(enemy.name);
            saveData.enemyMaxHealth.Add(enemy.GetComponent<EnemyStats>().maxHealth);
            saveData.enemyCurrentHealth.Add(enemy.GetComponent<EnemyStats>().health);
            saveData.enemyIsDead.Add(enemy.GetComponent<EnemyStats>().isDead);

            saveData.enemyPositionX.Add(enemy.GetComponent<Transform>().position.x);
            saveData.enemyPositionY.Add(enemy.GetComponent<Transform>().position.y);
            saveData.enemyPositionZ.Add(enemy.GetComponent<Transform>().position.z);
        }

        //NPC Positions
        //foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        //{
        //    saveData.npcPositionX.Add(npc.GetComponent<Transform>().position.x);
        //    saveData.npcPositionY.Add(npc.GetComponent<Transform>().position.y);
        //    saveData.npcPositionZ.Add(npc.GetComponent<Transform>().position.z);
        //}

        return saveData;
    }
    public void SaveToXML()
    {
        SaveData saveData = CreateSaveDataGameObject();
        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("Save"); //<Save>...elements...</Save>

        root.SetAttribute("FileName", "SavedGameplay");//OPTIONAL

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
        XmlElement playerPosXElement = xmlDocument.CreateElement("PlayerPositionX");
        XmlElement playerPosYElement = xmlDocument.CreateElement("PlayerPositionY");
        XmlElement playerPosZElement = xmlDocument.CreateElement("PlayerPositionZ");

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
        XmlElement strengthPotionElement = xmlDocument.CreateElement("StrengthPotion");
        XmlElement defencePotionElement = xmlDocument.CreateElement("DefencePotion");
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

        //Teleporters
        XmlElement teleporter1Element = xmlDocument.CreateElement("Teleporter1");
        XmlElement teleporter2Element = xmlDocument.CreateElement("Teleporter2");

        teleporter1Element.InnerText = saveData.isInCastle.ToString();
        teleporter2Element.InnerText = saveData.isInCave.ToString();

        root.AppendChild(teleporter1Element);
        root.AppendChild(teleporter2Element);

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

        //Items Picked Up
        XmlElement itemPickedUpElement;

        for (int i = 0; i < saveData.isPickedUp.Count; i++)
        {
            itemPickedUpElement = xmlDocument.CreateElement("ItemPickedUp");

            itemPickedUpElement.InnerText = saveData.isPickedUp[i].ToString();

            root.AppendChild(itemPickedUpElement);
        }

        //Enemy Info
        XmlElement enemyElement, enemyNameElement, enemyMaxHealthElement, enemyCurrentHealthElement, enemyIsDeadElement,
            enemyPosXElement, enemyPosYElement, enemyPosZElement;

        for (int i = 0; i < saveData.enemyPositionX.Count; i++)
        {
            enemyElement = xmlDocument.CreateElement("Enemy");

            enemyNameElement = xmlDocument.CreateElement("EnemyName");
            enemyMaxHealthElement = xmlDocument.CreateElement("EnemyMaxHealth");
            enemyCurrentHealthElement = xmlDocument.CreateElement("EnemyCurrentHealth");
            enemyIsDeadElement = xmlDocument.CreateElement("EnemyIsDead");
            enemyPosXElement = xmlDocument.CreateElement("EnemyPositionX");
            enemyPosYElement = xmlDocument.CreateElement("EnemyPositionY");
            enemyPosZElement = xmlDocument.CreateElement("EnemyPositionZ");

            enemyNameElement.InnerText = saveData.enemyName[i];
            enemyMaxHealthElement.InnerText = saveData.enemyMaxHealth[i].ToString();
            enemyCurrentHealthElement.InnerText = saveData.enemyCurrentHealth[i].ToString();
            enemyIsDeadElement.InnerText = saveData.enemyIsDead[i].ToString();
            enemyPosXElement.InnerText = saveData.enemyPositionX[i].ToString();
            enemyPosYElement.InnerText = saveData.enemyPositionY[i].ToString();
            enemyPosZElement.InnerText = saveData.enemyPositionZ[i].ToString();

            enemyElement.AppendChild(enemyNameElement);
            enemyElement.AppendChild(enemyMaxHealthElement);
            enemyElement.AppendChild(enemyCurrentHealthElement);
            enemyElement.AppendChild(enemyIsDeadElement);
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

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/SavedDataXML.text");
        if (File.Exists(Application.dataPath + "/SavedDataXML.text"))
        {
            Debug.Log("file saved");
        }
    }


    public void LoadFromXML()
    {
        if (File.Exists(Application.dataPath + "/SavedDataXML.text"))
        {
            //Load the variables

            SaveData saveData = new SaveData();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/SavedDataXML.text");

            //Get the saved data from the file

            //Player Info

            //Player Health
            XmlNodeList loadMaxHealth = xmlDocument.GetElementsByTagName("MaxHealth");
            XmlNodeList loadCurrentHealth = xmlDocument.GetElementsByTagName("CurrentHealth");
            XmlNodeList loadHealthRegeneration = xmlDocument.GetElementsByTagName("HealthRegeneration");

            saveData.maxHealth = float.Parse(loadMaxHealth[0].InnerText);
            saveData.currentHealth = float.Parse(loadCurrentHealth[0].InnerText);
            saveData.healthRegeneration = float.Parse(loadHealthRegeneration[0].InnerText);

            //Player Mana
            XmlNodeList loadMaxMana = xmlDocument.GetElementsByTagName("MaxMana");
            XmlNodeList loadCurrentMana = xmlDocument.GetElementsByTagName("CurrentMana");
            XmlNodeList loadManaRegeneration = xmlDocument.GetElementsByTagName("ManaRegeneration");

            saveData.maxMana = float.Parse(loadMaxMana[0].InnerText);
            saveData.currentMana = float.Parse(loadCurrentMana[0].InnerText);
            saveData.manaRegeneration = float.Parse(loadManaRegeneration[0].InnerText);

            //Player Attack
            XmlNodeList loadAttackDamage = xmlDocument.GetElementsByTagName("AttackDamage");
            XmlNodeList loadAttackSpeed = xmlDocument.GetElementsByTagName("AttackSpeed");
            XmlNodeList loadAttackTime = xmlDocument.GetElementsByTagName("AttackTime");
            XmlNodeList loadAttackRange = xmlDocument.GetElementsByTagName("AttackRange");

            saveData.attackDamage = float.Parse(loadAttackDamage[0].InnerText);
            saveData.attackSpeed = float.Parse(loadAttackSpeed[0].InnerText);
            saveData.attackTime = float.Parse(loadAttackTime[0].InnerText);
            saveData.attackRange = float.Parse(loadAttackRange[0].InnerText);

            //Player Position
            XmlNodeList loadPlayerPosX = xmlDocument.GetElementsByTagName("PlayerPositionX");
            XmlNodeList loadPlayerPosY = xmlDocument.GetElementsByTagName("PlayerPositionY");
            XmlNodeList loadPlayerPosZ = xmlDocument.GetElementsByTagName("PlayerPositionZ");

            saveData.playerPositionX = float.Parse(loadPlayerPosX[0].InnerText);
            saveData.playerPositionY = float.Parse(loadPlayerPosY[0].InnerText);
            saveData.playerPositionZ = float.Parse(loadPlayerPosZ[0].InnerText);

            //Player Level
            XmlNodeList loadPlayerExp = xmlDocument.GetElementsByTagName("PlayerExp");
            XmlNodeList loadPlayerLevel = xmlDocument.GetElementsByTagName("PlayerLevel");

            saveData.playerExp = float.Parse(loadPlayerExp[0].InnerText);
            saveData.playerLevel = int.Parse(loadPlayerLevel[0].InnerText);

            //Money
            XmlNodeList loadMoney = xmlDocument.GetElementsByTagName("Money");

            saveData.moneyAmount = int.Parse(loadMoney[0].InnerText);

            //Inventory items
            XmlNodeList loadHealthPotion = xmlDocument.GetElementsByTagName("HealthPotion");
            XmlNodeList loadManaPotion = xmlDocument.GetElementsByTagName("ManaPotion");
            XmlNodeList loadStrengthPotion = xmlDocument.GetElementsByTagName("StrengthPotion");
            XmlNodeList loadDefencePotion = xmlDocument.GetElementsByTagName("DefencePotion");
            XmlNodeList loadSpellPotion = xmlDocument.GetElementsByTagName("SpellPotion");
            XmlNodeList loadWheat = xmlDocument.GetElementsByTagName("Wheat");
            XmlNodeList loadIron = xmlDocument.GetElementsByTagName("Iron");
            XmlNodeList loadFowl = xmlDocument.GetElementsByTagName("Fowl");
            XmlNodeList loadMagicalDonut = xmlDocument.GetElementsByTagName("MagicalDonut");
            XmlNodeList loadRiskyPotion = xmlDocument.GetElementsByTagName("RiskyPotion");
            XmlNodeList loadRingOfMana = xmlDocument.GetElementsByTagName("RingOfMana");
            XmlNodeList loadHolyOrb = xmlDocument.GetElementsByTagName("HolyOrb");

            saveData.inventoryAmount[0] = int.Parse(loadHealthPotion[0].InnerText);
            saveData.inventoryAmount[1] = int.Parse(loadManaPotion[0].InnerText);
            saveData.inventoryAmount[2] = int.Parse(loadStrengthPotion[0].InnerText);
            saveData.inventoryAmount[3] = int.Parse(loadDefencePotion[0].InnerText);
            saveData.inventoryAmount[4] = int.Parse(loadSpellPotion[0].InnerText);
            saveData.inventoryAmount[5] = int.Parse(loadWheat[0].InnerText);
            saveData.inventoryAmount[6] = int.Parse(loadIron[0].InnerText);
            saveData.inventoryAmount[7] = int.Parse(loadFowl[0].InnerText);
            saveData.inventoryAmount[8] = int.Parse(loadMagicalDonut[0].InnerText);
            saveData.inventoryAmount[9] = int.Parse(loadRiskyPotion[0].InnerText);
            saveData.inventoryAmount[10] = int.Parse(loadRingOfMana[0].InnerText);
            saveData.inventoryAmount[11] = int.Parse(loadHolyOrb[0].InnerText);

            //Teleporters
            XmlNodeList loadTeleport1 = xmlDocument.GetElementsByTagName("Teleporter1");
            XmlNodeList loadTeleport2 = xmlDocument.GetElementsByTagName("Teleporter2");

            saveData.isInCastle = bool.Parse(loadTeleport1[0].InnerText);
            saveData.isInCave = bool.Parse(loadTeleport2[0].InnerText);

            //Quest 
            XmlNodeList loadWheatQuestActive = xmlDocument.GetElementsByTagName("WheatQuestActive");
            XmlNodeList loadWheatQuestCompleted = xmlDocument.GetElementsByTagName("WheatQuestCompleted");
            XmlNodeList loadIronQuestActive = xmlDocument.GetElementsByTagName("IronQuestActive");
            XmlNodeList loadIronQuestCompleted = xmlDocument.GetElementsByTagName("IronQuestCompleted");

            saveData.wheatQuestActive = bool.Parse(loadWheatQuestActive[0].InnerText);
            saveData.wheatQuestCompleted = bool.Parse(loadWheatQuestCompleted[0].InnerText);
            saveData.ironQuestActive = bool.Parse(loadIronQuestActive[0].InnerText);
            saveData.ironQuestCompleted = bool.Parse(loadIronQuestCompleted[0].InnerText);

            //Items Picked up
            XmlNodeList loadItems = xmlDocument.GetElementsByTagName("ItemPickedUp");

            if (loadItems.Count != 0)
            {
                for (int i = 0; i < loadItems.Count; i++)
                {
                    saveData.isPickedUp.Add(bool.Parse(loadItems[i].InnerText));
                }
            }

            //Enemy Info
            XmlNodeList loadEnemy = xmlDocument.GetElementsByTagName("Enemy");
            if (loadEnemy.Count != 0)//if there are enemies saved
            {
                for (int i = 0; i < loadEnemy.Count; i++)
                {
                    XmlNodeList loadEnemyMaxHealth = xmlDocument.GetElementsByTagName("EnemyMaxHealth");
                    XmlNodeList loadEnemyCurrentHealth = xmlDocument.GetElementsByTagName("EnemyCurrentHealth");
                    XmlNodeList loadEnemyIsDead = xmlDocument.GetElementsByTagName("EnemyIsDead");
                    XmlNodeList loadEnemyPosX = xmlDocument.GetElementsByTagName("EnemyPositionX");
                    XmlNodeList loadEnemyPosY = xmlDocument.GetElementsByTagName("EnemyPositionY");
                    XmlNodeList loadEnemyPosZ = xmlDocument.GetElementsByTagName("EnemyPositionZ");

                    saveData.enemyMaxHealth.Add(float.Parse(loadEnemyMaxHealth[i].InnerText));
                    saveData.enemyCurrentHealth.Add(float.Parse(loadEnemyCurrentHealth[i].InnerText));
                    saveData.enemyIsDead.Add(bool.Parse(loadEnemyIsDead[i].InnerText));
                    saveData.enemyPositionX.Add(float.Parse(loadEnemyPosX[i].InnerText));
                    saveData.enemyPositionY.Add(float.Parse(loadEnemyPosY[i].InnerText));
                    saveData.enemyPositionZ.Add(float.Parse(loadEnemyPosZ[i].InnerText));
                }
            }

            //NPC Info
            //XmlNodeList loadNpc = xmlDocument.GetElementsByTagName("NPC");
            //for (int i = 0; i < loadNpc.Count; i++)
            //{
            //    XmlNodeList loadNPCPosX = xmlDocument.GetElementsByTagName("NPCPositionX");
            //    XmlNodeList loadNPCPosY = xmlDocument.GetElementsByTagName("NPCPositionY");
            //    XmlNodeList loadNPCPosZ = xmlDocument.GetElementsByTagName("NPCPositionZ");

            //    saveData.npcPositionX.Add(float.Parse(loadNPCPosX[i].InnerText));
            //    saveData.npcPositionY.Add(float.Parse(loadNPCPosY[i].InnerText));
            //    saveData.npcPositionZ.Add(float.Parse(loadNPCPosZ[i].InnerText));
            //}

            ///////////////////////////////////////////////////////////////////////////////////
            
            //Assign saved data to the right gameobjects

            //Player Info

            //Player Health
            playerStats.maxHealth = saveData.maxHealth;
            playerStats.health = saveData.currentHealth;
            playerStats.healthRegeneration = saveData.healthRegeneration;

            //Player Mana
            playerStats.maxMana = saveData.maxMana;
            playerStats.mana = saveData.currentMana;
            playerStats.manaRegeneration = saveData.manaRegeneration;

            //Player Attack
            playerStats.attackDamage = saveData.attackDamage;
            playerStats.attackSpeed = saveData.attackSpeed;
            playerStats.attackTime = saveData.attackTime;
            playerStats.attackRange = saveData.attackRange;

            //Player Position
            player.GetComponent<NavMeshAgent>().enabled = false;
            playerPosition.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ);
            player.GetComponent<NavMeshAgent>().enabled = true;



            //Player Level
            levelUp.expBarImage.fillAmount = saveData.playerExp;
            levelUp.level = saveData.playerLevel;

            //Money
            MoneyDisplay.moneyAmount = saveData.moneyAmount;

            //Inventory Items
            inventory.itemAmount[0] = saveData.inventoryAmount[0];
            inventory.itemAmount[1] = saveData.inventoryAmount[1];
            inventory.itemAmount[2] = saveData.inventoryAmount[2];
            inventory.itemAmount[3] = saveData.inventoryAmount[3];
            inventory.itemAmount[4] = saveData.inventoryAmount[4];
            inventory.itemAmount[5] = saveData.inventoryAmount[5];
            inventory.itemAmount[6] = saveData.inventoryAmount[6];
            inventory.itemAmount[7] = saveData.inventoryAmount[7];
            inventory.itemAmount[8] = saveData.inventoryAmount[8];
            inventory.itemAmount[9] = saveData.inventoryAmount[9];
            inventory.itemAmount[10] = saveData.inventoryAmount[10];
            inventory.itemAmount[11] = saveData.inventoryAmount[11];
            inventory.RefreshInventoryItems();

            //Teleporters
            teleporter1.isInCastle = saveData.isInCastle;
            teleporter2.isInCave = saveData.isInCave;

            //Environment
            EnvironmentActive();

            //Quest
            questGiver.wheatQuest.isActive = saveData.wheatQuestActive;
            questGiver.wheatQuest.completed = saveData.wheatQuestCompleted;
            questGiver.ironQuest.isActive = saveData.ironQuestActive;
            questGiver.ironQuest.completed = saveData.ironQuestCompleted;

            //Items Picked Up
            for (int i = 0; i < saveData.isPickedUp.Count; i++)
            {
                if (items[i].GetComponent<Item>().isPickedUp)
                {
                    if (!saveData.isPickedUp[i])
                    {
                        RespawnItem(items[i]);
                    }
                }
            }

            //Enemy Info
            for (int i = 0; i < saveData.enemyMaxHealth.Count; i++)
            {
                if (enemies[i].GetComponent<EnemyStats>().isDead)
                {
                    if (!saveData.enemyIsDead[i])
                    {
                        Debug.Log("enemy died??");
                        float enemyPosX = saveData.enemyPositionX[i];
                        float enemyPosY = saveData.enemyPositionY[i];
                        float enemyPosZ = saveData.enemyPositionZ[i];
                        RespawnEnemy(enemies[i]);
                        enemies[i].transform.position = new Vector3(enemyPosX, enemyPosY, enemyPosZ);
                    }
                }
                else
                {
                    Debug.Log("Repositining");
                    float enemyPosX = saveData.enemyPositionX[i];
                    float enemyPosY = saveData.enemyPositionY[i];
                    float enemyPosZ = saveData.enemyPositionZ[i];
                    enemies[i].transform.position = new Vector3(enemyPosX, enemyPosY, enemyPosZ);
                }

                if (saveData.enemyIsDead[i])
                {
                    Debug.Log("Enemy: " + saveData.enemyName[i] + " is dead");
                }

            }

            //NPC Info

        }
        else
        {
            Debug.Log("No File is saved");
        }
    }

    private void EnvironmentActive()
    {
        //SetActive the right environment

        if (teleporter1.isInCastle)
        {
            castleActive.SetActive(true);
            wildActive.SetActive(false);
            caveActive.SetActive(false);
        }

        if (teleporter2.isInCave)
        {
            castleActive.SetActive(false);
            wildActive.SetActive(false);
            caveActive.SetActive(true);
        }

        if (!teleporter1.isInCastle && !teleporter2.isInCave)
        {
            castleActive.SetActive(false);
            wildActive.SetActive(true);
            caveActive.SetActive(false);
        }
    }

    private void RespawnItem(GameObject item)
    {
        item.GetComponent<Item>().isPickedUp = false;
        item.GetComponent<Collider>().enabled = true;
        item.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
    }
    private void RespawnEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().enabled = true;
        enemy.GetComponent<EnemyStats>().dieOnce = false;
        enemy.GetComponent<EnemyStats>().giveExpOnce = false;
        enemy.GetComponent<EnemyStats>().healthBar.SetActive(true);
        enemy.GetComponent<EnemyStats>().anim.SetBool("IsDying", false);
        enemy.GetComponent<Collider>().enabled = true;
        enemy.GetComponent<NavMeshAgent>().enabled = true;

        enemy.gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
    }
}
