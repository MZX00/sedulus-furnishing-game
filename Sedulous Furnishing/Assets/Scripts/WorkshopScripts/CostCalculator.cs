using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostCalculator : MonoBehaviour
{
    //Estimated Cost
    private int estCost;
    // private List<FurniutrePart> furnitureParts;
    IDictionary<string,int> partCost;
    IDictionary<string,int> materialCost;
    void Start()
    {
        // furnitureParts = new List<FurniutrePart>();
        initMaterialCost();
        initPartCost();
    }

    public void calculateCost(FurniutrePart part, bool remove){
        if(!remove){
            estCost += partCost[part.PartType] * materialCost[part.MaterialType];
        }else{
            estCost -= partCost[part.PartType] * materialCost[part.MaterialType];
        }
        GetComponent<TextMeshProUGUI>().text = "Estimated Cost: " + estCost; 
    }


    // public void addPart(FurniutrePart part){
    //     // furnitureParts.Add(part);
    //     calculateCost(part);
    // }
    // public void removePart(FurniutrePart part){
    //     // furnitureParts.Remove(part);
    //     calculateCost(part);
    // }
    void initPartCost(){
        partCost = new Dictionary<string,int>();
        partCost.Add("Arm Rest",2);
        partCost.Add("Chair Back",2);
        partCost.Add("Cushion",2);
        partCost.Add("Drawer",2);
        partCost.Add("Foam",2);
        partCost.Add("Hanging Rack",2);
        partCost.Add("Large Door",2);
        partCost.Add("Long Depth Panel",2);
        partCost.Add("Medium Depth Panel",2);
        partCost.Add("Small Depth Panel",2);
        partCost.Add("Long Surface Panel",2);
        partCost.Add("Medium Surface Panel",2);
        partCost.Add("Small Surface Panel",2);
        partCost.Add("Long Leg",2);
        partCost.Add("Medium Leg",2);
        partCost.Add("Small Leg",2);
        partCost.Add("Mirror",2);
        partCost.Add("Pillow",2);
        partCost.Add("Small Door",2);
        partCost.Add("Wheel Leg",2);
    }

    void initMaterialCost(){
        materialCost = new Dictionary<string,int>();
        materialCost.Add("Ash",1);
        materialCost.Add("Cherry",2);
        materialCost.Add("Walnut",3);
        materialCost.Add("White Oak",4);
        materialCost.Add("Mahogany",5);
        materialCost.Add("None",0);
    }
    
}
