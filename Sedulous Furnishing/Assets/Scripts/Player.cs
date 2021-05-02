using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Text moneyText;
    private int money;
    private int score;


    public int getMoney()
    {
        return money;
    }

    public void setMoney(int amount)
    {
        money = amount;
        updateMoney();
    }

    public void addMoney(int amount)
    {
        money = money+ amount;
        updateMoney();
    }

    public void subtractMoney(int amount)
    {
        if (money < amount)
        {
            Debug.Log("Can not subtract Money");
        }
        else
        {
            money = money - amount;
            updateMoney();
        }


    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int amount)
    {
        score = amount;
    }

    public void addScore(int amount)
    {
        score = score + amount;
    }

    public void subtractScore(int amount)
    {
        if (score < amount)
        {
            Debug.Log("Can not subtract Money");
        }
        else
        {
            score = score - amount;
            updateMoney();
        }

    }

    public void updateMoney()
    {
        if (moneyText != null)
        {
            moneyText.text = "" + money;
        }
        else
        {
            Debug.Log("money text do not exists");
        }
        
    }
}
