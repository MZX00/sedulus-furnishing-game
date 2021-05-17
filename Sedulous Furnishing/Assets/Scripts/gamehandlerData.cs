using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gamehandlerData
{
    public sbyte csl;


    public gamehandlerData()
    {
        csl = 7;
    }

    public gamehandlerData(GameHandler gh)
    {
        csl = gh.getCsl();
    }
}
