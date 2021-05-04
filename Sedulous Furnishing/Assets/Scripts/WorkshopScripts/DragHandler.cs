using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    //Left
    [SerializeField] private float xPadding;
    //Top
    [SerializeField] private float yPadding;

    void Start(){
        // Calculating screen bounds
        screenBounds = transform.root.GetComponent<RectTransform>().sizeDelta / 2;
        objectWidth = transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        objectHeight = transform.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    void LateUpdate(){
        // Prevent object from going out of bounds
        Vector3 viewPos = GetComponent<RectTransform>().anchoredPosition;
        //440
        viewPos.x = Mathf.Clamp( viewPos.x, screenBounds.x * -1 + xPadding + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp( viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight - yPadding);
        GetComponent<RectTransform>().anchoredPosition = viewPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Input.touchCount <= 1){
            Vector2 localpoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root.GetComponent<RectTransform>(),Input.mousePosition,GetComponentInParent<Canvas>().worldCamera,out localpoint);
            GetComponent<RectTransform>().anchoredPosition = localpoint;
        }
        
        
    }
}
