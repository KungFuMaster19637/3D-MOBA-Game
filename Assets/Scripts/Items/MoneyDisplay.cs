using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay instance;

    public static int moneyAmount;
    [SerializeField] private TMP_Text moneyAmountText;    


    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        moneyAmount = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            AddMoney(5);
        }
        moneyAmountText.text = moneyAmount.ToString();

    }

    //protected static void RefreshMoney()
    //{
    //    moneyAmountText.text = moneyAmount.ToString();
    //}

    public static void AddMoney(int amount)
    {
        moneyAmount += amount;
        //RefreshMoney();
    }
    public static void UseMoney(int amount)
    {
        moneyAmount -= amount;
        //RefreshMoney();
    }
}
