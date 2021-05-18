using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class timerData
{
    public sbyte dayInNum;
    public sbyte hour;
    public sbyte min;

    public timerData()
    {
        dayInNum = 0;

    }
    public timerData(timer t)
    {
        dayInNum = t.TodayInNum;
    }
}
