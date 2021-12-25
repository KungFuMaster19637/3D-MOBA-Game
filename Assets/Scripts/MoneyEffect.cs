using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    public GameObject moneyEffect;
    public void PlayMoneyEffect(int amount)
    {
        moneyText.text =  "+ " + amount.ToString();
        moneyEffect.SetActive(true);
    }

}
