using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("GameOver called");
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            Debug.Log("UI manager found");
            _ui.ToggleDeathPanel();
        }
    }
}
