using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerData
{
    public sbyte dayInNum;

    public timerData(timer t)
    {
        dayInNum = t.TodayInNum;
    }


}
