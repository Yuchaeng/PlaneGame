using UnityEngine;
using UnityEngine.UI;

public class FirstStageControl : MonoBehaviour
{
    public GameObject playerObj, playerInfo;
    public PlayerFirst playercs;
    //오브젝트 -> 스크립트 순서로 접근
    public GameObject playerSpawn;
    public GameObject playerHpObj;
    Slider playerHpSlider;
    public Text scoreNum;

    public GameObject camObj;
    MoveCamera camCS;

    public GameObject bossObj;
    public Boss bossCs;

    public GameObject bossHpbarObj;
    public GameObject bossSpawn;
    Slider bossHpSlider;
    public bool isBossInst = false;

    public GameObject gameWinObj;
    public GameObject gameOverObj;

    bool isBossSpawn = true;


    private void Start()
    {
        playerInfo = Instantiate(playerObj, playerSpawn.transform.position, playerSpawn.transform.rotation);

        playercs = playerInfo.GetComponent<PlayerFirst>();

        playerHpSlider = playerHpObj.GetComponent<Slider>();
        playercs.playerHpSlider = playerHpSlider;
        playercs.playerHpObj = playerHpObj;
        playercs.scoreNum = scoreNum;

        camCS = camObj.GetComponent<MoveCamera>();
        camCS.target = playerInfo;

        bossHpSlider = bossHpbarObj.GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (playercs.score >= 500 && isBossSpawn)
        {
            SpawnBossFirst();
        }

        if (isBossInst)
        {
            bossHpSlider.value = bossCs.currentBossHp / bossCs.maxBossHp;

            if (bossCs.isBossDead)
            {
                PlayerPrefs.SetFloat("scoreOfStage1", playercs.score);
                bossHpbarObj.SetActive(false);
                gameWinObj.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (playercs.playerIsDead)
        {
            Destroy(playerInfo);
            playerHpObj.SetActive(false);
            gameOverObj.SetActive(true);

            Time.timeScale = 0;
        }
    }

    void SpawnBossFirst()
    {
        isBossSpawn = false;
        GameObject bossInfo = Instantiate(bossObj, bossSpawn.transform.position, bossSpawn.transform.rotation);
        bossInfo.transform.Rotate(Vector3.back * 180);

        bossCs = bossInfo.GetComponent<Boss>();

        bossCs.playerObj = playerInfo;
        bossCs.playerCs = playercs;

        bossHpbarObj.SetActive(true);
        isBossInst = true;
    }
}
