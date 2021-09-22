using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCharacter : MonoBehaviour
{
    public GameObject textToShow;

    public void OnMouseOver()
    {
        Debug.Log("hovering over");
        textToShow.SetActive(true);
    }

    public void OnMouseExit()
    {
        textToShow.SetActive(false);
    }
}
