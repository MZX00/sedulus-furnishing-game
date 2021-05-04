using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class WoodMaterialCell : MonoBehaviour, IPointerClickHandler
{
    public GameObject selectHandler;
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
            string name = selected.GetComponent<Image>().name;
            string reg = "(" + materialName + ")";
            if(Regex.IsMatch(name,reg)){
                img = Resources.Load<Sprite>("Wood Furniture Parts/" + selected.name);
                selected.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
                selected.GetComponent<Image>().sprite = img;
                removeBorder();
            }else{
                addBorder();
                img = Resources.Load<Sprite>("Furniture Parts/Wood/" + materialName + "/" + selected.name);
                selected.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
                selected.GetComponent<Image>().sprite = img;
            }
            
        }
        
    }

    public void addBorder(){
        GameObject temp = Instantiate(borderPrefab,new Vector3(0,0,0),Quaternion.identity);
        temp.transform.SetParent(transform,false);
    }

    public void removeBorder(){
        if(transform.childCount > 0)
            Destroy(transform.GetChild(transform.childCount -1));
    }
}
