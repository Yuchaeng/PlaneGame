using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceItem : MonoBehaviour
{
    Rigidbody2D myRigid;

    float currentTime = 0;
    float delayTime = 3;

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        Vector2 nextDir = new Vector2(randomX, randomY).normalized;
        myRigid.AddForce(nextDir * 500);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > delayTime)
        {
            Destroy(gameObject);
            currentTime= 0;
        }
    }

    
}
