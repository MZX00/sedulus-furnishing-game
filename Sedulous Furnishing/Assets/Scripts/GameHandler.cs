using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject timer;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject levelIcon;
    [SerializeField]
    private GameObject cslIcon;
    public GameObject canvas;
    public GameObject cusomterPrefab;
    public GameObject showcase;
    public GameObject player;
    public GameObject variables;
    public GameObject SpeechBubble;


    [SerializeField]
    private Sprite level1;
    [SerializeField]
    private Sprite level2;
    [SerializeField]
    private Sprite level3;
    [SerializeField]
    private Sprite level4;
    [SerializeField]
    private Sprite level5;
    [SerializeField]
    private Sprite level6;
    [SerializeField]
    private Sprite level7;
    [SerializeField]
    private Sprite level8;
    [SerializeField]
    private Sprite level9;
    [SerializeField]
    private Sprite level10;
    [SerializeField]
    private Sprite level11;
    [SerializeField]
    private Sprite level12;
    [SerializeField]
    private Sprite level13;
    [SerializeField]
    private Sprite level14;
    [SerializeField]
    private Sprite level15;
    [SerializeField]
    private Sprite happy;
    [SerializeField]
    private Sprite neutral;
    [SerializeField]
    private Sprite sad;

    private Sprite[] levels;
    private int level;
    private List<GameObject> customers;
    private sbyte csl; // Customer satisfaction level
                     //    public GameObject requirenmentPopup;
    private string dailySummaryStr;
    
    

    public int getCustomerSatisfactionlevel()
    {
        return csl;
    }

    public void increaseCsl()
    {
        if (csl < 15)
        {
            csl = (sbyte) (csl + 3);
        }

        changeCslIcon();
    }

    public void decreaseCsl()
    {
        if (csl > 0)
        {
            csl--;
        }

        changeCslIcon();
    }

    public void changeCslIcon()
    {
        if (csl > 12)
        {
            cslIcon.GetComponent<SpriteRenderer>().sprite = happy;
        }
        else if (csl <= 12 && csl > 4)
        {
            cslIcon.GetComponent<SpriteRenderer>().sprite = neutral;
        }
        else if (csl <= 4)
        {
            cslIcon.GetComponent<SpriteRenderer>().sprite = sad;
        }
    }

    public int presentCustomers()
    {
        return customers.Count;
    }

    public bool isShowcaseEmpty()
    {
        if(showcase.GetComponent<FurnitureShowcase>().getFurnitureCount() == 0){
            return true;
        }
        else
        {
            return false;
        }

    }

    public sbyte getCsl()
    {
        return csl;
    }

    public void setCsl(sbyte value)
    {
        csl = value;
    }


    void Awake(){
 //       requirenmentPopup.SetActive(false);
        customers = new List<GameObject>();
        player.GetComponent<Player>().setMoney(500);
        csl = 7;
        level = 1;
        levels = new Sprite[] { level1, level1, level2, level3, level4, level5, level6, level7, level8, level9, level10, level11, level12, level13, level14};
    }
    
    void Start(){
        //GameObject customer = (GameObject) customers[0];
        variables = GameObject.Find("variableObject");

        if(variables != null) 
        {
            player.GetComponent<Player>().setMoney(variables.GetComponent<Player>().getMoney());
            timer.GetComponent<timer>().Minute = variables.GetComponent<timer>().Minute;
            timer.GetComponent<timer>().Hour = variables.GetComponent<timer>().Hour;
            timer.GetComponent<timer>().TodayInNum = variables.GetComponent<timer>().TodayInNum;
            csl = variables.GetComponent<GameHandler>().csl;
        }

        csl = 7;
        changeCslIcon();

        StartCoroutine(generateCustomers());
    }

    void Update(){
        /*if(customers.Count == 0){
            spawnCustomer();
        }*/
        
    }

    public void removeCustomerFromList(GameObject cust)
    {
        customers.Remove(cust);
    }

    IEnumerator generateCustomers()
    {
        while (true) 
        { 
            if (customers.Count > 2)
            {
                yield return new WaitForSeconds(10);
                Debug.Log("customers.Count = " + customers.Count);
            } else {
                yield return new WaitForSeconds(3);
                Debug.Log("Generate Customer running");
                int hour = timer.GetComponent<timer>().Hour;
                float ranNum = Random.Range(0.0f, 100f);
                Debug.Log("randNum" + ranNum);
                Debug.Log("hour = " + hour);
                /* if (hour < 12 && hour >= 8)
                 {

                     if (ranNum < 15.0f)
                     {
                         spawnCustomer();
                     }
                 }
                 else if (hour >= 0 && hour < 3)
                 {
                     if (ranNum < 35.0f)
                     {
                         spawnCustomer();
                     }
                 }
                 else if (hour >= 3 && hour < 6)
                 {
                     if (ranNum < 70.0f)
                     {
                         Debug.Log("< 80.0f");
                         spawnCustomer();
                     }
                 }*/

                
                if (ranNum < 60.0f)
                {
                    spawnCustomer();
                }


            }

        }
    }

    public void spawnCustomer()
    {
        GameObject customer = Instantiate(cusomterPrefab, new Vector3(-6.62f, -7.0f, 0), Quaternion.identity);
        // add customer to List<GameObject>
        customer.GetComponent<Customer>().RequirementCanvas = canvas;
        customer.GetComponent<Customer>().showcase = showcase;
        customer.GetComponent<Customer>().player = player;
        customer.GetComponent<Customer>().gameHandler = this.gameObject;
        customer.GetComponent<Customer>().UI = UI;
        customers.Add(customer);

    }

    public GameObject selectCustomFurniture()
    {
        // getting the first customer in the queue
        GameObject customer = (GameObject) customers[0];

        // Get how many furniture player has placed in showcase
        int count = showcase.GetComponent<FurnitureShowcase>().getFurnitureCount();
        
        // player has made some furniture and save in showcase
        if(count != 0){

            // select the index of furniture a customer wants to purchase
            int selectIndex = (int)Random.Range(0, count - 1);
            GameObject temp = showcase.GetComponent<FurnitureShowcase>().getFurniture(selectIndex);

            return temp;
        }

        return null;
    }

    public void setFurniturPrice()
    {
        // need to implement 
    }

    public void sellFurniturToCustomer(GameObject cust, GameObject furniture)
    {
        int fid = furniture.GetComponent<Furniture>().getFID();
        Debug.Log("The fid of selected furniture is " + fid);
        int price = furniture.GetComponent<Furniture>().getPrice();
        Debug.Log("The price fo selected furniture is " + price);
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(fid);
        
        if (csl > 12)
        {
            player.GetComponent<Player>().addMoney(price + 100);
        }
        else if(csl < 12)
        {
            player.GetComponent<Player>().addMoney(price - 100);
        }
        else
        {
            player.GetComponent<Player>().addMoney(price);
        }

        increaseCsl();
        player.GetComponent<Player>().calculateScore(cust.GetComponent<Customer>().Patience, price);
        Destroy(furniture);
        changeLevelIcon();
        //customer.GetComponent<Customer>().leaveShop();
    }

    private void changeLevelIcon()
    {
        int score = player.GetComponent<Player>().getScore();
        if (score < (level+1)*100)
        {
            level++;
            levelIcon.GetComponent<Image>().sprite = levels[level];
        }

    }


    public void terminateRequest(){
        GameObject customer = (GameObject) customers[0];
        customers.RemoveAt(0);
        GameObject furniture = customer.GetComponent<Customer>().Furniture;
        Destroy(furniture);
        Destroy(customer);
        SpeechBubble.SetActive(false);
//        requirenmentPopup.SetActive(false);
//        customer.GetComponent<Customer>().leaveShop();
    }

    public void checkPatience(int index){
        GameObject customer = (GameObject) customers[index];
        if(customer.GetComponent<Customer>().decreasePatience()){
            terminateRequest();
        }
    }

    public void ShowDaysSummary()
    {
        Debug.Log("Running Game handler");
        StartCoroutine(LoadAsyncDailySummary());
        SceneManager.LoadScene("Day Summary");
    }

    IEnumerator LoadAsyncDailySummary()
    {
        variables = GameObject.Find("variableObject");
        DontDestroyOnLoad(variables);
        Player playerScript = player.GetComponent<Player>();

        variables.GetComponent<Player>().setMoney(player.GetComponent<Player>().getMoney());
        variables.GetComponent<Player>().DayExpenses = player.GetComponent<Player>().DayExpenses;
        variables.GetComponent<Player>().DayNetIncome = player.GetComponent<Player>().DayNetIncome;
        variables.GetComponent<Player>().DayRevenue = player.GetComponent<Player>().DayRevenue;
        variables.GetComponent<timer>().Minute = timer.GetComponent<timer>().Minute;
        variables.GetComponent<timer>().Hour = timer.GetComponent<timer>().Hour;
        variables.GetComponent<timer>().TodayInNum = timer.GetComponent<timer>().TodayInNum;
        variables.GetComponent<GameHandler>().csl = this.csl;

        Scene currentScene = SceneManager.GetActiveScene();
        dailySummaryStr = "Day Summary";
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dailySummaryStr, LoadSceneMode.Additive);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(variables, SceneManager.GetSceneByName(dailySummaryStr));
        SceneManager.UnloadSceneAsync(currentScene);
    }
    public void goToWorkshop()
    {
        SceneManager.LoadScene("Workshop");
    }


}



