using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test: MonoBehaviour
{
    //public GameObject testify;
    GameObject newShape;
    public void create(Vector2 pos){
        newShape = Instantiate(this.gameObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        newShape.name = "Customer";
    }

    public void deSpawn(){
        Destroy(newShape,3.2f);
    }

}
