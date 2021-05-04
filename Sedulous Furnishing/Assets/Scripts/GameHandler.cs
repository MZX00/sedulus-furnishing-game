using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject timer;
    
    private string dailySummaryStr;
    public GameObject canvas;
    public GameObject cusomterPrefab;
    public GameObject showcase;
    public GameObject player;

    [SerializeField]
    private GameObject cslIcon;
    [SerializeField]
    private Sprite happy;
    [SerializeField]
    private Sprite neutral;
    [SerializeField]
    private Sprite sad;

    public GameObject SpeechBubble;
    private List<GameObject> customers;
    private int csl; // Customer satisfaction level
                     //    public GameObject requirenmentPopup;
    public GameObject variables;

    public int getCustomerSatisfactionlevel()
    {
        return csl;
    }

    public void increaseCsl()
    {
        if (csl < 15)
        {
            csl++;
        }

        changeCslIcon();
    }

    public void decreaseCsl()
    {
        if (csl >= 0)
        {
            csl--;
        }

        changeCslIcon();
    }

    public void changeCslIcon()
    {
        if (csl > 12)
        {
            CslIconHappy();
        }
        else if (csl <= 12 && csl > 4)
        {
            CslIconNeutral();
        }
        else if (csl <= 4)
        {
            CslIconSad();
        }
    }



    void Awake(){
 //       requirenmentPopup.SetActive(false);
        customers = new List<GameObject>();
        player.GetComponent<Player>().setMoney(500);
        csl = 6;

        // creating first csutomer
        spawnCustomer();
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
            yield return new WaitForSeconds(3);
            Debug.Log("Generate Customer running");
            int hour = timer.GetComponent<timer>().Hour;
            float ranNum = Random.Range(0.0f, 100f);
            Debug.Log("randNum" + ranNum);
            Debug.Log("hour = " + hour);
            if (hour < 12 && hour > 8)
            {
            
                if ( ranNum<10.0f)
                {
                    spawnCustomer();
                }
            } else if(hour > 0 && hour < 3){
                if (ranNum < 25.0f)
                {
                    spawnCustomer();
                }
            }
            else if (hour > 3 && hour < 6)
            {
                if (ranNum < 50.0f)
                {
                    Debug.Log("< 80.0f");
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
        customers.Add(customer);

    }

    public GameObject selectCustomFurniture()
    {
        // getting the first customer in the queue
        GameObject customer = (GameObject) customers[0];

        // Get how many furniture player has made
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
    public void sellFurniturToCustomer(GameObject cust, GameObject furniture)
    {
        int fid = furniture.GetComponent<Furniture>().getFID();
        Debug.Log("The fid of selected furniture is " + fid);
        int price = furniture.GetComponent<Furniture>().getPrice();
        Debug.Log("The price fo selected furniture is " + price);
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(fid);
        player.GetComponent<Player>().addMoney(price);
        increaseCsl();
        Destroy(furniture);
        //customer.GetComponent<Customer>().leaveShop();
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

    public void CslIconHappy()
    {
        cslIcon.GetComponent<SpriteRenderer>().sprite = happy;
    }

    public void CslIconNeutral()
    {
        cslIcon.GetComponent<SpriteRenderer>().sprite = neutral;
    }

    public void CslIconSad()
    {
        cslIcon.GetComponent<SpriteRenderer>().sprite = sad;
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
- When customer satisfaction is between 15 (3 customers can be uncatered before back to neutral)
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
 * 
 */
