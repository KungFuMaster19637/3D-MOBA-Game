using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverUI : MonoBehaviour
{

    public TMP_Text abilityDescription;
    public GameObject descriptionUI;

    private string ability1;
    private string ability2;
    private string ability3;
    private string ability4;

    private void Start()
    {
        descriptionUI.SetActive(false);
        ability1 = "";
        ability2 = "";
        ability3 = "";
        ability4 = "";
    }

    void Update()
    {
        //OnMouseOver();
    }
    public void OnMouseOver()
    {
        descriptionUI.SetActive(true);
        if (gameObject.name == "Ability1")
        {
            abilityDescription.text = ability1;
        }
        if (gameObject.name == "Ability2")
        {
            abilityDescription.text = ability2;
        }
        if (gameObject.name == "Ability3")
        {
            abilityDescription.text = ability3;
        }
        if (gameObject.name == "Ability4")
        {
            abilityDescription.text = ability4;
        }
    }

    public void OnMouseExit()
    {
        abilityDescription.text = "";
        descriptionUI.SetActive(false);
    }
}
