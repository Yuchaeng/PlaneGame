using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Record : MonoBehaviour
{
    //ȭ�鿡 �����ִ� ���Ҹ� �ϴ� Ŭ����

    List<float> records = new List<float>(4);
    [SerializeField] List<TextMeshProUGUI> ranks = new List<TextMeshProUGUI>(4);

    void Start()
    {
       

        records = SaveData.instance.LoadScoreData();

        for (int i = 0; i < records.Count; i++)
        {
            ranks[i].text = records[i].ToString();
        }
    }


}
