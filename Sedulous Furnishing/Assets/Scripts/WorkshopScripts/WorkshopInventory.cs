using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopInventory : MonoBehaviour
{
    public GameObject InventoryCellPrefab;
    [SerializeField] int maxSlots;
    private List<GameObject> Inventory;

    void Awake(){
        Inventory = new List<GameObject>();
        adjustContentLength();
        
        for(int i = 0; i < maxSlots; i++){
            addInventoryCell(i);
        }
    }

    public void addInventoryCell(int i){
        GameObject cell = Instantiate(InventoryCellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Inventory.Add(cell);
        Inventory[Inventory.Count-1].transform.SetParent(transform,false);
    }

    public void adjustContentLength(){
        if(maxSlots <= 3){
            GetComponent<RectTransform>().sizeDelta = new Vector2(400,822);
        } else {
            int height = maxSlots * 260 + 20;
            GetComponent<RectTransform>().sizeDelta = new Vector2(400,height);
        }
    }

    public void loadItemtoCell(){
        
    }
}
