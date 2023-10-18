using UnityEngine;
using UnityEngine.UI;

public class PlayerSecond : Player
{
    public ParticleSystem itemParticle;

    public override void Start()
    {
        base.Start();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.transform.tag == "EnemyM")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 6.7f;
            playerHpSlider.value = playerHp / playerMaxHp;
        }

        if (collision.transform.tag == "EnemyL")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 9.2f;
            playerHpSlider.value = playerHp / playerMaxHp;
        }

        if (collision.transform.tag == "BossBullet")
        {
            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);
            playerHp -= 8f;
            playerHpSlider.value = playerHp / playerMaxHp;
        }

        if (collision.transform.tag == "Item")
        {
            playerHp += 12f;
            Destroy(collision.gameObject);
            Instantiate(itemParticle, collision.transform.position, collision.transform.rotation);
        }
    }
}
