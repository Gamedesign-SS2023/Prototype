using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float hp = 100;
    public float moveSpeed = 3;
    public int level = 1;
    public int EXP = 0;
    public LevelManager lvlmanager;
    private bool isdead = false;
    public Animator animator;

    Rigidbody2D rb;
    private Vector2 moveDirection;

    [Header("Audio")]
    [SerializeField] private AudioSource pickUpExp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lvlmanager.updateExperienceBar(EXP, 10);
        lvlmanager.setLevelText(level);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        rb.velocity = moveDirection * moveSpeed;
        animator.SetFloat("Horizontal",moveDirection.x);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        if (hp <= 0)
        {

        }

        if(EXP == 10){
            LevelUp();
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
            pickUpExp.Play();
            other.gameObject.SetActive(false);
            updateExp(false);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(other.GetComponent<Enemy>().damage);
        }
    }

    public void LevelUp()
    {

        level++;
        updateExp(true);
        lvlmanager.setLevelText(level);
    }

    public void TakeDamage(float damageAmount)
    {;
        if (isdead)
        {
            return;
        }
        hp -= damageAmount;
        if (hp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isdead = true;
        }
    }

    public void updateExp(bool reset)
    {
        if (reset)
        {
            EXP = 0;
        } else {
            EXP++;
        }
        lvlmanager.updateExperienceBar(EXP, 10);
    }

}
