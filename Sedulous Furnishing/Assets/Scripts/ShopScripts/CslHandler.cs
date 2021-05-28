using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CslHandler : MonoBehaviour
{
    [SerializeField] Sprite happy;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite sad;
    public sbyte CSL{
        set{
            csl = value;
        }
        get{
            return csl;
        }
    }
    private sbyte csl;

    public void increaseCsl()
    {
        if (csl < 15)
        {
            csl = (sbyte) (csl + 3);
        }
        changeCslIcon();
    }

    public void decreaseCsl()
    {
        if (csl > 0)
        {
            csl--;
        }

        changeCslIcon();
    }

    public void changeCslIcon()
    {
        if (csl > 12)
        {
            GetComponent<SpriteRenderer>().sprite = happy;
        }
        else if (csl <= 12 && csl > 4)
        {
            GetComponent<SpriteRenderer>().sprite = neutral;
        }
        else if (csl <= 4)
        {
            GetComponent<SpriteRenderer>().sprite = sad;
        }
    }




    
}
