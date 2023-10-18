using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject playerObj;
    public Player playerCs;
    public GameObject bossBullet;
    public GameObject particle;

    public GameObject barrel;

    List<GameObject> bulletArray = new List<GameObject>();
    List<Rigidbody2D> bulletArrayRigids = new List<Rigidbody2D>();

    int patternSelect = -1;

    public float currentBossHp = 100;
    public float maxBossHp = 100;
    public bool isBossDead = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BossPattern", 3); //3초후에 BossPattern을 실행하겠다는 뜻, BossPattern에는 코루틴들어있음
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-0.04f, 4.8f, 0), 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            collision.gameObject.SetActive(false);

            particle = ObjectManager.Instance.SelectObj("particle");
            particle.transform.position = collision.transform.position;

            currentBossHp -= 3.5f;
            playerCs.score += 10;

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
                StartCoroutine(FireX());
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
                bulletArray[i].transform.position = barrel.transform.position;
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

    IEnumerator FireX()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                bulletArray.Add(ObjectManager.Instance.SelectObj("bossBullet"));
                bulletArray[i].transform.position = barrel.transform.position;
                bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                bulletArrayRigids[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);

            }
            yield return new WaitForSeconds(.8f);

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

            bulletArrayRigids[0].AddForce(new Vector2(1, 1) * 3, ForceMode2D.Impulse);
            bulletArrayRigids[1].AddForce(new Vector2(-1, 1) * 3, ForceMode2D.Impulse);
            bulletArrayRigids[2].AddForce(new Vector2(1, -1) * 3, ForceMode2D.Impulse);
            bulletArrayRigids[3].AddForce(new Vector2(-1, -1) * 3, ForceMode2D.Impulse);

            bulletArray.Clear();
            bulletArrayRigids.Clear();
        }

        Invoke("BossPattern", 1);
    }

}
