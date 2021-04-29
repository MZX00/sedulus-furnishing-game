using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Customer : MonoBehaviour
{
    public GameObject RequirementCanvas;
    private GameObject UI;
    public GameObject bubble;
    private Button bubbleButton;
    private GameObject instance;
    public GameObject Furniture;
    private int patience; // count down when customer reaches coutner 
    private sbyte journey; // +1 means that customer needs to go to counter and place their order 
    // 0 means that customer is in counter 
    // -1 means that customer is returning from counter
    private bool speechActive;  // boolean value to check if speech bubble is active or not
    private float timerPatience = 0.0f;
    private static bool isattened;



    private bool orderplaced; // customer has placed his order or not 
    private sbyte customerType; // 1 means customer will return, 0 means customer will purchase thee furniture onspot


    void Start()
    {
        journey = 1;
        patience = 3;
        orderplaced = false;
        customerType = 1;
        speechActive = false;
        UI = GameObject.Find("UI");
        isattened = false;
        

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
            transform.Translate(0, 1 * Time.deltaTime, 0);
        }
        else if (journey == 0 ) //When cusotmer is standing at counter
        {
            if (!speechActive)
            {
                //UI.GetComponent<uiscript>().showBubble();
                speechActive = true;
                // Need to render sprite when customer reaches counter 
                // and stop sprite render when customer goes out from counter  
                openBubble();
            }

            if (!isattened) {
                Debug.Log(isattened);
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
            }
            else { 
                transform.Translate(0, -1 * Time.deltaTime, 0);

                if(transform.position.y < -7.0f)
                {
                    Debug.Log("Customer Destroyed");
                    journey = 2;
                    GameObject.Find("Game Handler").GetComponent<GameHandler>().removeCustomerFromList(gameObject);
                    // need to remove this customer rom gamehandler's 
                    Destroy(instance);
                    Destroy(this);

                }
            }
        }
    }

    private void openBubble()
    {
        instance = Instantiate(bubble, new Vector3(-7.11f, 0.66f, 0), Quaternion.identity);
        instance.transform.parent = UI.transform;
        instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        bubbleButton = instance.GetComponent<Button>();
    }

    public void deleteBubble()
    {
        Destroy(instance);
    }

    public void openCanvas()
    {

        isattened = true;
        Debug.Log(isattened);
        Debug.Log("Bubble button has been clicked");
        GameObject requirementCanvs = Instantiate(RequirementCanvas, new Vector3(0, 0, 0), Quaternion.identity);

    }


    private void OnTriggerEnter2D(Collider2D counterCollider)
    {
        Debug.Log("Customer reached counter");
        journey = 0;
        /*customerBubbleSpeech.SetActive(true);*/

    }
}
