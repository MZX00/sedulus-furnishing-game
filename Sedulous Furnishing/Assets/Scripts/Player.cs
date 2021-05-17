using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private GameObject levelIcon;

    private int money;
    private int score;
    private int level;

    private Sprite[] levels;
    private int dayNetIncome;
    private int dayExpenses;
    private int dayRevenue;


    [SerializeField]
    private Sprite level1;
    [SerializeField]
    private Sprite level2;
    [SerializeField]
    private Sprite level3;
    [SerializeField]
    private Sprite level4;
    [SerializeField]
    private Sprite level5;
    [SerializeField]
    private Sprite level6;
    [SerializeField]
    private Sprite level7;
    [SerializeField]
    private Sprite level8;
    [SerializeField]
    private Sprite level9;
    [SerializeField]
    private Sprite level10;


    private void Awake()
    {
        dayExpenses = 0;
        dayNetIncome = 0;
        dayRevenue = 0;
        levels = new Sprite[] { level1, level1, level2, level3, level4, level5, level6, level7, level8, level9, level10 };
    }



    void Start()
    {

        PlayerData data = SaveManager.LoadPlayer();
        Debug.Log("data = "+ data);

        if (data != null)
        {
            Debug.Log("Saved File exists");
            money = data.money;
            score = data.score;
        }
        else
        {
            Debug.Log("Player Saved File do not exist");
            Debug.Log("Player Saved File do not exist");
            Debug.Log("Player Saved File do not exist");
            money = -500;
            score = -500;

        }
        level = (score / 100);

        if(level < 11)
        {
            Debug.Log(level);
            levelIcon.GetComponent<Image>().sprite = levels[level];
        }
        
        updateMoney();
        Debug.Log("level = " + level);
        Debug.Log("score = " + score);
    }

    public void changeLevelIcon()
    {
        Debug.Log("Change level icon running");
        Debug.Log("level = " + level);
        Debug.Log("score = " + score);

        level = score / 100;

        if(level < 11) 
        {
            levelIcon.GetComponent<Image>().sprite = levels[level];
        }
    }


    public int DayNetIncome
    {
        get
        {
            return dayNetIncome;
        }
        set
        {
            if(value > -1)
            {
                dayNetIncome = value;
            }
            else
            {
                Debug.Log("Inappropriate value = " + value);
            }
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
            if (value > -1)
            {
                dayExpenses = value;
            }
            else
            {
                Debug.Log("Inappropriate value = " + value);
            }
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
            if (value > -1)
            {
                dayRevenue = value;
            }
            else
            {
                Debug.Log("Inappropriate value = " + value);
            }
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int amount)
    {
        if(amount > -1)
        {
            money = amount;
            updateMoney();
        }
        else
        {
            Debug.Log("Inappropriate value = " + amount);
        }
    }

    public void addMoney(int amount)
    {
        if(amount > -1)
        {
            money = money + amount;
            dayRevenue = dayRevenue + amount;
            dayNetIncome = dayNetIncome + amount;
            updateMoney();
        }
        else
        {
            Debug.Log("Inappropriate value = " + amount);
        }
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
            int temp = (int)((patience * costOfFurnitur) / 200);
            addScore(temp);
        }
        Debug.Log("score = " + score);
    }
}
