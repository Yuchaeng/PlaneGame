using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float particleCurrent = 0;
    public float particleFire = .4f;


    // Update is called once per frame
    void Update()
    {
        particleCurrent += Time.deltaTime;

        if (particleCurrent > particleFire)
        {
            gameObject.SetActive(false);
            particleCurrent = 0;
        }
    }
}
