using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyS, enemyM, enemyL, playerBullet, particle, bossBullet;

    GameObject[] enemySmallArr, enemyMidArr, enemyLargeArr, playerBulletArr, particleArr, bossBulletArr;

    public static ObjectManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static ObjectManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        enemySmallArr = new GameObject[30];
        enemyMidArr = new GameObject[30];
        enemyLargeArr = new GameObject[30];
        playerBulletArr = new GameObject[32];
        particleArr = new GameObject[50];
        bossBulletArr = new GameObject[150];

        InitObj();
    }

    private void InitObj()
    {
        for (int i = 0; i < enemySmallArr.Length; i++)
        {
            enemySmallArr[i] = Instantiate(enemyS);
            enemySmallArr[i].SetActive(false);
        }

        for (int i = 0; i < enemyMidArr.Length; i++)
        {
            enemyMidArr[i] = Instantiate(enemyM);
            enemyMidArr[i].SetActive(false);
        }

        for (int i = 0; i < enemyLargeArr.Length; i++)
        {
            enemyLargeArr[i] = Instantiate(enemyL);
            enemyLargeArr[i].SetActive(false);
        }

        for (int i = 0; i < playerBulletArr.Length; i++)
        {
            playerBulletArr[i] = Instantiate(playerBullet);
            playerBulletArr[i].SetActive(false);
        }

        for (int i = 0; i < particleArr.Length; i++)
        {
            particleArr[i] = Instantiate(particle);
            particleArr[i].SetActive(false);
        }

        for (int i = 0; i < bossBulletArr.Length; i++)
        {
            bossBulletArr[i] = Instantiate(bossBullet);
            bossBulletArr[i].SetActive(false);
        }
    }

    public GameObject SelectObj(string objectName)
    {
        GameObject[] gameObjArr;
        switch (objectName)
        {
            case "enemyS":
                gameObjArr = enemySmallArr;
                break;
            case "enemyM":
                gameObjArr = enemyMidArr;
                break;
            case "enemyL":
                gameObjArr = enemyLargeArr;
                break;
            case "playerBullet":
                gameObjArr = playerBulletArr;
                break;
            case "particle":
                gameObjArr = particleArr;
                break;
            case "bossBullet":
                gameObjArr = bossBulletArr;
                break;
            default:
                gameObjArr = null;
                break;
        }

        for (int i = 0; i < gameObjArr.Length; i++)
        {
            if (!gameObjArr[i].activeSelf)
            {
                gameObjArr[i].SetActive(true);
                return gameObjArr[i];
            }
        }
        return null;
    }
}
