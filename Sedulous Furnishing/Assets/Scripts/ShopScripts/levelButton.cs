using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelButton : MonoBehaviour
{
    [SerializeField]
    private GameObject textcloud;

    // Start is called before the first frame update
    void Start(){

        textcloud.SetActive(false);
    }
    // toggle text cloud when level buoon is clicked
    public void toggleTextCloud(){

        if (textcloud.activeSelf)
        {
            textcloud.SetActive(false);
        }
        else
        {
            textcloud.SetActive(true);
        }
    }
    
}
