using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float hp;
    [SerializeField] public float maxhp;
    [SerializeField] public float moveSpeed = 3f;

    public int level = 1;
    public int EXP = 0;
    [SerializeField] HpBar hpbar;

    [Header("Audio")]
    public AudioSource pickUpExp;
    public AudioSource getHit;

    [Header("Backend")]
    public LevelManager lvlmanager;


    [SerializeField] UpgradePanelManager leveluppanelmanager;

    Rigidbody2D rb;
    private Vector3 moveDirection;
    private bool isdead = false;
    Animator animator;

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

        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        rb.velocity = moveDirection * moveSpeed;

        // sprite animation control
        if(moveDirection.x != 0)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
        }

        if((moveDirection.x == 0) && animator.GetBool("Movement"))
        {
            animator.SetBool("Movement", false);
        }
        if ((moveDirection.x != 0) && !animator.GetBool("Movement"))
        {
            animator.SetBool("Movement", true);
        }

        if (EXP >= lvlmanager.expSlider.maxValue){
            LevelUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EXP"))
        {
            //pickUpExp.Play();
            pickUpExp.PlayOneShot(pickUpExp.clip);
            other.gameObject.SetActive(false);
            updateExp(false);
        }
    }

    public void LevelUp()
    {
        level++;
        updateExp(true);
        lvlmanager.setLevelText(level);
        leveluppanelmanager.OpenPanel();
    }

    public void TakeDamage(int damageAmount)
    {
        //getHit.Play();
        getHit.PlayOneShot(getHit.clip);

        animator.SetTrigger("Hurt");

        if (isdead)
        {
            return;
        }
        hp -= damageAmount;
        if (hp <= 0)
        {
            GameObject.Find("Managers").GetComponent<GameOver>().GameOverPanel(true);
            isdead = true;
        }

        hpbar.SetState(hp, maxhp);
    }

    public void Heal(float healamount)
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
            Buffs buffs = GameObject.Find("Buffs").GetComponent<Buffs>();
            if (buffs.buffXPGain != 0)
            {
                EXP += 1 + buffs.buffXPGain;
            } else
            {
                EXP++;
            }
        }
        lvlmanager.updateExperienceBar(EXP, level*10);
    }
}
