using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class StickHandler : MonoBehaviour
{
    private List<GameObject> selectedParts;
    [SerializeField] private GameObject stuckPartPrefab;
    [SerializeField] GameObject selectHandler;
    [SerializeField] private GameObject stickButton;

    void Start(){
        selectedParts = new List<GameObject>();
    }

    public void stick(){
        selectedParts = selectHandler.GetComponent<SelectHandler>().getSelected(-1);
        SelectHandler select = selectHandler.GetComponent<SelectHandler>();
        if(selectedParts.Count == 2){
            GameObject temp = Instantiate(stuckPartPrefab,new Vector3(0,0,0),Quaternion.identity);
            temp.transform.SetParent(transform.root,false);
            temp.GetComponent<RectTransform>().sizeDelta = transform.root.GetComponent<RectTransform>().sizeDelta;
            changeParentPosition(temp.GetComponent<RectTransform>());
            foreach (GameObject item in selectedParts)
            {

                select.removeBorder(item);
                //checking if sticking part is furniture
                Button b;
                if(item.TryGetComponent<Button>(out b)){
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(delegate {select.selectObject(temp);});
                }else{
                    //operations if sticking part is stuckParts
                    Button[] buttons = item.GetComponentsInChildren<Button>();
                    foreach (Button child  in buttons)
                    {
                        child.onClick.RemoveAllListeners();
                        child.onClick.AddListener(delegate {select.selectObject(temp);});
                    }
                }
                item.transform.SetParent(temp.transform);
                item.GetComponent<DragHandler>().enabled = false;
            }
            // selectedParts[0].GetComponent<Button>().onClick.AddListener(delegate {selectObject(temp);});
            resizeParent(temp.GetComponent<RectTransform>());
            selectedParts.Clear();
        }else if(!(selectedParts.Count == 1 && Regex.IsMatch(selectedParts[0].name,"(StuckPart)"))){
            if(selectedParts.Count< 2 ){
                select.setErrorMsg("Please select two objects to stick") ;
            }else{
                select.setErrorMsg("Please select only two objects to stick");
            }
        }
    }
    
    //Remove Selected Parts container and make the parts independent
    public void unStick(){
        selectedParts = selectHandler.GetComponent<SelectHandler>().getSelected(-1);
        SelectHandler select = selectHandler.GetComponent<SelectHandler>();
        if(selectedParts.Count == 1 && Regex.IsMatch(selectedParts[0].name,"(StuckPart)")){
            GameObject temp = selectedParts[0];
            selectedParts.Clear();
            int length = temp.transform.childCount;
            Debug.Log("Childeren COunt: " + length);
            for (int i = 0; i < length; i++)
            {
                Transform child = temp.transform.GetChild(0);
                //removing border
                if(Regex.IsMatch(child.name,"(Border)")){
                    Destroy(child.gameObject);
                    continue;
                }
                //checking if sticking part is furniture
                Button b;
                if(child.TryGetComponent<Button>(out b)){
                    b.onClick.RemoveAllListeners();
                    b.onClick.AddListener(delegate {select.selectObject(child.gameObject);});
                }else{
                    //operations if sticking part is stuckPart
                    Button[] buttons = child.GetComponentsInChildren<Button>();
                    foreach (Button childButton  in buttons)
                    {
                        childButton.onClick.RemoveAllListeners();
                        childButton.onClick.AddListener(delegate {select.selectObject(child.gameObject);});
                    }
                }
                child.SetParent(transform.root);
                child.GetComponent<DragHandler>().enabled = true;
            }
            stickButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stick";
            Destroy(temp);
        }
    }

    public void changeParentPosition(RectTransform parent){
        float min_x, max_x, min_y, max_y;
        max_y = max_x = -99999;
        min_y = min_x = 99999;

        foreach (GameObject item in selectedParts)
        {
            RectTransform child = item.GetComponent<RectTransform>();
            float temp_min_x, temp_max_x, temp_min_y, temp_max_y;

            temp_min_x = child.anchoredPosition.x - (child.sizeDelta.x / 2);
            temp_max_x = child.anchoredPosition.x + (child.sizeDelta.x / 2);
            temp_min_y = child.anchoredPosition.y - (child.sizeDelta.y / 2);
            temp_max_y = child.anchoredPosition.y + (child.sizeDelta.y / 2);

            if (temp_min_x < min_x)
                min_x = temp_min_x;
            if (temp_max_x > max_x)
                max_x = temp_max_x;

            if (temp_min_y < min_y)
                min_y = temp_min_y;
            if (temp_max_y > max_y)
                max_y = temp_max_y;
        }
        float avg_x = (Mathf.Abs(max_x) + Mathf.Abs(min_x)) / 2;
        float avg_y = (Mathf.Abs(max_y) + Mathf.Abs(min_y)) / 2;
        

        //Relative to center if far apart
        

        //Setting avg_x pos to -ve 
        if(max_x <= 0 && min_x <= 0){
            avg_x = avg_x * -1;
        }else if(max_x > 0 && min_x <0 ){
            if(Mathf.Abs(max_x) < Mathf.Abs(min_x)){
                avg_x = max_x - avg_x;
            }else{
                avg_x = avg_x + min_x ;
            }
            
        }

        //Setting avg_y pos to -ve 
        if(max_y <= 0 && min_y <= 0){
            avg_y = avg_y * -1;
        }else if(max_y > 0 && min_y <0 ){
            if(Mathf.Abs(max_y) < Mathf.Abs(min_y)){
                avg_y = max_y - avg_y;
            }else{
                avg_y = avg_y + min_y ;
            }
        }

        Debug.Log("Position:" + avg_x + " " + avg_y);
        Debug.Log("X:" + max_x + " " + min_x);
        Debug.Log("Max:" + max_y + " " + min_y);
        
        parent.anchoredPosition = new Vector2(avg_x ,avg_y);
    }

    public void resizeParent(RectTransform parent) {
        RectTransform children = parent.GetComponentInChildren<RectTransform>();
        // Vector2 tempv = new Vector2();
        // tempv = parent.anchoredPosition;
        // parent.anchoredPosition = new Vector2(0,0);
        float min_x, max_x, min_y, max_y;
        max_y = max_x = -99999;
        min_y = min_x = 99999;

        foreach (RectTransform child in children) {
            Vector2 scale = child.sizeDelta;
            float temp_min_x, temp_max_x, temp_min_y, temp_max_y;

            temp_min_x = child.anchoredPosition.x - (scale.x / 2);
            temp_max_x = child.anchoredPosition.x + (scale.x / 2);
            temp_min_y = child.anchoredPosition.y - (scale.y / 2);
            temp_max_y = child.anchoredPosition.y + (scale.y / 2);

            Debug.Log("TEMP: " + temp_max_x + " " + temp_max_y + " " + temp_min_x + " " + temp_min_y);

            if (temp_min_x < min_x)
                min_x = temp_min_x;
            if (temp_max_x > max_x)
                max_x = temp_max_x;

            if (temp_min_y < min_y)
                min_y = temp_min_y;
            if (temp_max_y > max_y)
                max_y = temp_max_y;
        }
        Debug.Log("MAX AND MIN: " + max_x + " " + min_x + " " + max_y + " " + min_y);
        parent.sizeDelta = new Vector2(max_x - min_x, max_y - min_y);
        // parent.anchoredPosition = tempv;
    }
    
}
