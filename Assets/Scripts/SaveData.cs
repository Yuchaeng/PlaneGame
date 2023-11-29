using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //점수 저장 클래스

    public static SaveData instance = null;    

    public List<float> scores;   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }           
        else if (instance != this)
            Destroy(gameObject);
    }

    public List<float> LoadScoreData()
    {
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/scoredata.dat"))
        {
            file = new FileStream(Application.persistentDataPath + "/scoredata.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            scores = (List<float>)formatter.Deserialize(file);
            file.Close();           
        }
        else
        {
            scores = new List<float>();            
        }

        return scores;
    }

    public void SaveScoreData()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/scoredata.dat", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, scores);
        file.Close();
    }

    public void Rank(float totalScore)
    {
        LoadScoreData();

        //저장된 점수가 4개 미만이라면
        if (scores.Count < 4)
        {
            scores.Add(totalScore);
        }
        else
        {
            //이미 4개의 점수가 저장되었고 새로운 점수가 랭킹에 들어갈 수 있다면
            if (totalScore > scores.Min())
            {
                scores.Remove(scores.Min());
                scores.Add(totalScore);
            }
        }

        scores.Sort(new Comparison<float>((n1, n2) => n2.CompareTo(n1)));

        SaveScoreData();
    }




}
