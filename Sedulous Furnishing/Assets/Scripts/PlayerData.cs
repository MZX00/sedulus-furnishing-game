using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int score;

    public int dayNetIncome;
    public int dayExpenses;
    public int dayRevenue;

    public PlayerData()
    {
        money = 100;
        score = 0;

        dayNetIncome = 0;
        dayExpenses = 0;
        dayRevenue = 0;
    }


    public PlayerData(Player player)
    {
        money = player.getMoney();
        score = player.getScore();

        dayNetIncome = player.DayNetIncome;
        dayExpenses = player.DayExpenses;
        dayRevenue = player.DayRevenue;
    }
}
