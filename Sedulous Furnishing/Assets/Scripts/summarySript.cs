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

    public GameObject variables;
    private string shopStr;

    // Start is called before the first frame update
    private void Awake()
    {
        variables = GameObject.Find("variableObject");

        if (variables != null)
        {
            Object timer = variables.GetComponent<timer>();
            if (timer == null)
            {
                Debug.Log("timer not found");
            }
            else
            {
                Debug.Log("timer found");
            }

            Debug.Log("Player Game Object found");
            earningText.text = "" + variables.GetComponent<Player>().DayRevenue;
            expensesText.text = "" + variables.GetComponent<Player>().DayExpenses;
            netincomeText.text = "" + variables.GetComponent<Player>().DayNetIncome;
        }
        else
        {
            Debug.Log("unable to find variablezzz");
        }

        Button returnButton = GameObject.Find("returnButton").gameObject.GetComponent<Button>();
        if (returnButton == null)
        {
            Debug.Log("returnButton button not found");
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //returnButton.onClick.AddListener(delegate { returnToShop(); });
    }

    public void loadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void returnToShop()
    {
        StartCoroutine(LoadAsyncShop());
    }

    IEnumerator LoadAsyncShop()
    {

        DontDestroyOnLoad(variables);
        Scene currentScene = SceneManager.GetActiveScene();
        shopStr = "Shop";
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(shopStr, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(variables, SceneManager.GetSceneByName(shopStr));
        SceneManager.UnloadSceneAsync(currentScene);

    }

    public void goToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
