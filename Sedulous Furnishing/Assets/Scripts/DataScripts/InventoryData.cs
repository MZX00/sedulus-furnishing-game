using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public FurnitureData[] furnitures;

    public InventoryData(int cellSize){
        furnitures = new FurnitureData[cellSize];
    }

    public InventoryData(FurnitureData[] furn){
        
        furnitures = new FurnitureData[furn.Length];

        for (int i = 0; i < furn.Length; i++)
        {
            furnitures[i] = furn[i];
        }
    }
}
