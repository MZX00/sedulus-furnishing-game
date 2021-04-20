using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public  GameObject Furniture;
    private float patience;

    public void setPatience(float patience){
        this.patience = patience;
    }
    
    public bool decreasePatience(){
        if (this.patience > 0){
            this.patience -= Time.deltaTime;
            // Debug.Log(patience);
            return false;
        }else{
            return true;
        }

    }

    
    
}
