using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debris : Enemy
{
    public ParticleSystem debrisParticle;

    private void Start()
    {
        enemyHp = 7;
    }   

    private void FixedUpdate()
    {
        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(debrisParticle, transform.position, transform.rotation);
            playercs.score += 270;
        }
    }
}
