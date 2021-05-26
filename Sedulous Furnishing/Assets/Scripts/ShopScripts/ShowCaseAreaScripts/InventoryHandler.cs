using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject furnitureShowcase;
    [SerializeField] GameObject furniturePrefab;
    [SerializeField] GameObject furniturePartPrefab;
    [SerializeField] private int defaultCellCount;
    private GameObject[] inventoryCells;
    // private int maxIndex;
    private GameObject[] furnitures;
    

    void Awake(){
        // maxIndex = 0;

        InventoryData inventory = SaveManager.loadInventory();
        FurnitureData[] fdata;
        if(inventory != null){
            fdata = inventory.furnitures;
            if(fdata.Length > defaultCellCount){
                inventoryCells = new GameObject[fdata.Length];
                setContentSize(inventoryCells.Length);
            }else{
                inventoryCells = new GameObject[defaultCellCount];
                setContentSize(defaultCellCount);
            }
            furnitures = new GameObject[fdata.Length];
            
        }else{
            fdata = null;
            inventoryCells = new GameObject[defaultCellCount];
            setContentSize(defaultCellCount);
        }
        
        for(int i = 0; i < inventoryCells.Length; i++){
            addInventoryCell(i);
            if(fdata!= null){
                if(i < fdata.Length){
                    initFurniture(fdata[i],i);
                    addFurnituretoCell(i);
                }
            }
            
        }
        
        setShowCaseActive(true);
    }

    public void initFurniture(FurnitureData furn, int index){

        furnitures[index] = Instantiate(furniturePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        furnitures[index].GetComponent<RectTransform>().sizeDelta = new Vector2(325,325);
        
        furnitures[index].GetComponent<Furniture>().FID = index;
        furnitures[index].GetComponent<Furniture>().DisplayPrice = furn.displayPrice;
        furnitures[index].GetComponent<Furniture>().Profit = furn.profit;

        for (int i = 0; i < furn.length; i++)
        {
            Debug.Log("MaterialName: " + furn.partMaterial[i] + " Part Name " + furn.partName[i]);
            Sprite img = Resources.Load<Sprite>("Furniture Parts/Wood/" + furn.partMaterial[i] + "/" + furn.partName[i]);
            GameObject part = Instantiate(furniturePartPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            part.transform.SetParent(furnitures[index].transform,false);
            part.GetComponent<Image>().sprite = img;

            //Adjusting size
            float width, height, screenX, screenY;

            screenX = transform.root.GetComponent<RectTransform>().sizeDelta.x;
            screenY = transform.root.GetComponent<RectTransform>().sizeDelta.y;

            // if(img.texture.width > img.texture.height){
            //     width = img.texture.width*325/screenX;
            //     height = img.texture.height*width/img.texture.width;
            // }else{
            //     height = img.texture.height*325/screenY;
            //     width = img.texture.width*height/img.texture.height;
            // }
            // width = img.texture.width;
            // height = img.texture.height;
            width = img.texture.width*325/screenX;
            height = img.texture.height*325/screenY;
            part.GetComponent<RectTransform>().sizeDelta = new Vector2(width,height);

            // *325/screenX   *325/screenY
            part.GetComponent<RectTransform>().anchoredPosition = new Vector2(furn.partPosX[i]*325/screenX,furn.partPosY[i]*325/screenY);
        }
        
    }

    public void setShowCaseActive(bool val){
        furnitureShowcase.SetActive(val);
        transform.root.GetChild(1).gameObject.SetActive(!val);
        // this.gameObject.SetActive(!val);
        // button.SetActive(!val);
    }

    public void addInventoryCell(int i){
        inventoryCells[i] = Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        inventoryCells[i].transform.SetParent(transform,false);
        inventoryCells[i].GetComponent<InventoryCell>().Inventory = this.gameObject;
        inventoryCells[i].GetComponent<InventoryCell>().setIndex(i);
    }

    public void addFurnituretoCell(int n){
        furnitures[n].transform.SetParent(inventoryCells[n].transform,false);
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

    public void setContentSize(int n){
        if(n == defaultCellCount){
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0,800);
        }else{
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0,n/4 * 400);
        }
    }
    

}
