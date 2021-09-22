using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public void LoadLevel(string sceneString)
    {
        StartCoroutine(LoadAsynchronously(sceneString));
    }

    IEnumerator LoadAsynchronously(string sceneString)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneString);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }

}
