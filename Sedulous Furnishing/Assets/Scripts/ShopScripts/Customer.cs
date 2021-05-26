using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class Customer : MonoBehaviour
{
    //[SerializeField]
    // private float speed = 0.4f;
    public GameObject UI;
    public GameObject gameHandler;
    public GameObject customRequirementCanvas;
    public GameObject RequirementCanvas; // requirement pop up canvas
    public GameObject player; 
    public GameObject showcase;
    public GameObject speechBubblePrefab;
    public GameObject Furniture;
    private Button bubbleButton;
    private GameObject instance;

    [SerializeField]
    private int patience; // count down when customer reaches coutner 
    [SerializeField]
    private int countdown;

    private sbyte journey; // +1 means that customer needs to go to counter and place their order 
    // 0 means that customer is in counter 
    // -1 means that customer is returning from counter
    private bool speechActive;  // boolean value to check if speech bubble is active or not
    private float timerPatience = 0.0f; // to check for  1 second
    private bool isattended;
    private bool orderplaced; // customer has placed his order or not 
    private bool isCustomOrder; // true means customer will have a custom requirement, false means customer will purchase thee furniture onspot
    private Rigidbody2D rb;
    private bool colliding;
    private bool itemsold;

    void Start()
    {
        journey = 1;
        orderplaced = false;
        
        float ranNum = Random.Range(0.0f, 100f);
        if(ranNum < 00.0f)
        {
            isCustomOrder = true;
        }
        else
        {
            isCustomOrder = false;
        }

        speechActive = false;
        isattended = false;
        //RequirementCanvas = GameObject.Find("Game Handler").GetComponent<GameHandler>().canvas;
        RequirementCanvas.SetActive(false); 
        rb = gameObject.GetComponent<Rigidbody2D>();
        patience = 13;
        colliding = false;
        itemsold = false;

    }

    void FixedUpdate()
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
            // If patience has ended then decrease customer satisfaction level
            gameHandler.GetComponent<GameHandler>().decreaseCsl();
            return true;
        }
    }

    public void movement()
    {
        if (journey == 1) //When Customer is moving towards counter
        {
            if (!colliding)
            {
                transform.Translate(0, 1 * Time.deltaTime, 0);
            }
            if (transform.position.y > -0.53f)
            {
                Debug.Log("Customer reached counter");
                journey = 0;
            }
        }
        else if (journey == 0 ) //When cusotmer is standing at counter
        {
            if (transform.position.y < -0.53f)
            {
                transform.Translate(0, 1 * Time.deltaTime, 0);
            }

            if (!speechActive)
            {
                rb.velocity = Vector3.zero;

                // Need to render sprite when customer reaches counter 
                // and stop sprite render when customer goes out from counter  

                if (isCustomOrder)
                {
                    openBubble();
                    speechActive = true;
                }
                else
                {
                    if (!GameObject.Find("Game Handler").GetComponent<GameHandler>().isShowcaseEmpty())
                    {
                        if (!itemsold)
                        {
                            openBubble();
                            speechActive = true;
                        }

                    }
                }
            }

            if (!isattended)
            {

                timerPatience += Time.deltaTime;
                // after every second the value of patience is decreased by 1
                if (timerPatience > 1.0f)
                {
                    if (decreasePatience())
                    {
                        journey = -1;
                    }
                    timerPatience = 0.0f;
                    Debug.Log("Patience = " + patience);
                }
            }

        }

        else if (journey == -1) //When customer is returning 
        {
            if (speechActive) {
                speechActive = false;
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
                    GameObject.Find("Game Handler").GetComponent<GameHandler>().removeCustomerFromList(this.gameObject);
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
        if (!isCustomOrder)
        {
            bubbleButton.onClick.AddListener(delegate { openRequirementCanvas(); });
        }
        else
        {
            bubbleButton.onClick.AddListener(delegate { openCustomRequirementCanvas(); });
        }
    }


    public void customRequestDeclined()
    {
        customRequirementCanvas.SetActive(false);
        journey = -1;
    }

    public void customRequestAccepted()
    {
        customRequirementCanvas.SetActive(false);
        
        
        SceneManager.LoadScene("Workshop");
    }

    public void openCustomRequirementCanvas()
    {
        isattended = true;
        deleteBubble();
        speechActive = false;
        customRequirementCanvas.SetActive(true);
        

        Button declineButton = customRequirementCanvas.transform.Find("Decline Button").GetComponent<Button>();

        if(declineButton == null)
        {
            Debug.Log("Decline Button not found");
        }

        declineButton.onClick.AddListener(customRequestDeclined);

        
        Button acceptButton = customRequirementCanvas.transform.Find("Accept Button").GetComponent<Button>();

        if (acceptButton == null)
        {
            Debug.Log("Accept Button not found");
        }

        acceptButton.onClick.AddListener(customRequestAccepted);


    }

    public void openRequirementCanvas()
    {

        isattended = true;
        deleteBubble();
        speechActive = false;
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
        itemsold = true;
        isattended = false;
        closeRequirementCanvas();
        Furniture = GameObject.Find("Game Handler").GetComponent<GameHandler>().selectCustomFurniture();
        GameObject.Find("Game Handler").GetComponent<GameHandler>().sellFurniturToCustomer(this.gameObject, this.Furniture);
        Debug.Log("Furniture Sold");
        //        sellFurniturToCustomer();
    }

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
    public void deleteBubble()
    {
        isattended = false;
        Destroy(instance);
        Debug.Log("speech bubble is destroyed");
    }
    
    public void closeRequirementCanvas()
    {
        RequirementCanvas.SetActive(false);
        gameHandler.GetComponent<GameHandler>().decreaseCsl();
        // customer will return
        journey = -1;
    }

    void OnCollisionEnter2D()
    {
        colliding = true;
    }

    void OnCollisionExit2D()
    {
        colliding = false;
    }
}


/* 
 * Tasks
 * (1) Customer Request type 
 * (2) add sound, when customer reaches to counter a bell sound will ring 
 * (3) Add Animation 
 * 
 * Center heading "parts list"
 * 1) name of the part  eg .Arm
 * 2) matrial 
 * 3) quantity
 * 
 * 
 * 
 */
