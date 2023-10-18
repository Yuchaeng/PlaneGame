using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerFirst : Player
{

    public override void Start()
    {
        base.Start();
        score = 0;
        scoreNum.text = score.ToString();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.transform.tag == "EnemyS")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 5.0f;
            playerHpSlider.value = playerHp / playerMaxHp;   
        }

        if (collision.transform.tag == "EnemyL")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 7.5f;
            playerHpSlider.value = playerHp / playerMaxHp;

        }

        if (collision.transform.tag == "BossBullet")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 6.5f;
            playerHpSlider.value = playerHp / playerMaxHp;
        }
    }

 
}
