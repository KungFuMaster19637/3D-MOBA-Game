using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToCity : MonoBehaviour
{
    private string arthurString = "Arthur";
    private string cityString = "City";
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
           StartCoroutine(ChangeSceneToCity());
        }
    }

    IEnumerator ChangeSceneToCity()
    {
        SceneManager.LoadScene(arthurString, LoadSceneMode.Additive);

        Scene arthurScene = SceneManager.GetSceneByName(arthurString);
        Scene cityScene = SceneManager.GetSceneByName(cityString);

        SceneManager.MoveGameObjectToScene(player, arthurScene);

        yield return null;

        SceneManager.UnloadSceneAsync(cityScene);
    }
}
