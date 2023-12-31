using UnityEngine;

public class BackGround : MonoBehaviour
{
    Rigidbody2D backRigid;
    float backSpeed = 10.0f;

    void Start()
    {
        backRigid = GetComponent<Rigidbody2D>();  
    }

    private void FixedUpdate()
    {
        backRigid.MovePosition(backRigid.position + (Vector2.down * backSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "BackGroundBoundary")
        {
            transform.position = transform.position + new Vector3(0, 32, 0);
        }
    }
}
