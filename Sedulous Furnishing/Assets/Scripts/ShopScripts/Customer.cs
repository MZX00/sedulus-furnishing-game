using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class Customer : MonoBehaviour
{
    public GameObject speechBubble;
    public CslHandler csl;
    public SellHandler sellHandler;
    public Animator animator;
    public sbyte Journey{
        set{
            journey = value;
        }
    }

    int updateCount = 0;

    [SerializeField]private sbyte patience, journey;
    private float timerPatience = 0.0f;

    private bool colliding;

    void Awake(){
        journey = 1;
        setPatience();
        animator.SetInteger("Journey", -1);
    }

    void FixedUpdate(){
        if(journey == 0){
            waitingAtCounter();
        }else if(journey == 1){
            moveToCounter();
        }else if(journey == -1){
            moveToBack();
        }
    }
    public void moveToCounter(){
        if (!colliding)
        {
            animator.SetInteger("Journey", -1);
            transform.Translate(0, 2 * Time.deltaTime, 0);
        }
        else
        {
            animator.SetInteger("Journey", 0);
        }
        if (transform.position.y > -0.53f)
        {
            journey = 0;
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("New Trigger");
        }
    }

    public void waitingAtCounter(){
        animator.SetInteger("Journey", 1);
        speechBubble.SetActive(true);

        if (transform.position.y < -0.30f)
        {
            transform.Translate(0, 1 * Time.deltaTime, 0);
        }
        timerPatience += Time.deltaTime;
        // after every second the value of patience is decreased by 1
        if (timerPatience > 1.0f)
        {
            if (decreasePatience())
            {
                journey = -1;
            }
            timerPatience = 0.0f;
        }
    }

    public void moveToBack(){
        if (updateCount == 0){
            Debug.Log(" I RAN ");
            sellHandler.gameObject.GetComponent<CustomerHandler>().removeCustomer(this.gameObject);
            speechBubble.SetActive(false);
            updateCount = 1;
        }
        

        if (transform.position.x > -8.26f){
            animator.SetInteger("Journey", 2);
            transform.Translate(-1 * Time.deltaTime, 0, 0);
        }else {
            animator.SetInteger("Journey", 3);
            transform.Translate(0, -1 * Time.deltaTime, 0);
            if (transform.position.y < -7.0f){
                animator.SetInteger("Journey", 4);
                Destroy(gameObject);
            }
        }
    }

    public bool decreasePatience(){
        
        if (patience > 0){
            if(patience == 1){
                speechBubble.SetActive(false);
                speechBubble.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            patience -= 1;
            return false;
        }else{
            // If patience has ended then decrease customer satisfaction level
            csl.decreaseCsl();
            setPatience();
            return true;
        }
    }

    public void setPatience(){
        patience = 13;
        //CSL affecting patience
        // if (csl.CSL > 12)
        //{
        //    patience = 25;
        //}
        //else if (csl.CSL <= 12 && csl.CSL > 4)
        //{
        //    patience = 20;
        //}
        //else if (csl.CSL <= 4)
        //{
        //    patience = 15;
        //}
    }


    //Collider detection between customers
    void OnCollisionEnter2D()
    {
        colliding = true;
    }

    void OnCollisionExit2D()
    {
        colliding = false;
    }

}
