using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BossSecond : MonoBehaviour
{
    public GameObject playerObj;
    public Player playerCs;
    public GameObject bossBullet;
    public GameObject particle;

    public GameObject[] barrels;

    List<GameObject> bulletArray = new List<GameObject>();
    List<Rigidbody2D> bulletArrayRigids = new List<Rigidbody2D>();

    int patternSelect = -1;

    public float currentBossHp = 120;
    public float maxBossHp = 120;
    public bool isBossDead = false;

    void Start()
    {
        Invoke("BossPattern", 3);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-0.04f, 4.3f, 0), 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            collision.gameObject.SetActive(false);

            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            currentBossHp -= 2.1f;
            playerCs.score += 25;

            if(currentBossHp <= 0)
            {
                isBossDead = true;
                Destroy(gameObject);
            }
        }
       
    }

    void BossPattern()
    {
        patternSelect++;

        switch (patternSelect)
        {
            case 0:
                StartCoroutine(FireCross());
                break;
            case 1:
                StartCoroutine(FireCircle());
                break;
            case 2:
                StartCoroutine(FireSin());
                patternSelect = -1;
                break;
            default:
                break;
        }
    }

    IEnumerator FireCross()
    {
        //한번에 4개씩 3번 총알쏨
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                bulletArray.Add(ObjectManager.Instance.SelectObj("bossBullet"));
                bulletArray[i].transform.position = barrels[0].transform.position;
                bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                bulletArrayRigids[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(.7f);

            //보스 총알이 퍼지기 전 플레이어와 충돌해서 없어졌을 경우에 break(보통 4개 다 없어짐)
            if (bulletArray[0] == null || bulletArray[1] == null || bulletArray[2] == null || bulletArray[3] == null)
            {
                bulletArray.Clear();
                bulletArrayRigids.Clear();
                break;
            }

            for (int i = 0; i < 4; i++)
            {
                bulletArrayRigids[i].velocity = Vector2.zero;
            }

            bulletArrayRigids[0].AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse); 
            bulletArrayRigids[1].AddForce(new Vector2(0, -1) * 3, ForceMode2D.Impulse);
            bulletArrayRigids[2].AddForce(new Vector2(1, 0) * 3, ForceMode2D.Impulse); 
            bulletArrayRigids[3].AddForce(new Vector2(-1, 0) * 3, ForceMode2D.Impulse); 

            bulletArray.Clear();
            bulletArrayRigids.Clear();
        }
 
        Invoke("BossPattern", 1);
    }

    IEnumerator FireCircle()
    {
        Vector2 dir = playerObj.transform.position - transform.position;
        for (int i = 0; i < 30; i++)
        {
            bulletArray.Add(ObjectManager.Instance.SelectObj("bossBullet"));
            bulletArray[i].transform.position = barrels[0].transform.position;
            bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());

            bulletArrayRigids[i].AddForce(dir.normalized * 4, ForceMode2D.Impulse);
        }
        
        yield return new WaitForSeconds(.8f);

        for (int i = 0; i < 30; i++)
        {
            Vector2 bulletDir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 30), Mathf.Sin(Mathf.PI * 2 * i / 30));
            bulletArrayRigids[i].velocity = Vector2.zero;
            bulletArrayRigids[i].AddForce(bulletDir.normalized * 5, ForceMode2D.Impulse);
        }

        bulletArray.Clear();
        bulletArrayRigids.Clear();
        Invoke("BossPattern", 1);
    }

    IEnumerator FireSin()
    {
        for (int i = 0; i < 30; i++)
        {

            GameObject bulletInfo1 = ObjectManager.Instance.SelectObj("bossBullet");
            GameObject bulletInfo2 = ObjectManager.Instance.SelectObj("bossBullet");

            bulletInfo1.transform.position = barrels[1].transform.position;
            bulletInfo2.transform.position = barrels[2].transform.position;

            Rigidbody2D bulletRigid1 = bulletInfo1.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRigid2 = bulletInfo2.GetComponent<Rigidbody2D>();
            

            Vector2 bulletDir1 = new Vector2(Mathf.Sin(Mathf.PI * 3 * i/30), -1);
            Vector2 bulletDir2 = new Vector2(Mathf.Sin(Mathf.PI * 3 * i/30), -1);

            bulletRigid1.AddForce(bulletDir1.normalized * 5, ForceMode2D.Impulse);
            bulletRigid2.AddForce(bulletDir2.normalized * 5, ForceMode2D.Impulse);

            yield return new WaitForSeconds(.3f);

        }

        Invoke("BossPattern", 1);
    }

}