/*
 * 
 * - Customer satisfaction can be reduced by rejecting orders (-1), letting their patience bar fillup (-1), or overcharging furniture (-1)
 * - Customer satisfaction can be increased by selling furniture (+1)
 * - (overcharging furniture) customer overpriced will be randomly generated from 1.25 to 2 and if the price falls within that range then satisfaction (-1)
 * - Starting satisfaction is neutral with the value of 7
 * 
 * 
 * Customer satisfaction will have 3 levels:

Happy: 
----------------
- When customer satisfaction is between 12 and 15 (3 customers can be uncatered before back to neutral)
- Customer will give tips ( +$10) and have increased wait time

Neutral:
----------------
- When customer satisfaction is between  5 and 12 (in the neutral mode need to cater to 7 customers to get to happy)
- Customer will have normal wait time ( no advantages)


Angry: 
----------------
- When customer satisfaction is between 0 and 5 (in the angry mode need to tap 5 customers instantly to get to neutral)
- Customer will have reduced waiting time and will pay the cost price (-$profit)
- This will be shown in the customer requirement popup
  
  
  
 * Bugs which needs to be corrected:
 * 1) csl not working properly (and size not fit properly) ( Done)
 * 2) pop up should only be clickable when there is atleast on furniute in showcase (Done)
 * 3) maximum 3 customers can form a queue (Done)
 * 4) Customers patience should only start ticking when they reach counter (Done)
 * 5) 
 * 
 */




