using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    [SerializeField]
    private GameObject gamehandler;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text minuteTextObj;
    [SerializeField]
    private Text hourTextObj;
    [SerializeField]
    private Text dayTextObj;


    IDictionary<int, string> Days = new Dictionary<int, string>();

    private sbyte minute;
    private sbyte todayInNum;
    private static sbyte hour;

    private void Awake()
    {
        initDays();
    }
    public void initDays()
    {
        Days.Add(0, "Monday");
        Days.Add(1, "Tuesday");
        Days.Add(2, "Wednesday");
        Days.Add(3, "Thursday");
        Days.Add(4, "Firday");
        Days.Add(5, "Saturday");
    }

    // Start is called before the first frame update
    void Start()
    {
        timerData data = SaveManager.loadDate();
        Debug.Log("data = "+ data);

        if (data != null)
        {
            Debug.Log("Saved File exists");
            todayInNum = data.dayInNum;
        }
        else
        {
            Debug.LogError("Saved File do not exists");
            Debug.Log("Saved File do not exists");
            Debug.LogError("Saved File do not exists");
            todayInNum = 4;

        }

        //todayInNum = 0;
        
        dayTextObj.text = Days[todayInNum];
        minute = 00;
        minuteTextObj.text = "00";
        hour = 4;
        hourTextObj.text = "0" + hour;
        StartCoroutine(changeSecond());
    }

    public sbyte Hour
    {
        get
        {
            return hour;
        }
        set
        {
            hour = value;
        }
    }

    public sbyte Minute
    {
        get
        {
            return minute;
        }
        set
        {
            minute = value;
        }
    }

    public sbyte TodayInNum
    {
        get
        {
            return todayInNum;
        }
        set
        {
            todayInNum = value;
        }
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
        if (hour == 5)
        {
            Debug.Log("Day ended");
            hour = 00;
            incrementDay();
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

    public void incrementDay()
    {
        Debug.Log("Day has been changed");
        todayInNum++;
        dayTextObj.text = Days[todayInNum];
        hour = 8;
        minute = 00;
        gamehandler.GetComponent<GameHandler>().ShowDaysSummary();
        SaveManager.saveDate(this);
    }
}
