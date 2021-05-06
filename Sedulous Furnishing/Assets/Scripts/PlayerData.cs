using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int score;

    public PlayerData(Player player)
    {
        money = player.getMoney();
        score = player.getScore();
    }
}
