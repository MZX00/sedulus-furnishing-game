using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class resumeGameScript : MonoBehaviour
{

    [SerializeField]
    private Button resumeGameButton;


    private void Awake()
    {
        resumeGameButton.interactable = true;
        string path = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(path))
        {
            resumeGameButton.interactable = true;
        }
        else
        {
            resumeGameButton.interactable = false;
        }
    }

    public void resumeGame()
    {
        SceneManager.LoadScene("Shop");
    }
}
