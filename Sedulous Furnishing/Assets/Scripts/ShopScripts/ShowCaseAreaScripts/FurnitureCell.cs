using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurnitureCell : MonoBehaviour , IPointerClickHandler
{
    public GameObject Inventory;
    private int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.GetComponent<FurnitureShowcase>().setShowCaseActive(false);
        Inventory.GetComponent<FurnitureShowcase>().initiateSwap(index);
    }
    public void setIndex(int index){
        this.index = index;
    }
}
