using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnControlSec : MonoBehaviour
{
    private int _randNum;
    private int _randomEnemy;
    private float currentTime = 0;
    private float delayTime = 1.1f;
    [SerializeField] private GameObject[] _spawnPos;
    [SerializeField] private GameObject[] _enemys;
    public GameObject item;
    public SecondStageControl playerSpawnSec;

    private void Start()
    {
        Invoke("ItemStart", 5.5f);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (currentTime > delayTime)
        {
            SpawnEnemySecond();
        }   
    }

    public void SpawnEnemySecond()
    {
        _randNum = Random.Range(0, _spawnPos.Length);
        _randomEnemy = Random.Range(0, _enemys.Length);

        Enemy enemycs;
        GameObject whatEnemy;
        Rigidbody2D whatRigid;

        if (_randomEnemy == 0)
        {
            whatEnemy = ObjectManager.Instance.SelectObj("enemyM");
            whatEnemy.transform.position = _spawnPos[_randNum].transform.position;
            whatRigid = whatEnemy.GetComponent<Rigidbody2D>();

            enemycs = whatEnemy.GetComponent<MiddleEnemy>();
            enemycs.enemyHp = 3;
            enemycs.playerObj = playerSpawnSec.playerObj;
            enemycs.playercs = playerSpawnSec.playercs;
        }
        else
        {
            whatEnemy = ObjectManager.Instance.SelectObj("enemyL");
            whatEnemy.transform.position = _spawnPos[_randNum].transform.position;
            whatRigid = whatEnemy.GetComponent<Rigidbody2D>();

            enemycs = whatEnemy.GetComponent<Debris>();
            enemycs.enemyHp = 7;
            enemycs.playerObj = playerSpawnSec.playerObj;
            enemycs.playercs = playerSpawnSec.playercs;
        }

        switch (_randNum)
        {
            case 2:
                whatRigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
                break;
            case 3:
                whatRigid.AddForce(Vector2.left * 4, ForceMode2D.Impulse);
                break;
            default:
                whatRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
                break;
        }

        //if (_randNum == 2)
        //{
        //    whatRigid.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
        //}
        //else if (_randNum == 3)
        //{
        //    whatRigid.AddForce(Vector2.left * 4, ForceMode2D.Impulse);
        //}
        //else
        //{
        //    whatRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        //}

        currentTime = 0;
    }

    void ItemStart()
    {
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(2.2f);

        _randNum = Random.Range(0, _spawnPos.Length);

        Instantiate(item, _spawnPos[_randNum].transform.position, _spawnPos[_randNum].transform.rotation);

        Invoke("ItemStart", 1.5f);
    }
}
