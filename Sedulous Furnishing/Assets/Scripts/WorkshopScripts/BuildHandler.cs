using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildHandler : MonoBehaviour
{
    [SerializeField] GameObject furniture;
    public void build(){
        if(transform.root.childCount == 12){
            Debug.Log(transform.root.GetChild(11).name);
            Transform temp = transform.root.GetChild(11);
            temp.SetParent(furniture.transform) ;
            DontDestroyOnLoad(furniture);
            SceneManager.LoadScene("Shop");
        }else{
            Debug.Log("Not stuck all");
        }
    }
}
