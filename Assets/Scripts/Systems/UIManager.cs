using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ToggleDeathPanel()
    {
        Debug.Log("Toggling deathpanel");
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
}
