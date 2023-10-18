using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 inputVec;
    public GameObject bullet;
    Rigidbody2D bulletRigid;
    float speed = 5;

    float currentTime = 0;
    float delayTime = .2f;

    bool leftHit = false;
    bool rightHit = false;
    bool topHit = false;
    bool bottomHit = false;

    public float playerHp { get; set; }
    public float playerMaxHp { get; set; }

    public Text scoreNum;
    public float score;

    public GameObject particle;

    public Slider playerHpSlider;
    public GameObject playerHpObj;

    public bool playerIsDead = false;


    public virtual void Start()
    {
        playerHp = 100;
        playerMaxHp = 100;
        playerHpSlider.value = playerMaxHp;

        scoreNum.text = score.ToString();
    }

    public void Update()
    {
        currentTime += Time.deltaTime;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (leftHit && inputVec.x == -1)
            inputVec.x = 0;
        else if (rightHit && inputVec.x == 1)
            inputVec.x = 0;
        else if(topHit && inputVec.y == 1)
            inputVec.y = 0;
        else if(bottomHit && inputVec.y == -1)
            inputVec.y = 0;      

        Fire();

        scoreNum.text = score.ToString();

    }

    public virtual void FixedUpdate()
    {
        //update는 너무 빨리 불려져와서 update보다 안정적인 fixedupdate 사용
        inputVec = inputVec.normalized * Time.fixedDeltaTime * speed; //0.1초마다 불려져서 정규화만 하면 한번 클릭했을 때 열배로 움직이는 느낌
        transform.position = new Vector2(transform.position.x + inputVec.x, transform.position.y + inputVec.y);

        float clampX = Mathf.Clamp(transform.position.x, -4, 4);
        float clampY = Mathf.Clamp(transform.position.y, -7, 7);

        transform.position = new Vector2(clampX, clampY);

        if (playerHp <= 0.0f)
        {
            playerIsDead = true; 
        }
    }

    void Fire()
    {
        if (currentTime < delayTime)
            return;

        bullet = ObjectManager.Instance.SelectObj("playerBullet");
        bullet.transform.position = transform.position;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        currentTime = 0;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    leftHit= true;
                    break;
                case "RightBoundary":
                    rightHit = true; break;
                case "TopBoundary":
                    topHit= true; break;
                case "BottomBoundary":
                    bottomHit= true; break;
                default:
                    break;
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    leftHit = false;
                    break;
                case "RightBoundary":
                    rightHit = false;
                    break;
                case "TopBoundary":
                    topHit = false;
                    break;
                case "BottomBoundary":
                    bottomHit = false;
                    break;
                default:
                    break;
            }
        }
    }
}
