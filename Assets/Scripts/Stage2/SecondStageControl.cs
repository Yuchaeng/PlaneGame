using UnityEngine;
using UnityEngine.UI;

public class SecondStageControl : MonoBehaviour
{
    public GameObject playerObj, playerInfo;
    public PlayerSecond playercs;
    public GameObject playerSpawn;
    public GameObject playerHpObj;
    Slider playerHpSlider;
    public Text scoreNum;

    public GameObject camObj;
    MoveCamera camCS;

    public GameObject bossObj;
    public BossSecond bossSecondCs;
    public GameObject bossHpbarObj;
    public GameObject bossSpawn;
    Slider bossHpSlider;
    bool isBossInst = false;
    bool isBossSpawn = true;

    public GameObject gameWinObj;
    public GameObject gameOverObj;

    public Text totalScore;

    float scoreOfStage1;


    private void Start()
    {
        playerInfo = Instantiate(playerObj, playerSpawn.transform.position, playerSpawn.transform.rotation);

        playercs = playerInfo.GetComponent<PlayerSecond>();

        playerHpSlider = playerHpObj.GetComponent<Slider>();
        playercs.playerHpSlider = playerHpSlider;
        playercs.playerHpObj = playerHpObj;
        playercs.scoreNum = scoreNum;
        scoreOfStage1 = PlayerPrefs.GetFloat("scoreOfStage1");
        playercs.score = scoreOfStage1;

        camCS = camObj.GetComponent<MoveCamera>();
        camCS.target = playerInfo;

        bossHpSlider = bossHpbarObj.GetComponent<Slider>();

    }

    private void FixedUpdate()
    {
        if (playercs.score >= scoreOfStage1 + 1200 && isBossSpawn)
        {
            SpawnBossSecond();
        }

        if (isBossInst)
        {
            bossHpSlider.value = bossSecondCs.currentBossHp / bossSecondCs.maxBossHp;

            if (bossSecondCs.isBossDead)
            {
                bossHpbarObj.SetActive(false);
                gameWinObj.SetActive(true);
              
                totalScore.text = playercs.score.ToString();
                
                SaveData.instance.Rank(playercs.score);
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

    void SpawnBossSecond()
    {
        isBossSpawn = false;
        GameObject bossInfo = Instantiate(bossObj, bossSpawn.transform.position, bossSpawn.transform.rotation);
        bossInfo.transform.Rotate(Vector3.back * 180);

        bossSecondCs = bossInfo.GetComponent<BossSecond>();

        bossSecondCs.playerObj = playerInfo;
        bossSecondCs.playerCs = playercs;

        bossHpbarObj.SetActive(true);
        isBossInst = true;
    }

}
