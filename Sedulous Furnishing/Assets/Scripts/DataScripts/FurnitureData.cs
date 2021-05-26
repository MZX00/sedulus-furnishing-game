using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
    public int length;
    public int displayPrice;
    public int profit;
    public float[] partPosX;
    public float[] partPosY;
    public string[] partName;
    public string[] partMaterial;
    // public string[] partMaterialCategory;
    public FurnitureData(FurniturePart[] furniture,int profit, int cost){

        //init
        partPosX = new float[furniture.Length];
        partPosY = new float[furniture.Length];
        partName = new string[furniture.Length];
        partMaterial = new string[furniture.Length];
        length = furniture.Length;
        this.profit = profit;
        displayPrice = cost;

        //assigning values
        for (int i = 0; i < furniture.Length; i++)
        {
            partPosX[i] = furniture[i].X;
            partPosY[i] = furniture[i].Y;

            partName[i] = furniture[i].PartType;
            partMaterial[i] = furniture[i].MaterialType;
        }
    }
}
