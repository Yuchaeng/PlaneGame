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
        SetResolution();
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

    //블로그보고 가져온 코드
    public void SetResolution()
    {
        int setWidth = 1080; // 사용자 설정 너비
        int setHeight = 1920; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
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
