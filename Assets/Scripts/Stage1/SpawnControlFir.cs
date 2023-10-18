using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnControlFir : MonoBehaviour
{
    private int _randNum;
    private int _randomEnemy;
    private float currentTime = 0;
    private float delayTime = .8f;
    [SerializeField] private GameObject[] _spawnPos;
    [SerializeField] private GameObject[] _enemys;
    public FirstStageControl playerSpawnFir;

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (currentTime > delayTime)
        {
            SpawnEnemyFirst();
        }
    }

    public void SpawnEnemyFirst()
    {
        _randNum = Random.Range(0, _spawnPos.Length);
        _randomEnemy = Random.Range(0, _enemys.Length);

        Enemy enemycs;
        GameObject whatEnemy;
        Rigidbody2D whatRigid;

        if (_randomEnemy == 0)
        {
            whatEnemy = ObjectManager.Instance.SelectObj("enemyS");
            whatEnemy.transform.position = _spawnPos[_randNum].transform.position;
            whatRigid = whatEnemy.GetComponent<Rigidbody2D>();

            enemycs = whatEnemy.GetComponent<Asteroid>();

            enemycs.enemyHp = 3;
            enemycs.playerObj = playerSpawnFir.playerObj;
            enemycs.playercs = playerSpawnFir.playercs;
        }
        else
        {
            whatEnemy = ObjectManager.Instance.SelectObj("enemyL");
            whatEnemy.transform.position = _spawnPos[_randNum].transform.position;
            whatRigid = whatEnemy.GetComponent<Rigidbody2D>();

            enemycs = whatEnemy.GetComponent<Debris>();

            enemycs.enemyHp = 7;
            enemycs.playerObj = playerSpawnFir.playerObj;
            enemycs.playercs = playerSpawnFir.playercs;

        }

        whatRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        //Instantiate(_enemys[_randomEnemy], _spawnPos[_randNum].transform.position, _spawnPos[_randNum].transform.rotation);
        currentTime = 0;
    }
}
