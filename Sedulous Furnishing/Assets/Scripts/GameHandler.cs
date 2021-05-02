using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour
{
    public GameObject canvas;
    public GameObject cusomterPrefab;
    public GameObject showcase;
    public GameObject player;
    public GameObject cslIcon;
    public GameObject SpeechBubble;
    private List<GameObject> customers;
    private int csl; // Customer satisfaction level
//    public GameObject requirenmentPopup;

    public int getCustomerSatisfactionlevel()
    {
        return csl;
    }


    void Awake(){
 //       requirenmentPopup.SetActive(false);
        customers = new List<GameObject>();
        player.GetComponent<Player>().setMoney(500);
        csl = 7;

        // creating first csutomer
        spawnCustomer();
    }
    
    void Start(){
        //GameObject customer = (GameObject) customers[0];

        StartCoroutine(newCustomer());
        StartCoroutine(newCustomer1());

    }

    void Update(){
        if(customers.Count == 0){
            spawnCustomer();
        }

        /*for(int i = 0; i<customers.Count; i++){
            checkPatience(i);
        }*/
    }

    IEnumerator newCustomer()
    {
        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(3);
        spawnCustomer();
    }

    IEnumerator newCustomer1()
    {
        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(6);
        spawnCustomer();
    }

    public void removeCustomerFromList(GameObject cust)
    {
        customers.Remove(cust);
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

    public GameObject selectCustomFurniture(){

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
    public void sellFurniturToCustomer(GameObject cust, GameObject furniture){


        /*
         * Step 1: player clicks on sell button 
         * setp 2: destroy item from showcase 
         * step 3: add money of the player
         * step 4: Destroy customer
         * 
         */

        // remove furniture from showcase 

        /*GameObject customer = (GameObject)customers[0];
        GameObject furniture = customer.GetComponent<Customer>().Furniture;
        int fid = furniture.GetComponent<Furniture>().getFID();*/

        int fid = furniture.GetComponent<Furniture>().getFID();
        Debug.Log("The fid of selected furniture is " + fid);
        int price = furniture.GetComponent<Furniture>().getPrice();
        Debug.Log("The price fo selected furniture is " + price);
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(fid);
        player.GetComponent<Player>().addMoney(price);
        csl += 1;
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
            csl -= 1;
        }
    }
}
