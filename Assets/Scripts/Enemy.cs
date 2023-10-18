using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float currentTime = 0;
    public float delayTime = 4.5f;

    //public int enemyHp { get; set; }
    public int enemyHp;

    public GameObject playerObj;
    public Player playercs;
    public GameObject particle;

    public virtual void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > delayTime)
        {
            gameObject.SetActive(false);
            currentTime = 0;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            collision.gameObject.SetActive(false);

            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            enemyHp--;

            if (enemyHp <= 0)
            {
                //재사용할 때 타이머 초기화 안돼서 타이머 0으로 설정해줌
                currentTime = 0;
            }
        }
    }


}
