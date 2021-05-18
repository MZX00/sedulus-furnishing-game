using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{

    public GameObject player;
    [SerializeField] GameObject furniturePrefab;
    [SerializeField] GameObject selectHandler;

    private GameObject furniture;

    void Start(){
        StartCoroutine("LateStart");
        
        
    }

    IEnumerator LateStart()
    {   
        while(SceneManager.sceneCount != 1){
            Debug.Log("Scene COuntin " + SceneManager.sceneCount);
            yield return null;
        }

        if(SceneManager.GetActiveScene().name == "Workshop" && SceneManager.sceneCount == 1){
            Debug.Log("AFTER OFA");
            furniture = GameObject.Find("Furniture");
            while(furniture != null && furniture.transform.childCount != 0){
                Transform temp = furniture.transform.GetChild(0);
                temp.gameObject.SetActive(true);
                temp.SetParent(transform.root);
                temp.GetComponent<Button>().onClick.RemoveAllListeners();
                foreach (Transform child in temp) {
                    GameObject.Destroy(child.gameObject);
                }
                temp.GetComponent<Button>().onClick.AddListener(delegate { selectHandler.GetComponent<SelectHandler>().selectObject(temp.gameObject); });
            }
        }
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
        // Debug.Log("Amount of Money = " + player.GetComponent<Player>().getMoney());
    }

    // Go to main menu from Gameplay
    public void gameplayToMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void shopToWorkshop(){
        furniture = GameObject.Find("Furniture");
        // Debug.Log("PART NAME: " + temp.name);
        if(furniture != null){
            StartCoroutine(LoadAsyncShop(furniture,"Workshop"));
            // loadScene(furniture,"Shop");
        }else{
            SceneManager.LoadScene("Workshop");
        }
    }

    public void workshopToShop(){
        // Debug.Log("Item Count " + transform.root.childCount);
        furniture = Instantiate(furniturePrefab,new Vector3(0,0,0),Quaternion.identity);
        furniture.name = "Furniture";
        while(transform.root.childCount > 11){
            // Debug.Log("PART NAME: " + transform.root.GetChild(11).name);
            Transform temp = transform.root.GetChild(11);
            temp.gameObject.SetActive(false);
            temp.SetParent(furniture.transform);
        }
        StartCoroutine(LoadAsyncShop(furniture,"Shop"));
        // loadScene(furniture,"Shop");
        
    }

    public void goToShop(){
        SceneManager.LoadScene("Shop");
    }

    // public void loadScene(GameObject item, string sceneName){
    //     DontDestroyOnLoad(item);

    //     Scene currentScene = SceneManager.GetActiveScene();

    //     SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        
    //     SceneManager.MoveGameObjectToScene(item, SceneManager.GetSceneByName(sceneName));
    //     SceneManager.UnloadSceneAsync(currentScene);
    // }

    //Transfering Furniture To shop
    IEnumerator LoadAsyncShop( GameObject item, string sceneName)
    {

        DontDestroyOnLoad(item);

        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(item, SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync(currentScene);

    }
    
}
