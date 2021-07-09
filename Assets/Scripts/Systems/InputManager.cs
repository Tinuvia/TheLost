using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] ScenesData scenesData;   


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scenesData.LoadPauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scenesData.ExitGame();
        }
    }
}
