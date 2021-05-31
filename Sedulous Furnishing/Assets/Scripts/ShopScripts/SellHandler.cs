using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellHandler : MonoBehaviour
{
    [SerializeField] GameObject showcase;
    [SerializeField] GameObject player;
    public CslHandler csl;
    [SerializeField] GameObject inventory; 
    GameObject selectedFurniture = null;

    public GameObject selectFurniture(){
        int count = showcase.GetComponent<FurnitureShowcase>().getFurnitureCount();
        if(count != 0){
             int selectPos = (int)Random.Range(0, count - 1);
             Debug.Log("Selected Pos " + selectPos);
             selectedFurniture = showcase.GetComponent<FurnitureShowcase>().getFurniture(selectPos);
             if(selectedFurniture == null){
                 Debug.Log("Selected is null" );
             }
             return selectedFurniture;
        }else{
            return null;
        }
    }

    public bool checkSelected(){
        if(selectedFurniture == null){
            return true;
        }else{
            return false;
        }
    }

    public void sellFurniture(){
        int price = selectedFurniture.GetComponent<Furniture>().DisplayPrice;
        
        if (csl.CSL > 12)
        {
            player.GetComponent<Player>().addMoney(price + 100);
        }
        else if (csl.CSL <= 12 && csl.CSL > 4)
        {
            player.GetComponent<Player>().addMoney(price);
        }
        else if (csl.CSL <= 4)
        {
            if(price > 100){
                player.GetComponent<Player>().addMoney(price - 100);
            }else{
                player.GetComponent<Player>().addMoney(price );
            }
            
        }
        GetComponent<CustomerHandler>().setJourney();
        showcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell(selectedFurniture.GetComponent<Furniture>().FID);
        inventory.GetComponent<InventoryHandler>().saveInventory(selectedFurniture.GetComponent<Furniture>().FID);
        Destroy(selectedFurniture);
        selectedFurniture = null;
        csl.increaseCsl();
        SaveManager.SavePlayer(player.GetComponent<Player>(),csl);
        
    }
    
    public void returnFurniture(){
        if(selectedFurniture != null){
            showcase.GetComponent<FurnitureShowcase>().addFurnitureCell(selectedFurniture.GetComponent<Furniture>().FID);
            selectedFurniture = null;
        }
        
    }

}
