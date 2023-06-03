using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] public int hp;
    [SerializeField] public int maxhp;
    [SerializeField] public float moveSpeed = 3;
    public int level = 1;
    public int EXP = 0;
    public LevelManager lvlmanager;
    private bool isdead = false;
    public Animator animator;
    public float lastHorizontalVector;
    public float lastVertictalVector;
    [SerializeField] HpBar hpbar;

    Rigidbody2D rb;
    private Vector3 moveDirection;

    [Header("Audio")]
    [SerializeField] private AudioSource pickUpExp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3();
    }

    // Start is called before the first frame update
    void Start()
    {
        lvlmanager.updateExperienceBar(EXP, 10);
        lvlmanager.setLevelText(level);
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        if (moveDirection.x != 0)
        {
            lastHorizontalVector = moveDirection.x;
        }
        if (moveDirection.y != 0)
        {
            lastVertictalVector = moveDirection.y;
        }

        rb.velocity = moveDirection * moveSpeed;
        animator.SetFloat("Horizontal",moveDirection.x);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);


        if(EXP == 10){
            LevelUp();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EXP"))
        {
            pickUpExp.Play();
            other.gameObject.SetActive(false);
            updateExp(false);
        }
    }

    public void LevelUp()
    {

        level++;
        updateExp(true);
        lvlmanager.setLevelText(level);
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
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
        hpbar.SetState(hp, maxhp);
    }

    public void Heal(int healamount)
    {
        if(hp <= 0)
        {
            return;
        }
        hp += healamount;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
        hpbar.SetState(hp, maxhp);
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
