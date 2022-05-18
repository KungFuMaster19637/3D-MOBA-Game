using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShopManager : MonoBehaviour
{

    public ItemDisplay itemDisplay;
    [SerializeField] private GameObject blockInventory;
    [SerializeField] private GameObject warningSpawner;
    [SerializeField] private Transform warningTrans;

    private int healthPotionPrice;
    private int manaPotionPrice;
    private int fowlPrice;
    private int magicalDonutPrice;

    private PlayerSounds playerSounds;

    private float warningTime;
    private NavMeshAgent agent;
    private Coroutine errorLock;

    void Start()
    {
        healthPotionPrice = 10;
        manaPotionPrice = 20;
        fowlPrice = 50;
        magicalDonutPrice = 75;

        warningTime = 2f;

        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>();
    }

    void Update()
    {
        
    }

    public void Shopping()
    {
        agent.enabled = false;
        blockInventory.SetActive(true);
    }

    public void NotShopping()
    {
        agent.enabled = true;
        blockInventory.SetActive(false);
    }

    public void SoundOnBought()
    {
        playerSounds.ShopSoundPlayed();
    }

    public void SpawnError()
    {
        if (errorLock != null) { return; }
        errorLock = StartCoroutine(SpawnErrorMessage());
    }

    private IEnumerator SpawnErrorMessage()
    {
        GameObject warning = Instantiate(warningSpawner, warningTrans);
        Destroy(warning, warningTime);
        yield return new WaitForSeconds(warningTime);
        errorLock = null;
    }

    public void BuyHealthPotion()
    {
        if (MoneyDisplay.moneyAmount >= healthPotionPrice)
        {
            itemDisplay.AddItem(0);
            MoneyDisplay.UseMoney(healthPotionPrice);
            SoundOnBought();
        }
        else
        {
            SpawnError();
        }
    }
    public void BuyManaPotion()
    {
        if (MoneyDisplay.moneyAmount >= manaPotionPrice)
        {
            itemDisplay.AddItem(1);
            MoneyDisplay.UseMoney(manaPotionPrice);
            SoundOnBought();
        }
        else
        {
            SpawnError();
        }
    }

    public void BuyFowl()
    {
        if (MoneyDisplay.moneyAmount >= fowlPrice)
        {
            itemDisplay.AddItem(7);
            MoneyDisplay.UseMoney(fowlPrice);
            SoundOnBought();
        }
        else
        {
            SpawnError();
        }
    }

    public void BuyMagicalDonut()
    {
        if (MoneyDisplay.moneyAmount >= magicalDonutPrice)
        {
            itemDisplay.AddItem(8);
            MoneyDisplay.UseMoney(magicalDonutPrice);
            SoundOnBought();
        }
        else
        {
            SpawnError();
        }
    }

}
