using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    [SerializeField] private TMP_Text textFade;
    [SerializeField] private GameObject fadeObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeText(string fadeString)
    {
        fadeObject.GetComponent<TextMeshPro>().text = fadeString;
        Instantiate(fadeObject);
    }
}
