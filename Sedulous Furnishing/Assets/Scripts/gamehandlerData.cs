using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamehandlerData
{
    public sbyte csl;

    public gamehandlerData(GameHandler gh)
    {
        csl = gh.getCsl();
    }
}
