using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerHandler : MonoBehaviour
{
    private List<GameObject> customers;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] GameObject customerRequirementCanvas1;
    [SerializeField] GameObject customerRequirementCanvas2;
    [SerializeField] GameObject speechBubble;
    int customerCounter;
    void Start(){
        customers = new List<GameObject>();
        StartCoroutine(spawnCustomer());
    }

    IEnumerator spawnCustomer()
    {
        while (true) 
        { 
            if ( customers.Count > 2)
            {
                yield return new WaitForSeconds(10);
            } else {
                yield return new WaitForSeconds(3);
                float ranNum = Random.Range(0.0f, 100f);
                if (ranNum < 60.0f)
                {
                    GameObject customer = Instantiate(customerPrefab, new Vector3(-6.62f, -7.0f, 0), Quaternion.identity);
                    customer.GetComponent<Customer>().speechBubble = speechBubble;
                    customer.GetComponent<Customer>().csl = GetComponent<SellHandler>().csl;
                    customer.GetComponent<Customer>().sellHandler = GetComponent<SellHandler>();

                    customers.Add(customer);
                }
            }
            
        }
    }

    public void setJourney(){
        customers[0].GetComponent<Customer>().Journey = -1;
        customers.RemoveAt(0);
    }

    public void openRequirementCanvas(){
        customers[0].GetComponent<Customer>().Journey = 2;
        sbyte type = (sbyte) Random.Range(0, 2);
        if(GetComponent<SellHandler>().checkSelected()){

            GameObject temp = GetComponent<SellHandler>().selectFurniture();

            if(temp != null){
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().text = "···";
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().fontSize = 100;

                temp.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f,0.5f);
                temp.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,0.5f);

                temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);

                temp.transform.SetParent(customerRequirementCanvas1.transform.GetChild(2).GetComponent<RectTransform>(),false);
                
                customerRequirementCanvas1.SetActive(true);
            }else{
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().text = "No Furniture!";
                speechBubble.GetComponentInChildren<TextMeshProUGUI>().fontSize = 24;
            }
        }
        // if(type == 0){
        //     customerRequirementCanvas1.SetActive(true);
        // }else{
        //     customerRequirementCanvas2.SetActive(true);
        // }
    }

    public void removeCustomer(GameObject cust){
        if(customers.Count > 0){
            customers.Remove(cust);
        }else{
            Debug.Log("MEIN 0 HOGAYA ");
        }
        
    }
    public void closeRequirementCanvas(){
        customers[0].GetComponent<Customer>().Journey = 0;
        GetComponent<SellHandler>().returnFurniture();
        customerRequirementCanvas1.SetActive(false);
        customerRequirementCanvas2.SetActive(false);
    }

}
