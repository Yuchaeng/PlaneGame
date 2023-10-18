using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : Enemy
{

    private void Start()
    {
        enemyHp = 3;
    }

    public override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * 80);
    }

    private void FixedUpdate()
    {
        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);            
            playercs.score += 100;
        }
    }


}
