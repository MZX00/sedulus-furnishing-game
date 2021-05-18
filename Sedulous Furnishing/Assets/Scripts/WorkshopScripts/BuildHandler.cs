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
            furniture.SetActive(false);
            StartCoroutine(LoadAsyncShop());
            
        }else{
            Debug.Log("Not stuck all");
        }
    }

    IEnumerator LoadAsyncShop()
    {

        DontDestroyOnLoad(furniture);

        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(furniture, SceneManager.GetSceneByName("Shop"));
        SceneManager.UnloadSceneAsync(currentScene);

    }
}
