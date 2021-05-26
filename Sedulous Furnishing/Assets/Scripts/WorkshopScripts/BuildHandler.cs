using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildHandler : MonoBehaviour
{
    // [SerializeField] GameObject furniture;
        SelectHandler select;
    void Awake(){
        select = GetComponent<sceneManager>().selectHandler.GetComponent<SelectHandler>();
    }
    public void build(){
        if(transform.root.childCount == 12 && transform.root.GetChild(11).name == "StuckPart"){
            // Debug.Log(transform.root.GetChild(11).name);

            // saving furniture
            Transform temp = transform.root.GetChild(11);
            FurniturePart[] parts = temp.GetComponentsInChildren<FurniturePart>();
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
            foreach (FurniturePart part in parts)
            {
                part.gameObject.transform.SetParent(transform.root);
                part.X = part.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
                Debug.Log("X: " + part.X);
                part.Y = part.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
                Debug.Log("Y: " + part.Y);
            }

            FurnitureData fdata = new FurnitureData(parts,10,10);

            //updating inventory
            InventoryData inventory = SaveManager.loadInventory();
            FurnitureData[] newInventory;

            if(inventory != null){
                FurnitureData[] furnitures = inventory.furnitures;
                newInventory = new FurnitureData[furnitures.Length + 1];
                for (int i = 0; i < furnitures.Length; i++)
                {
                    newInventory[i] = furnitures[i];
                }
            }else{
                newInventory = new FurnitureData[1];
            }

            newInventory[newInventory.Length-1] = fdata;

            SaveManager.saveInventory(newInventory);
            
            GetComponent<sceneManager>().goToShop();

            // Debug.Log("Saved");

        }else{
            select.setErrorMsg("Please stick all parts before building");
        }
    }
}
