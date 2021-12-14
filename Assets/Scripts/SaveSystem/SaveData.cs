using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData
{
    /*
    All data that needs to be saved:
    PlayerStats
    Inventory
    Money
    Quests
    EnemyStats
    NPC Positions
 
    */
    [Header ("PlayerStats")] //PlayerStats
    public float maxHealth;
    public float currentHealth;
    public float healthRegeneration;

    public float maxMana;
    public float currentMana;
    public float manaRegeneration;

    public float attackDamage;
    public float attackSpeed;
    public float attackTime;
    public float attackRange;

    public float spellPower;
    public float defence;

    [Header("PlayerPosition")]
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    [Header("PlayerLevel")] //LevelUpStats
    //public Image playerExpBar; //Image.fillamount
    public float playerExp;
    public int playerLevel;

    [Header("Money")] //MoneyDisplay
    public int moneyAmount;

    [Header("Inventory")] //ItemDisplay
    public int[] inventoryAmount = new int[12];

    [Header("QuestProgression")] //QuestGiver
    public bool wheatQuestActive;
    public bool wheatQuestCompleted;

    public bool ironQuestActive;
    public bool ironQuestCompleted;

    [Header("EnemyStats")] //EnemyStats
    public List<float> enemyMaxHealth;
    public List<float> enemyCurrentHealth;

    [Header("EnemyPositions")]
    public List<float> enemyPositionX;
    public List<float> enemyPositionY;
    public List<float> enemyPositionZ;

    [Header("NPCPositions")]
    public List<float> npcPositionX;
    public List<float> npcPositionY;
    public List<float> npcPositionZ;

}
