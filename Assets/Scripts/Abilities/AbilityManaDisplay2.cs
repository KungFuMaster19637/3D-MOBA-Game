using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManaDisplay2 : MonoBehaviour
{
    private MerlinAbilities merlinAbilitiesScript;

    [SerializeField] private TMP_Text ability1;
    [SerializeField] private TMP_Text ability2;
    [SerializeField] private TMP_Text ability3;
    [SerializeField] private TMP_Text ability4;


    // Start is called before the first frame update
    void Start()
    {
        merlinAbilitiesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MerlinAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        ability1.text = merlinAbilitiesScript.abilityMana1.ToString();
        ability2.text = merlinAbilitiesScript.abilityMana2.ToString();
        ability3.text = merlinAbilitiesScript.abilityMana3.ToString();
        ability4.text = merlinAbilitiesScript.abilityMana4.ToString();
    }
}