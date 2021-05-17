using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // when player clickes help button from main menu
    public void mainMenuToHelp()
    {
        SceneManager.LoadScene("HelpSceneMainMenu");
    }

    public void goToHelpSceneInventory()
    {
        SceneManager.LoadScene("HelpSceneInventory");
    }
    // When player clicks return button from help page
    public void helpToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // When Start Button is clicked
    // Game is resumed where it was left off

    // when player clicks inventory Button
    public void firstSceneToInventory()
    {
        SceneManager.LoadScene("inventoryScene");
    }

    // When return button is clicked in inventory
    public void inventoryToFirstScene()
    {
        SceneManager.LoadScene("Shop");
    }

    // When Player wants to start new game
    public void startNewGame()
    {
        string path = Application.persistentDataPath + "/PlayerData";
        try
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            path = Application.persistentDataPath + "/timeData";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            path = Application.persistentDataPath + "/ghData";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        SceneManager.LoadScene("Shop");
    }

    // Go to main menu from Gameplay
    public void gameplayToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
    
}
