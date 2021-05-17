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

    void Awake()
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
        Days.Add(6, "Saturday");
    }

    // Start is called before the first frame update
    void Start()
    {

        // code for loading data from binay file/

        timerData data = SaveManager.loadDate();
        Debug.Log("data = "+ data);

        if (data != null)
        {
            Debug.Log("Saved File exists");
            todayInNum = data.dayInNum;
        }
        else
        {
            Debug.Log("Timer Saved File do not exists");
            Debug.Log("Timer Saved File do not exists");
            Debug.Log("Timer Saved File do not exists");
            todayInNum = 0;
        }

        // todayInNum = 0;

        // For testing purposes I want to only change the date
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
            if(value > -1 && value < 12)
            {
                hour = value;
            }
            else
            {
                Debug.Log("Invalid value of hour, value = " + value);
            }
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
            if(value < 60 && value > -1)
            {
                minute = value;
            }
            else
            {
                Debug.Log("Invalid value of hour, value = " + value);
            }
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
            if(value < 6 && value > -1 ) 
            {
                todayInNum = value;
            }
            else
            {
                Debug.Log("Invalid value of hour, value = " + value);
            }
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
        else if (minute > 9)
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
        if (hour == 12)
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

        if (hour > 9)
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
        Debug.Log("todayInNum = " + todayInNum);
        
        if (todayInNum >= 5)
        {
            todayInNum = 0;
        }
        else
        {
            todayInNum++;
        }
        
        dayTextObj.text = Days[todayInNum];
        hour = 8;
        minute = 00;

        // save the data of time
        SaveManager.saveDate(this);
        SaveManager.saveGamehandler(gamehandler.GetComponent<GameHandler>());
        gamehandler.GetComponent<GameHandler>().ShowDaysSummary();
    }

}
