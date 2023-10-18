using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    float currentTimer = 0;
    float destoryTimer = 5;

    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer > destoryTimer)
        {
            gameObject.SetActive(false);
            currentTimer = 0;
        }
    }

}
