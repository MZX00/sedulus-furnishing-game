using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public string[] Names;
    public GameObject[] Contents;
    public Text Money;
    public GameObject NotEnoughMoney,NotPurchasedYet;
    string Value;

    private int money;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveManager.LoadPlayer();

        if (data != null)
        {
            Debug.Log("Saved File exists");
            //money = data.money;
            money = 50000;
        }
        else
        {
            Debug.Log("Player Saved File do not exist");
            money = 0;

        }
        Debug.Log(money);
        Money.text = money.ToString();
        CheckPurchases();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void CheckPurchases()
    {
        foreach (GameObject item in Contents)
        {
            item.SetActive(false);
        }
        for (int i = 0,j=0; i < 33; i++)
        {
            Value = "Inv" + i;
            if (PlayerPrefs.GetInt(Value)>0)
            {
                GameObject Current = Contents[j];
                Current.SetActive(true);
                Current.GetComponent<Text>().text = Names[i];
                GameObject CurrentChild =  Current.transform.GetChild(0).gameObject;
                CurrentChild.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(Value).ToString()+" KG";
                j++;
            }
        }
    }
    public void BuyProduct(int num)
    {
        Value = "Inv" + num;
        if (money >= 100)
        {
            PlayerPrefs.SetInt(Value, PlayerPrefs.GetInt(Value) + 1);
            money -= 100;
            Money.text = money.ToString();
        }
        else
        {
            NotEnoughMoney.SetActive(true);
            Invoke("HideNotEnoughMoney", 2.0f);
        }
        CheckPurchases();
    }

    public void ReturnProduct( int num)
    {
        Value = "Inv" + num;
        if(PlayerPrefs.GetInt(Value)>0)
        {
            PlayerPrefs.SetInt(Value, PlayerPrefs.GetInt(Value) - 1);
            money += 100;
            Money.text = money.ToString();
        }
        else
        {
            NotPurchasedYet.SetActive(true);
            Invoke("HideNotPurchased", 2.0f);
        }
        CheckPurchases();

    }

    void HideNotPurchased()
    {
        NotPurchasedYet.SetActive(false);
    }
    void HideNotEnoughMoney()
    {
        NotEnoughMoney.SetActive(false);
    }
}
