using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurniutrePart : MonoBehaviour
{
    // private Vector2 screenBounds;
    // private float objectWidth;
    // private float objectHeight;
    // private float childWidth;
    // private float childHeight;

    // [SerializeField] private GameObject test;

    // private List<GameObject> stuckParts;

    // public List<GameObject> StuckPartsList{
    //     get{
    //         return stuckParts;
    //     }
    // }

    // public GameObject StuckPart{
    //     set{
    //         stuckParts.Add(value);
    //     }
    // }

    // void Start(){
    //     // Calculating screen bounds
    //     stuckParts = new List<GameObject>();
    //     screenBounds = transform.root.GetComponent<RectTransform>().sizeDelta / 2;
    //     objectWidth = transform.GetComponent<RectTransform>().sizeDelta.x / 2;
    //     objectHeight = transform.GetComponent<RectTransform>().sizeDelta.y / 2;
    //     // childWidth = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x/2;
    //     // childHeight = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y/2;
    // }

    // void LateUpdate(){
    //     // Prevent object from going out of bounds
    //     Vector3 viewPos = GetComponent<RectTransform>().anchoredPosition;
    //     viewPos.x = Mathf.Clamp( viewPos.x, screenBounds.x * -1 + 440 + objectWidth, screenBounds.x - objectWidth);
    //     viewPos.y = Mathf.Clamp( viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
    //     GetComponent<RectTransform>().anchoredPosition = viewPos;
    // }

    // void FixedUpdate(){
    //     // if(GetComponent<Collider2D>().IsTouching(test.GetComponent<Collider2D>()))
    //     //     Debug.Log("Touching");
    //     // else
    //     // {
    //     //     Debug.Log("Not Touching");
    //     // }
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     if(Input.touchCount <= 1 && stuckParts.Count == 0){
    //         Vector2 localpoint;
    //         RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root.GetComponent<RectTransform>(),Input.mousePosition,GetComponentInParent<Canvas>().worldCamera,out localpoint);
    //         GetComponent<RectTransform>().anchoredPosition = localpoint;
    //     }
        
        
    // }
    

}
