using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money;
    private int score;


    public int getMoney()
    {
        return money;
    }

    public void setMoney(int amount)
    {
        money = amount;
    }

    public void addMoney(int amount)
    {
        money = money+ amount;
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
        }

    }
}
