using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniInventoryHandler : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject selectHandler;
    [SerializeField] GameObject costCalculator;
    [SerializeField] GameObject furniturePartsList;
    [SerializeField] GameObject tabText;
    [SerializeField] GameObject woodMaterialList;
    void Start(){
        changeToFurnitureParts();
    }

    public void changeContents(){
        if(tabText.GetComponentInChildren<TextMeshProUGUI>().text == "Furniture Parts")
            changeToWoodMaterial();
        else if(tabText.GetComponentInChildren<TextMeshProUGUI>().text == "Wood"){
            changeToFurnitureParts();
        }
    }

    public void changeToFurnitureParts(){
        if(content.transform.childCount != 0 && content.transform.GetChild(0).name == "FurnitureParts"){
            return;
        }
        if(content.transform.childCount != 0){
            Destroy(content.transform.GetChild(0).gameObject);
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(415,5210);
        content.GetComponent<RectTransform>().anchoredPosition = new Vector2(-207.5f,0);

        tabText.GetComponentInChildren<TextMeshProUGUI>().text = "Furniture Parts";
        GameObject temp = Instantiate(furniturePartsList,new Vector3(0,0,0),Quaternion.identity);
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            temp.transform.GetChild(i).GetComponent<FurniturePartCell>().selectHandler = selectHandler;
        }
        temp.transform.SetParent(content.transform,false);
    }

    public void changeToWoodMaterial(){
        if(content.transform.childCount != 0 && content.transform.GetChild(0).name == "MaterialWood"){
            return;
        }
        if(content.transform.childCount != 0){
            Destroy(content.transform.GetChild(0).gameObject);
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(415,1315);
        content.GetComponent<RectTransform>().anchoredPosition = new Vector2(-207.5f,0);

        tabText.GetComponentInChildren<TextMeshProUGUI>().text = "Wood";
        GameObject temp = Instantiate(woodMaterialList,new Vector3(0,0,0),Quaternion.identity);

        string name = "e";
        //Checking if a selected object has material applied 
        List<GameObject> tempList = selectHandler.GetComponent<SelectHandler>().getSelected(1);
        if(tempList != null){
            name = tempList[0].GetComponent<FurniturePart>().MaterialType;
        }
        // Debug.Log("name is:" + name);
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            GameObject child = temp.transform.GetChild(i).gameObject;
            child.GetComponent<WoodMaterialCell>().selectHandler = selectHandler;
            child.GetComponent<WoodMaterialCell>().costCalculator = costCalculator.GetComponent<CostCalculator>();
            if(child.transform.GetComponentInChildren<TextMeshProUGUI>().text == name){
                child.GetComponent<WoodMaterialCell>().addBorder();
            }
            
        }
        temp.transform.SetParent(content.transform,false);
    }
}
