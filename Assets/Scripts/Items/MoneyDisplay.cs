using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public static MoneyDisplay instance;

    public int moneyAmount;
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
    }
    protected void RefreshMoney()
    {
        moneyAmountText.text = moneyAmount.ToString();
    }

    public void AddMoney(int amount)
    {
        moneyAmount += amount;
        RefreshMoney();
    }
    public void UseMoney(int amount)
    {
        moneyAmount -= amount;
        RefreshMoney();
    }
}
