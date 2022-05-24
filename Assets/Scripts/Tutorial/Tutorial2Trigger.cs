using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2Trigger : MonoBehaviour
{
    private bool ability1Clicked;
    private bool ability2Clicked;
    private bool ability3Clicked;
    private bool ability4Clicked;

    private ArthurAbilities arthurAbilities;
    private MerlinAbilities merlinAbilities;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<ArthurAbilities>())
        {
            arthurAbilities = player.GetComponent<ArthurAbilities>();
        }
        if (player.GetComponent<MerlinAbilities>())
        {
            merlinAbilities = player.GetComponent<MerlinAbilities>();
        }

        ability1Clicked = false;
        ability2Clicked = false;
        ability3Clicked = false;
        ability4Clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.Instance.tutorialCounter != 2) return;

        MerlinTutorial();
        ArthurTutorial();
    }
    #region Arthur Tutorial
    public void ArthurTutorial()
    {
        if (arthurAbilities != null)
        {
            if (Input.GetKeyDown(arthurAbilities.ability1))
            {
                ability1Clicked = true;
                CheckIfAllClicked();
            }

            if (arthurAbilities.targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
            {
                ability2Clicked = true;
                CheckIfAllClicked();
            }

            if (Input.GetKeyDown(arthurAbilities.ability3))
            {
                ability3Clicked = true;
                CheckIfAllClicked();
            }

            if (Input.GetKeyDown(arthurAbilities.ability4))
            {
                ability4Clicked = true;
                CheckIfAllClicked();
            }
        }
    }
    #endregion

    #region Merlin Tutorial
    public void MerlinTutorial()
    {
        if (merlinAbilities != null)
        {
            if (Input.GetKeyDown(merlinAbilities.ability1))
            {
                ability1Clicked = true;
                CheckIfAllClicked();
            }

            if (merlinAbilities.skillshot2.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
            {
                ability2Clicked = true;
                CheckIfAllClicked();
            }

            if (merlinAbilities.skillshot3.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
            {
                ability3Clicked = true;
                CheckIfAllClicked();
            }

            if (merlinAbilities.skillshot4.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
            {
                ability4Clicked = true;
                CheckIfAllClicked();
            }

        }
    }
    #endregion

    private void CheckIfAllClicked()
    {
        if (ability1Clicked && ability2Clicked && ability3Clicked && ability4Clicked)
        {
            TutorialManager.Instance.Tutorial2();
        }
        else
        {
            Debug.Log("not full true yet");
        }

    }

    [ContextMenu("Skip Tutorial")]
    public void SkipTutorial2()
    {
        TutorialManager.Instance.Tutorial2();
    }

}
