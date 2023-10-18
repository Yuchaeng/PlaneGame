using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float currentTime = 0;
    float destroyTime = 4;

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > destroyTime)
        {
            gameObject.SetActive(false);
            currentTime = 0;
        }
    }


}
