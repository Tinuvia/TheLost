using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;

    public static bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ToggleDeathPanel()
    {
        Debug.Log("Toggling deathpanel");
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void TogglePausePanel()
    {
        Debug.Log("Toggling pausepanel");
        pausePanel.SetActive(!pausePanel.activeSelf);
        isPaused = pausePanel.activeSelf;
        Debug.Log("Toggle and isPaused " + isPaused);
    }

    public void PauseGame()
    {
        TogglePausePanel();
        Time.timeScale = 0f; // stops all animations, updates etc
        Debug.Log("Pause called");
        Debug.Log("isPaused " + isPaused);
    }

    public void ResumeGame()
    {
        TogglePausePanel();
        Time.timeScale = 1f;
        Debug.Log("Resume called");
        Debug.Log("isPaused " + isPaused);
    }
}
