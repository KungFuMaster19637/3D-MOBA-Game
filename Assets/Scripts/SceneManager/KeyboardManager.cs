using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Dropdown controlsDropdown;
    // Start is called before the first frame update
    void Start()
    {
        InitDropdown();
    }

    private void InitDropdown()
    {
        Debug.Log("came here");
        string dropdown = PlayerPrefs.GetString("Ability 1");
        //0 = Qwerty, 1 = Azerty
        switch (dropdown)
        {
            case ("Q"):
                controlsDropdown.value = 0;
                break;
            case ("A"):
                controlsDropdown.value = 1;
                break;
            default:
                controlsDropdown.value = 1;
                break;
        }
    }

    public void ChangeKeyboardLayout(int layout)
    {
        switch (layout)
        {
            //Azerty
            case 0:
                PlayerPrefs.SetString("Ability 1", "Q");
                PlayerPrefs.SetString("Ability 2", "W");
                break;
            //Qwerty
            case 1:
                PlayerPrefs.SetString("Ability 1", "A");
                PlayerPrefs.SetString("Ability 2", "Z");
                break;
            default:
                PlayerPrefs.SetString("Ability 1", "A");
                PlayerPrefs.SetString("Ability 2", "Z");
                break;
        }
    }
}
