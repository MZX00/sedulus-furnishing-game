using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionScript : MonoBehaviour
{
    public GameObject WoodScreen;
    public GameObject MetalScreen;

    // Start is called before the first frame update
    void Start()
    {
        WoodScreen.SetActive(true);
        MetalScreen.SetActive(false);
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
