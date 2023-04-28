using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float hp = 100;
    public float moveSpeed = 3;
    public int Level = 1;
    public float EXP = 0;
    Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        //transform.Translate(new Vector2(moveX, moveY)*moveSpeed*Time.deltaTime);
        if (hp <= 0)
        {

        }

    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EXP"))
        {
            other.gameObject.SetActive(false);
            EXP++;
        }
       

    }
}
