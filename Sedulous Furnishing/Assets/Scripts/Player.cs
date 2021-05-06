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

    private int dayNetIncome;
    private int dayExpenses;
    private int dayRevenue;


    public int DayNetIncome
    {
        get
        {
            return dayNetIncome;
        }
        set
        {
            dayNetIncome = value;
        }
    }

    public int DayExpenses
    {
        get
        {
            return dayExpenses;
        }
        set
        {
            dayExpenses = value;
        }
    }

    public int DayRevenue
    {
        get
        {
            return dayRevenue;
        }
        set
        {
            dayRevenue = value;
        }
    }

    private void Awake()
    {
        dayExpenses = 0;
        dayNetIncome = 0;
        dayRevenue = 0;
    }


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
        dayRevenue = dayRevenue + amount;
        dayNetIncome = dayNetIncome + amount;
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
            dayExpenses = dayExpenses + amount;
            dayNetIncome = dayNetIncome - amount;
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
        Debug.Log("score = " + score);
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

    public void calculateScore(int patience, int costOfFurnitur)
    {
        if(patience > 6)
        {
            int temp = (int)((patience * costOfFurnitur) / 800);
            addScore(temp);
        }
    }
}
