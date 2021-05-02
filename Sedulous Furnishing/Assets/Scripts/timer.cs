using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    [SerializeField]
    private Text minuteTextObj;
    [SerializeField]
    private Text hourTextObj;
    [SerializeField]
    private Text day;

    private sbyte minute;
    private sbyte hour;
    // Start is called before the first frame update
    void Start()
    {
        minute = 00;
        minuteTextObj.text = "00";
        hour = 8;
        hourTextObj.text = "0" + hour;
        StartCoroutine(changeSecond());
    }

   IEnumerator changeSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            incrementMinute();
        }
    }

    public void incrementMinute()
    {
        minute++;

        if (minute == 60)
        {
            minute = 0;
            minuteTextObj.text = "00";
            incrementHour();
        }
        else if(minute > 9)
        {
            minuteTextObj.text = "" + minute;
        }
        else
        {
            minuteTextObj.text = "0" + minute;
        }
        

    }
    public void incrementHour()
    {
        hour++;
        if(hour == 12)
        {
            hour = 00;
        }
        if (hour == 6)
        {
            Debug.Log("Day ended");
            // changed to next scene

        }
        
        if(hour > 9)
        {
            hourTextObj.text = "" + hour;
        }
        else
        {
            hourTextObj.text = "0" + hour;
        }

    }
}
