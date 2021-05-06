using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WoodMaterialCell : MonoBehaviour, IPointerClickHandler
{
    public GameObject selectHandler;
    public CostCalculator costCalculator;
    [SerializeField] GameObject borderPrefab;
    // [SerializeField] GameObject materialHandler;
    public void OnPointerClick(PointerEventData eventData)
    {
        Sprite img;
        string materialName = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        List<GameObject> temp = selectHandler.GetComponent<SelectHandler>().getSelected(1);
        if(temp == null){
            selectHandler.GetComponent<SelectHandler>().setErrorMsg("Select furniture part (only 1) to apply material");
        }else{
            GameObject selected = temp[0];
            FurniutrePart part = selected.GetComponent<FurniutrePart>();
            if(part.MaterialType == "None"){
                addBorder();
                part.MaterialType = materialName;
                img = Resources.Load<Sprite>("Furniture Parts/Wood/" + materialName + "/" + part.PartType);
                selected.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
                selected.GetComponent<Image>().sprite = img;
                costCalculator.calculateCost(part,false);
            }else if(part.MaterialType == materialName){
                img = Resources.Load<Sprite>("Empty Furniture Parts/" + part.PartType);
                selected.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
                selected.GetComponent<Image>().sprite = img;
                part.MaterialType = "None";
                removeBorder();
                costCalculator.calculateCost(part,true);
            }else{
                costCalculator.calculateCost(part,true);
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    string childName =  transform.parent.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text;
                    if(childName == part.MaterialType){
                        transform.parent.GetChild(i).GetComponent<WoodMaterialCell>().removeBorder();
                        break;
                    }
                }
                addBorder();
                part.MaterialType = materialName;
                img = Resources.Load<Sprite>("Furniture Parts/Wood/" + materialName + "/" + part.PartType);
                selected.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
                selected.GetComponent<Image>().sprite = img;
                costCalculator.calculateCost(part,false);
            }
            
        }
        
    }

    public void addBorder(){
        GameObject temp = Instantiate(borderPrefab,new Vector3(0,0,0),Quaternion.identity);
        temp.transform.SetParent(transform,false);
    }

    public void removeBorder(){
        if(transform.childCount > 0)
            Destroy(transform.GetChild(transform.childCount -1).gameObject);
    }
}
