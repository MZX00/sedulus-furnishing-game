using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryHandler : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject furniturePrefab;
    
    public GameObject furnitureShowcase;
    private GameObject[] inventoryCells;
    private int maxIndex;
    private GameObject[] furnitures;
    public GameObject button;

    void Awake(){
        // SceneManager.MoveGameObjectToScene(variables, SceneManager.GetSceneByName(dailySummaryStr));
        // SceneManager.UnloadSceneAsync(currentScene);
        maxIndex = 0;
        inventoryCells = new GameObject[8];
        furnitures = new GameObject[8];
        for(int i = 0; i < 8; i++){
            addInventoryCell(i);
            addFurnituretoCell(i);
        }
        setShowCaseActive(true);
    }

    public void setShowCaseActive(bool val){
        furnitureShowcase.SetActive(val);
        this.gameObject.SetActive(!val);
        button.SetActive(!val);
    }

    public void addInventoryCell(int i){
        inventoryCells[i] = Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        inventoryCells[i].transform.SetParent(transform,false);
        inventoryCells[i].GetComponent<InventoryCell>().Inventory = this.gameObject;
        inventoryCells[i].GetComponent<InventoryCell>().setIndex(i);
    }

    public void addFurnituretoCell(int n){
        furnitures[n] = Instantiate(furniturePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        furnitures[n].GetComponent<Furniture>().initiateFurniture();
        furnitures[n].transform.SetParent(inventoryCells[n].transform,false);
        furnitures[n].GetComponent<Furniture>().setFID(n);
        furnitures[n].GetComponent<Furniture>().setPrice((int) Random.Range(200,1000));
    }

    public void swapFurniture(int n){
        if(furnitures[n]!= null){
            GameObject temp = removeFurniturefromCell(n);
            GameObject temp2 = furnitureShowcase.GetComponent<FurnitureShowcase>().addFurnituretoCell(temp);
            if(temp2 != null){
                furnitures[n] = temp2;
                furnitures[n].transform.SetParent(inventoryCells[n].transform,false);
            }
            setShowCaseActive(true);
        }else{
            GameObject temp = furnitureShowcase.GetComponent<FurnitureShowcase>().removeFurniturefromCell();
            if(temp!= null){
                furnitures[n] = temp;
                temp.transform.SetParent(inventoryCells[n].transform,false);
            }
        }
    }

    public GameObject removeFurniturefromCell(int n){
        GameObject temp = furnitures[n];
        furnitures[n] = null;
        return temp;
    }
    

}
