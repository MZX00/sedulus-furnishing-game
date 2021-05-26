using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour ,IPointerClickHandler 
{
    public GameObject Inventory;
    private int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.GetComponent<InventoryHandler>().swapFurniture(index);
    }

    public void setIndex(int index){
        this.index = index;
    }
}
