using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class FurniturePartCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject furniture;
    public GameObject selectHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject temp = Instantiate(itemPrefab,new Vector3(0,0,0),Quaternion.identity);
        temp.transform.SetParent(transform.root,false);
        // furniture.GetComponent<Furniture>().addPart(temp);
        string itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        Sprite img = Resources.Load<Sprite>("Empty Furniture Parts/" + itemName);
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(img.texture.width,img.texture.height);
        temp.GetComponent<Image>().sprite = img;
        temp.GetComponent<Button>().onClick.AddListener(delegate { selectHandler.GetComponent<SelectHandler>().selectObject(temp); });
        temp.name = itemName;
        if(Regex.IsMatch(itemName,"(Surface Panel)"))
            temp.transform.SetAsFirstSibling();
        else
            temp.transform.SetAsLastSibling();
    }
}