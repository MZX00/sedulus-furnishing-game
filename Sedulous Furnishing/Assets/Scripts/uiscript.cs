/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiscript : MonoBehaviour
{

    public GameObject bubble;
    public Button bubbleButton;

    private GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showBubble()
    {

        // instanciate bubble from its fabricatoin
        instance = Instantiate(bubble, new Vector3(-7.11f, 0.66f, 0), Quaternion.identity);
        // add parent to bubble
        instance.transform.parent = gameObject.transform;
        // adjust scale 
        instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        bubbleButton = instance.GetComponent<Button>();
        bubbleButton.onClick.AddListener(openCanvas);

    }

    public void openCanvas()
    {
        

    }



    public void deleteBubble()
    {
        Destroy(instance);
    }
}
*/