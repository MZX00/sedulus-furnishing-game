using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectHandler : MonoBehaviour
{
    private List<GameObject> selectedParts;
    [SerializeField] private GameObject miniInventoryHandler;
    [SerializeField] private GameObject stickButton;
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private float sec;

    void Start(){
        selectedParts = new List<GameObject>();
    }
    public void selectObject(GameObject part){
        int index = selectedParts.FindIndex(x => x == part);
        if(index == -1){
            if(Regex.IsMatch(part.name,"(StuckPart)") && selectedParts.Count == 0){
                // Debug.Log("I am ready for unstick");
                stickButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unstick";
            }else{
                stickButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stick";
            }
            addBorder(part);
            selectedParts.Add(part);
            // Debug.Log("Item Added");
        }
        else {
            stickButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stick";
            selectedParts.RemoveAt(index);
            removeBorder(part);
            // Debug.Log("Item Removed");
        }
        if(selectedParts.Count == 1 && !Regex.IsMatch(part.name,"(StuckPart)")){
            miniInventoryHandler.GetComponent<MiniInventoryHandler>().changeToWoodMaterial();
        }else{
            miniInventoryHandler.GetComponent<MiniInventoryHandler>().changeToFurnitureParts();
        }
    }

    public List<GameObject> getSelected(int ammount){
        // Debug.Log("Selected amount:" + selectedParts.Count);
        if(ammount == -1){
            return selectedParts;
        }else if(selectedParts.Count != ammount){
            return null;
        }else{
            List<GameObject> arr = new List<GameObject>();
            for (int i = 0; i < ammount; i++)
            {
                arr.Add(selectedParts[i]) ;
            }
            return arr;
        }
    }

    public void delete(){
        foreach (GameObject item in selectedParts)
        {
            Destroy(item);
        }
        selectedParts.Clear();
    }

    public void addBorder(GameObject part){
        GameObject temp = Instantiate(borderPrefab,new Vector3(0,0,0),Quaternion.identity);
        temp.name = "Border";
        temp.transform.SetParent(part.transform,false);
    }

    public void removeBorder(GameObject part){
        GameObject temp = part.transform.GetChild(part.transform.childCount -1).gameObject;
        Destroy(temp);
    }

    public void setErrorMsg(string msg){
        errorMessage.GetComponent<TextMeshProUGUI>().text = msg;
        errorMessage.SetActive(true);
        StartCoroutine(wait(msg));
    }

    IEnumerator wait(string msg)
     {
        yield return new WaitForSeconds(sec);
        if(errorMessage.GetComponent<TextMeshProUGUI>().text == msg){
            errorMessage.SetActive(false);
        }
     }
}
