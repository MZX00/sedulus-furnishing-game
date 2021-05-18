using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{   
    [SerializeField]
    private GameObject settingsPanel;
    
    [SerializeField]
    private GameObject mainMenuPanel;

    private string selected;

    public void printGroup()
    {
        selected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(selected);
    }

    public void openOptionsPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void openMainMenuPanel()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
