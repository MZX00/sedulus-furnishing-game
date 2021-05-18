using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private int perUnitCost = 1;
    private string imagePath = "Assets/Texture & Sprites/wood.jpg";

    public int getPerUnitCost(){
        return perUnitCost;
    }

    public string getImagePath(){
        return imagePath;
    }
    public void setPerUnitCost(int cost){
        perUnitCost = cost;
    }

    public void setImagePath(string imagePath){
        this.imagePath = imagePath;
    }
}
