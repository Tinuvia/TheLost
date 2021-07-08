using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene("Gameplay");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive); // Loads double eventsystem etc, since Gameplay loads the parts.
    }

    public void ChangeSceneByName(string name)
    {
        if(name != null)
        {
            SceneManager.LoadScene(name);
        }
    }
}
