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

    //��α׺��� ������ �ڵ�
    public void SetResolution()
    {
        int setWidth = 1080; // ����� ���� �ʺ�
        int setHeight = 1920; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
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
