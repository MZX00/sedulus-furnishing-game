using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Customer : MonoBehaviour
{
    //[SerializeField]
    private float speed = 0.4f;
    public GameObject gameHandler;
    public GameObject RequirementCanvas; // requirement pop up canvas
    public GameObject player; 
    public GameObject showcase;
    public GameObject speechBubblePrefab;
    public GameObject Furniture;
    private GameObject UI;
    private Button bubbleButton;
    private GameObject instance;
    private int csl;
    private int patience; // count down when customer reaches coutner 
    private sbyte journey; // +1 means that customer needs to go to counter and place their order 
    // 0 means that customer is in counter 
    // -1 means that customer is returning from counter
    private bool speechActive;  // boolean value to check if speech bubble is active or not
    private float timerPatience = 0.0f;
    private bool isattended;
    private bool orderplaced; // customer has placed his order or not 
    private sbyte customerType; // 1 means customer will return, 0 means customer will purchase thee furniture onspot
    private Rigidbody2D rb;

    private void Awake()
    {
        patience = 13;
    }


    void Start()
    {
        journey = 1;
        orderplaced = false;
        customerType = 1;
        speechActive = false;
        UI = GameObject.Find("UI");
        isattended = false;
        //RequirementCanvas = GameObject.Find("Game Handler").GetComponent<GameHandler>().canvas;
        RequirementCanvas.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        movement();
    }


    public sbyte Journey
    {
        get
        {
            return journey;
        }
        set
        {
            journey = value;
        }
    }
    public int Patience
    {
        get
        {
            return patience;
        }
        set
        {
            patience = value;
        }
    }

    public bool Orderplaced
    {
        get
        {
            return orderplaced;
        }
        set
        {
            orderplaced = value;
        }
    }


    public void setPatience(int patience){
        this.patience = patience;
    }

    // returns true when patience ends
    public bool decreasePatience(){
        if (this.patience > 0){
            this.patience -= 1;
            return false;
        }else{
            return true;
        }
    }

    public void movement()
    {

        if (journey == 1) //When Customer is moving towards counter
        {
            //transform.Translate(0, 1 * Time.deltaTime, 0);

            rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            //Vector3 newPosition = transform.position + (transform.up*Time.deltaTime);
            //rb.MovePosition(newPosition);
            if(transform.position.y > -0.53f)
            {
                Debug.Log("Customer reached counter");
                journey = 0;
            }
        }
        else if (journey == 0 ) //When cusotmer is standing at counter
        {
            if (!speechActive)
            {
                rb.velocity = Vector3.zero;
                //UI.GetComponent<uiscript>().showBubble();
                speechActive = true;
                // Need to render sprite when customer reaches counter 
                // and stop sprite render when customer goes out from counter  
                openBubble();
            }

            if (!isattended) {
                timerPatience += Time.deltaTime;
                // after every second the value of patience is decreased by 1
                if (timerPatience > 1.0f)
                {
                    if (decreasePatience())
                    {
                        journey = -1;
                        //UI.GetComponent<uiscript>().deleteBubble();
                        deleteBubble();
                        speechActive = false;
                    }
                    timerPatience = 0.0f;
                    Debug.Log(patience);
                }
            }
            
        }

        else if (journey == -1) //When customer is returning 
        {
            if (speechActive) {
                speechActive = false;
                //UI.GetComponent<uiscript>().deleteBubble();
                deleteBubble();
            }
            
            if (transform.position.x > -8.26f)
            {
                transform.Translate(-1 * Time.deltaTime, 0, 0);
                //rb.AddForce(new Vector3(-1,0,0) * speed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else {

                transform.Translate(0, -1 * Time.deltaTime, 0);
                //rb.AddForce(new Vector3(0, -1, 0) * speed * Time.deltaTime);

                if (transform.position.y < -7.0f)
                {
                    journey = 2;
                    GameObject.Find("Game Handler").GetComponent<GameHandler>().removeCustomerFromList(gameObject);
                    Destroy(instance);
                    Debug.Log("Customer Destroyed");
                    Destroy(this.gameObject);

                }
            }
        }
    }

    private void openBubble()
    {
        instance = Instantiate(speechBubblePrefab, new Vector3(-7.11f, 0.66f, 0), Quaternion.identity);
        instance.transform.parent = UI.transform;
        instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        bubbleButton = instance.GetComponent<Button>();
        bubbleButton.onClick.AddListener(delegate { openRequirementCanvas(); });
    }

    public void openRequirementCanvas()
    {

        Debug.Log("RequirementCanvas.activeInHierarchy " + RequirementCanvas.activeSelf);
        isattended = true;
        deleteBubble();
        Debug.Log(isattended);
        Debug.Log("Bubble button clicked");
        RequirementCanvas.SetActive(true);

        Furniture = gameHandler.GetComponent<GameHandler>().selectCustomFurniture();

        if (Furniture != null)
        {
            // create the instance of the furniture customer selected
            GameObject furn = Instantiate(Furniture, new Vector3(0, 0, 0), Quaternion.identity);
            // save the slected furniture in customer's class
            Furniture = furn;

            furn.transform.SetParent(RequirementCanvas.transform.GetChild(2).transform,false);
            RectTransform furntrt = (RectTransform)furn.transform;
            furntrt.anchorMax = new Vector2(0.5f, 0.5f);
            furntrt.anchorMin = new Vector2(0.5f, 0.5f);
            furntrt.pivot = new Vector2(0.5f, 0.5f);

            //Find("Furniture Panel")
        }
        else
        {
            Debug.Log("Furniture not found");
        }

        // Find close button 
        GameObject closeBtnGameObject = RequirementCanvas.transform.Find("Minimize Button").gameObject;
        if (closeBtnGameObject == null)
        {
            Debug.Log("minimze button not found");
        }

        Button closeBtn = closeBtnGameObject.GetComponent<Button>();
        closeBtn.onClick.AddListener(closeRequirementCanvas);

        Button sellButton = RequirementCanvas.transform.Find("Sell Button").gameObject.GetComponent<Button>();
        if (sellButton == null)
        {
            Debug.Log("sell gameobject or button not found");
        }
        Debug.Log("Before listner");
        sellButton.onClick.AddListener(sellFurniture);
        Debug.Log("After listner");
        // Button  = sellButtonGameObject.GetComponent<Button>();


    }

    public void sellFurniture()
    {
        closeRequirementCanvas();
        Furniture = GameObject.Find("Game Handler").GetComponent<GameHandler>().selectCustomFurniture();
        GameObject.Find("Game Handler").GetComponent<GameHandler>().sellFurniturToCustomer(this.gameObject, this.Furniture);
        //        sellFurniturToCustomer();
    }

  /*  public void sellFurniturToCustomer()
    {
        GameObject furniture = Furniture;
        int fid = furniture.GetComponent<Furniture>().getFID();
        int price = furniture.GetComponent<Furniture>().getPrice();
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(fid);
        player.GetComponent<Player>().addMoney(price);
        csl += 1;
        GameObject.Find("Game Handler").GetComponent<GameHandler>().removeCustomerFromList(gameObject);
        Destroy(furniture);
        Destroy(gameObject);
        //customer.GetComponent<Customer>().leaveShop();
    }
  */
    public void selectCustomFurniture()
    {
        // Get how many furniture player has made
        int count = showcase.GetComponent<FurnitureShowcase>().getFurnitureCount();
        Debug.Log("Furniture Count = " + count);
        // player has made some furniture and save in showcase
        if (count != 0)
        {

            // select the index of furniture a customer wants to purchase
            int selectIndex = (int)Random.Range(0, count - 1);
            GameObject temp = showcase.GetComponent<FurnitureShowcase>().getFurniture(selectIndex);

            // create the instance of the furniture customer selected
            GameObject furn = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity);
            // save the slected furniture in customer's class
            Furniture = furn;

            //            furn.transform.SetParent(requirenmentPopup.transform.Find("Furniture Panel").transform,false);
            RectTransform furntrt = (RectTransform)furn.transform;
            furntrt.anchorMax = new Vector2(0.5f, 0.5f);
            furntrt.anchorMin = new Vector2(0.5f, 0.5f);
            furntrt.pivot = new Vector2(0.5f, 0.5f);

            //Find("Furniture Panel")
        }


    }

    /*    public void terminateRequest()
    {
        //GameObject customer = (GameObject)customers[0];
        //customers.RemoveAt(0);
        GameObject.Find("Game Handler").GetComponent<GameHandler>().removeCustomerFromList(gameObject);
        //GameObject furniture = customer.GetComponent<Customer>().Furniture;
        GameObject furniture = Furniture;
        deleteBubble(); //SpeechBubble.SetActive(false);
        Destroy(furniture);
        Destroy(gameObject);


        //        requirenmentPopup.SetActive(false);
        //        customer.GetComponent<Customer>().leaveShop();
    }
*/

    public void OnCollisionEnter()
    {
        Debug.Log("Runinng oncollision enter");
        decreasePatience();
    }

    public void deleteBubble()
    {
        Destroy(instance);
        Debug.Log("speech bubble is destroyed");
    }

    
    public void closeRequirementCanvas()
    {
        RequirementCanvas.SetActive(false);
        // customer will return
        journey = -1;
    }


    /*private void OnTriggerEnter2D(Collider2D counterCollider)
    {
        if(counterCollider.gameObject.name == "cusomterPrefab(Clone)")
        {
            Debug.Log("Evil Laugh ");
        }
        else
        {
            Debug.Log("Customer reached counter");
            journey = 0;
            *//*customerBubbleSpeech.SetActive(true);*//*
        }
    }*/
}





/* 
 * 
 * 1) Correct movements (Done Donea Done)
 * 2) sell furniture will be in game handler, sell button will execute sellfurniture of gamehandler(customer, furniture);(Done Donea Done)
 * 3) patience should continue then requirement popup is closed  // need to work on this 
 * 4) Form proper queue (Done Donea Done)
 * 5) Random generation of customer 
 * 6) Add timer and it's functionality (Highest priority)
 * 7) Customer Request type 
 * 8) When day ends, change scene(to new scene)
 * 9) Money, increase, decrease, count, how much sold in the entire day
 * 10) customer satisfaction level implementation and changing of icon.
 * 11) 
 * 
 *  Add Animation if possible.
 * 
 */
