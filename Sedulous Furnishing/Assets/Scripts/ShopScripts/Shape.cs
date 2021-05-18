using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shape : MonoBehaviour
{
    //public GameObject shapePrefab;
    //private GameObject ShapeObj;
    // GameObject newShape;
    public Sprite material;
    private int materialcostPerUnit = 5;
    private int totalMaterialAmmount;

    





    // public GameObject create(Transform furniture){
    //     GameObject newShape = Instantiate(this.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
    //     newShape.name = "Shape";
    //     //GetComponent<Image>().sprite = Resources.Load<Sprite>(material.getImagePath());
    //     newShape.transform.SetParent(furniture,false);
    //     return newShape;
    // }

    // public void setDimensions(float x, float y){
    //     totalMaterialAmmount = (int) (materialAmmountPerUnit * x * y);
    //     GetComponent<RectTransform>().sizeDelta = new Vector2(x,y);
    // }

    // public void setPosition(float x,float y, float z){
    //     GetComponent<RectTransform>().anchoredPosition = new Vector3(x,y,z);
    // }

    public int getCost(){
        return totalMaterialAmmount * materialcostPerUnit;
    }

    // public void deSpawn(GameObject destroy){
    //     Destroy(destroy,3.2f);
    // }


    
    
    
}
