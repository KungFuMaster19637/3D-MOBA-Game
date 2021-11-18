using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShopManager : MonoBehaviour
{

    public ItemDisplay itemDisplay;
    [SerializeField] private GameObject blockInventory;

    private int healthPotionPrice;
    private int manaPotionPrice;
    private int fowlPrice;
    private int magicalDonutPrice;

    private NavMeshAgent agent;
    void Start()
    {
        healthPotionPrice = 10;
        manaPotionPrice = 20;
        fowlPrice = 50;
        magicalDonutPrice = 75;

        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
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

    public void BuyHealthPotion()
    {
        if (MoneyDisplay.moneyAmount >= healthPotionPrice)
        {
            itemDisplay.AddItem(0);
            MoneyDisplay.UseMoney(healthPotionPrice);
        }
        else
        {
            //Display can't buy
        }

    }
    public void BuyManaPotion()
    {
        if (MoneyDisplay.moneyAmount >= manaPotionPrice)
        {
            itemDisplay.AddItem(1);
            MoneyDisplay.UseMoney(manaPotionPrice);
        }
    }

    public void BuyFowl()
    {
        if (MoneyDisplay.moneyAmount >= fowlPrice)
        {
            itemDisplay.AddItem(7);
            MoneyDisplay.UseMoney(fowlPrice);
        }
    }

    public void BuyMagicalDonut()
    {
        if (MoneyDisplay.moneyAmount >= magicalDonutPrice)
        {
            itemDisplay.AddItem(8);
            MoneyDisplay.UseMoney(magicalDonutPrice);
        }
    }
    //public void BuyHealthPotion()
    //{

    //}
    //public void BuyHealthPotion()
    //{

    //}

}
