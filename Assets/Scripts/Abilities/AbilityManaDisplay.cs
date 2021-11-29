using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManaDisplay : MonoBehaviour
{
    private ArthurAbilities arthurAbilitiesScript;

    [SerializeField] private TMP_Text ability1;
    [SerializeField] private TMP_Text ability2;
    [SerializeField] private TMP_Text ability3;
    [SerializeField] private TMP_Text ability4;


    // Start is called before the first frame update
    void Start()
    {
        arthurAbilitiesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ArthurAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        ability1.text = arthurAbilitiesScript.abilityMana1.ToString(); 
        ability2.text = arthurAbilitiesScript.abilityMana2.ToString(); 
        ability3.text = arthurAbilitiesScript.abilityMana3.ToString(); 
        ability4.text = arthurAbilitiesScript.abilityMana4.ToString();
    }
}
