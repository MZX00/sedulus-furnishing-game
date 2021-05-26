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
    private GameObject cslIcon;
    [SerializeField]
    private GameObject customRequirementCanvas;

    public GameObject canvas;
    public GameObject cusomterPrefab;
    public GameObject showcase;
    public GameObject player;
    public GameObject SpeechBubble;


    [SerializeField]
    private Sprite happy;
    [SerializeField]
    private Sprite neutral;
    [SerializeField]
    private Sprite sad;

    
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
        Debug.Log("csl = " + csl);
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
//        player.GetComponent<Player>().setMoney(500);
    }
    
    void Start(){
        //GameObject customer = (GameObject) customers[0];

        gamehandlerData data = SaveManager.loadGamehandler();

        if (data != null)
        {
            Debug.Log("Game Handler Saved File exist");
            csl = data.csl;
            Debug.Log("value of csl =" + csl);
        }
        else
        {
            Debug.Log("Game Handler  Saved File do not exists");
            Debug.Log("Game Handler  Saved File do not exists");
            Debug.Log("Game Handler  Saved File do not exists");
            csl = 7;

        }

        changeCslIcon();


        Debug.Log("csl = " + csl);
        StartCoroutine(generateCustomers());
    }

    void Update(){
        
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
                int hour = timer.GetComponent<timer>().Hour;
                float ranNum = Random.Range(0.0f, 100f);
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
        customer.GetComponent<Customer>().customRequirementCanvas = customRequirementCanvas;
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

    public void sellFurniturToCustomer(GameObject cust, GameObject furniture)
    {
        int fid = furniture.GetComponent<Furniture>().FID;
        int price = furniture.GetComponent<Furniture>().DisplayPrice;
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
        player.GetComponent<Player>().changeLevelIcon();
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
        SaveManager.SavePlayer(player.GetComponent<Player>());
        SceneManager.LoadScene("Day Summary");
    }

    public void goToWorkshop()
    {
        SaveManager.saveDate(timer.GetComponent<timer>());
        SaveManager.SavePlayer(player.GetComponent<Player>());
        SceneManager.LoadScene("Workshop");
    }


}

