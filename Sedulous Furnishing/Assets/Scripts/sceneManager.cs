using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    public void mainMenuToStartGame()
    {
        SceneManager.LoadScene("Shop");
    }

    // when player clicks inventory Butoon
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
        SceneManager.LoadScene("Shop");
        player.GetComponent<Player>().setMoney(500);
        Debug.Log("Amount of Money = " + player.GetComponent<Player>().getMoney());
    }

    // Go to main menu from Gameplay
    public void gameplayToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void goToWorkshop()
    {
        SceneManager.LoadScene("Workshop");
    }
}
