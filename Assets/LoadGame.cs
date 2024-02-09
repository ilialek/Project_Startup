using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image LoadingBarFil;

    public GameObject headerButtons;
    public GameObject guy;
    public GameObject lady;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);
        headerButtons.SetActive(false);
        guy.SetActive(false);
        lady.SetActive(false);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFil.fillAmount = progressValue;

            yield return null;
        }
    }
        
}
