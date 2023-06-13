using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int hp;
    [SerializeField] public int maxhp;
    [SerializeField] public float moveSpeed = 3;
    public int level = 1;
    public int EXP = 0;
    [SerializeField] HpBar hpbar;

    [Header("Audio")]
    [SerializeField] private AudioSource pickUpExp;

    [Header("Backend")]
    public LevelManager lvlmanager;
    public Animator animator;
    public float lastHorizontalVector;
    public float lastVertictalVector;
    [SerializeField] LevelUpPanelManager leveluppanelmanager;
    WeaponManager weaponmanager;

    [Header("Upgrades")]
    public List<UpgradeData> upgrades;
    public List<UpgradeData> selectedUpgrades;
    [SerializeField] public List<UpgradeData> aquiredUpgrades;

    Rigidbody2D rb;
    private Vector3 moveDirection;
    private bool isdead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3();
        weaponmanager= GetComponent<WeaponManager>();
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


        if(EXP >= lvlmanager.expSlider.maxValue){
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
        if (selectedUpgrades == null)
        {
            selectedUpgrades=new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));

        leveluppanelmanager.OpenPanel(selectedUpgrades);
        level++;
        updateExp(true);
        lvlmanager.setLevelText(level);
    }

    public void TakeDamage(int damageAmount)
    {
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

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if(aquiredUpgrades== null)
        {
            aquiredUpgrades = new List<UpgradeData>();
        }

        switch (upgradeData.UpgradeType)
        {   
            case UpgradeType.WeaponUnlock:
                weaponmanager.AddWeapon(upgradeData.WeaponData);
                break;
            case UpgradeType.WeaponUpgrade:
                weaponmanager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUnlock:
                break;
            case UpgradeType.ItemUpgrade:
                break;
        }
        aquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void AddUpgradesIntoListOfAvailableUpgrades(List<UpgradeData> upgradestoAdd)
    {
        upgrades.AddRange(upgradestoAdd);
    }
}
