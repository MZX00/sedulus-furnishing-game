using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class summarySript : MonoBehaviour
{
    [SerializeField]
    private Text earningText;
    [SerializeField]
    private Text expensesText;
    [SerializeField]
    private Text netincomeText;
    [SerializeField]
    private Button returnButton;

    void Start()
    {
        PlayerData data = SaveManager.LoadPlayer();
        Debug.Log("data = "+ data);

        if (data != null)
        {
            Debug.Log("Player Saved File exists");

            earningText.GetComponent<Text>().text = data.dayRevenue.ToString();
            expensesText.GetComponent<Text>().text = data.dayExpenses.ToString();
            netincomeText.GetComponent<Text>().text = data.dayNetIncome.ToString();
        }
        else
        {
            Debug.Log("Player Saved File do not exists");
            Debug.Log("Player Saved File do not exists");
            Debug.Log("Player Saved File do not exists");
            earningText.GetComponent<Text>().text = "-999";
            expensesText.GetComponent<Text>().text = "-999";
            netincomeText.GetComponent<Text>().text = "-999";
        }

    }

    public void goToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
