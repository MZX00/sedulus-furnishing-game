using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionScript : MonoBehaviour
{
    public GameObject WoodScreen;
    public GameObject MetalScreen;
    public GameObject[] Screens;

    // Start is called before the first frame update
    void Start()
    {
        ActivateScreen(0);
    }

    public void ActivateScreen(int index)
    {
        foreach (GameObject gameObject in Screens)
        {
            gameObject.SetActive(false);
        }
        Screens[index].SetActive(true);
    }

    public void WoodOptionClicked()
    {
        WoodScreen.SetActive(true);
        MetalScreen.SetActive(false);
    }

    public void MetalOptionClicked()
    {
        WoodScreen.SetActive(false);
        MetalScreen.SetActive(true);
    }
}
