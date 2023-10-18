using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Text level;
    int sceneIndex;

    private void Awake()
    {
        Screen.SetResolution(1080, 1920, false);
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            case 1:
                level.text = "LEVEL1";
                break;
            case 2:
                level.text = "LEVEL2";
                break;
            default:
                break;
        }
    }

    public void GameStart()
   {
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
   }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void GoToNextStage()
    {
        SceneManager.LoadScene(sceneIndex + 1);
        Time.timeScale = 1;
    }

    public void ShowRank()
    {
        SceneManager.LoadScene(3);
    }

    
}
