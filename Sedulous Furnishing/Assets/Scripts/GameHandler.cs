using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour
{

    public GameObject cusomterPrefab;
    public GameObject showcase;
    public GameObject player;
    public GameObject cslIcon;
    public GameObject SpeechBubble;
    private ArrayList customers;
    private int csl; // Customer satisfaction level
    public GameObject requirenmentPopup;

    void Awake(){
        requirenmentPopup.SetActive(false);
        customers = new ArrayList();
        csl = 7;
        GameObject customer = Instantiate(cusomterPrefab, new Vector3(-7.62f, 0.32f, 0), Quaternion.identity);
        customers.Add(customer);
    }
    
    void Start(){
        GameObject customer = (GameObject) customers[0];
        customer.GetComponent<Customer>().setPatience(30);
    }

    void Update(){
        if(customers.Count == 0){
            GameObject cust = Instantiate(cusomterPrefab, new Vector3(-7.62f, 0.32f, 0), Quaternion.identity);
            customers.Add(cust);
            cust.GetComponent<Customer>().setPatience(30);
        }
        for(int i = 0; i<customers.Count; i++){
            checkPatience(i);
        }
    }

    public void selectCustomFurniture(){
        GameObject customer = (GameObject) customers[0];
        int count = showcase.GetComponent<FurnitureShowcase>().getFurnitureCount();
        if(count != 0){
            int selectIndex = (int) Random.Range(0, count-1);
            GameObject temp = showcase.GetComponent<FurnitureShowcase>().getFurniture(selectIndex);
            GameObject furn = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity);
            customer.GetComponent<Customer>().Furniture = furn;
            furn.transform.SetParent(requirenmentPopup.transform.Find("Furniture Panel").transform,false);
            RectTransform furntrt = (RectTransform) furn.transform;
            furntrt.anchorMax = new Vector2(0.5f,0.5f);
            furntrt.anchorMin = new Vector2(0.5f,0.5f);
            furntrt.pivot = new Vector2(0.5f,0.5f);

            //Find("Furniture Panel")
        }
        

    }
    public void sellFurniturToCustomer(){
        GameObject customer = (GameObject) customers[0];
        GameObject furniture = customer.GetComponent<Customer>().Furniture;
        int fid = furniture.GetComponent<Furniture>().getFID();
        int price = furniture.GetComponent<Furniture>().getPrice();
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(fid);
        player.GetComponent<Player>().addMoney(price);
        csl += 1;
        Destroy(customer);
        Destroy(furniture);
        customers.RemoveAt(0);
        //customer.GetComponent<Customer>().leaveShop();
    }

    public void terminateRequest(){
        GameObject customer = (GameObject) customers[0];
        customers.RemoveAt(0);
        GameObject furniture = customer.GetComponent<Customer>().Furniture;
        Destroy(furniture);
        Destroy(customer);
        SpeechBubble.SetActive(false);
        requirenmentPopup.SetActive(false);
        //customer.GetComponent<Customer>().leaveShop();
    }

    public void checkPatience(int index){
        GameObject customer = (GameObject) customers[index];
        if(customer.GetComponent<Customer>().decreasePatience()){
            terminateRequest();
            csl -= 1;
        }
    }


}
