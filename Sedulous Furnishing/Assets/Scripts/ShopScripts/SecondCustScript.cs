using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCustScript : MonoBehaviour
{
    
    [SerializeField] GameObject Canvas;

    public void declined()
    {
        Canvas.SetActive(false);
    }

    public void accepted()
    {

    }
}
