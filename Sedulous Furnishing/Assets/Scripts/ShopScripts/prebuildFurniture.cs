/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prebuildFurniture : MonoBehaviour
{
    // 8 furnitures pre built 

    [SerializeField]
    GameObject partPrefab;

    //Furniture[] prebuilt = new Furniture[30];
    //FurniturePart[] pre = new FurniturePart[30];
    //Start is called before the first frame update
    void Start()
    {
        GameObject furniture = transform.GetChild(2).gameObject;
        // n = your pre defined furniture part count
        sbyte n = 8;
        sbyte m = 5; // materials count
        sbyte selectfurn = (sbyte)Random.Range(1, n);

        if (selectfurn == 1)
        {
            GameObject part1 = Instantiate(partPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject part2 = Instantiate(partPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject part3 = Instantiate(partPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject part4 = Instantiate(partPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            part1.GetComponent<RectTransform>().anchoredPosition = new Vector2(109f, 105f);
            part1.GetComponent<FurniturePart>().name = "arm rest"; //make sure to match these with the name on resources
            // part1.transform.SetParent(furniture.transform);
            // same for part 2, 3, 4

        }
        else if (selectfurn == 2)
        {

        }// and so on

        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            int selectedmaterial = Random.Range(1, m);
            if (selectedmaterial == 1)
            {
                GameObject child = furniture.transform.GetChild(i).gameObject;
                Sprite img = Resources.Load<Sprite>("furniture parts/wood/" + child.name + "/" + "cherry");
            }
            else if (selectedmaterial == 2)
            {
                GameObject child = furniture.transform.GetChild(i).gameObject;
                Sprite img = Resources.Load<Sprite>("furniture parts/wood/" + child.name + "/" + "white oak");
            }// and so on
        }
        int selectedmater = Random.Range(1, m);
    }

    // n= your pre defined furniture part count


}
*/