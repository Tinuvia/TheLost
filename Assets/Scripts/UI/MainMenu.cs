using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// not yet implemented, no need for loading bar. Should be synchronized with ScenesData too using NewGame() ?
public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay1"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level1Part1", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
                Debug.Log("Loading ...");
            }
        }
    }
}
