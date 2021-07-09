using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "sceneDB", menuName = "Scene Data/Database")]
public class ScenesData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public List<Menu> menus = new List<Menu>();
    public int CurrentLevelIndex=1;

    /*
     * Levels
     */

    //Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            //Load Gameplay scene for the level
            SceneManager.LoadSceneAsync("Gameplay" + index.ToString());
            //Load first part of the level in additive mode
            SceneManager.LoadSceneAsync("Level" + index.ToString() + "Part1", LoadSceneMode.Additive);
        }
        //reset the index if we have no more levels
        else CurrentLevelIndex =1;
    }
    //Start next level
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //Restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //New game, load level 1
    public void NewGame()
    {
        LoadLevelWithIndex(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /*
     * Menus
     */

    //Load main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].sceneName);
    }
    //Load Pause Menu
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].sceneName); // , LoadSceneMode.Additive
        Time.timeScale = 0f; // stops all animations, updates etc
        Debug.Log("Pause called");
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(menus[(int)Type.Pause_Menu].sceneName);
        Time.timeScale = 1f;
        Debug.Log("Resume called");
    }

    //Load Death Menu
    public void LoadDeathMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Death_Menu].sceneName, LoadSceneMode.Additive);
    }


}
