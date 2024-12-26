using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [Header("=== Player Identifier ===")]
    public string ID;
    public string Name;


    [SerializeField] float UnlockStationTimer;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] JoystickLogic movment;
    public MoneyController moneyMoverForUI;
    bool canIntract;
    public float SwordDamage;

    [Header("=== Timer Logic ===")]

    [Range(0, 5f)][SerializeField] float collectElapsed;
    [Range(0, 5f)][SerializeField] float purchaseElapsed;
    [Range(0, 5f)][SerializeField] float refillElapsed;
    [Range(0, 5f)][SerializeField] float moneyElapsed;
    float refillTimer;
    float collectTimer;
    float purchaseTimer;
    float moneyTimer;


    [Header("== Upgrade Logic ==")]
    [SerializeField] List<float> speedLevelAmount;
    [SerializeField] List<float> collectSpeedLevelAmount;
    [SerializeField] List<int> capacityLevelAmount;
    int collectSpeedLevel=1;
    int speedLevel = 1;
    int capacityLevel = 1;
    [SerializeField] weaponController weaponController;
    [SerializeField] ActionButtonController actionButtonController;
    [SerializeField] GameObject TowerUI;
    [SerializeField] CameraFollower cameraFollower;
    [SerializeField] CharacterController characterController;
    public Health health;
    public MaterialCreator currMaterialCreator;
    [SerializeField] VariableJoystick variableJoystick;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        LoadData();
    }

   
    // Update is called once per frame
    void Update()
    {
        AnimationControll();// SaveData();
    }
    private void LoadData()
    {
        var x = PlayerPrefs.GetFloat(ID + Name + "PosX", transform.position.x);
        var y = PlayerPrefs.GetFloat(ID + Name + "PosY", transform.position.y);
        var z = PlayerPrefs.GetFloat(ID + Name + "PosZ", transform.position.z);
        transform.position = new Vector3(x, y, z);
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(ID + Name + "PosX", transform.position.x);
        PlayerPrefs.SetFloat(ID + Name + "PosY", transform.position.y);
        PlayerPrefs.SetFloat(ID + Name + "PosZ", transform.position.z);
    }

    private void AnimationControll()
    {
        canIntract = !animator.GetBool("run");
    }


    private void OnTriggerStay(Collider other)
    {
       

        if (other.gameObject.tag == "BarrackPayment" && canIntract)
        {
            var unlockedManager = other.GetComponent<UnlockBarrack>();
            var money = CalculateGivenMoney(GameSingleton.Instance.Inventory.woodCount, unlockedManager.TotalMoney, (unlockedManager.TotalMoney - unlockedManager.investedPrice));
            if (!unlockedManager.isUnlocked && GameSingleton.Instance.Inventory.woodCount >= money)
            {
                GameSingleton.Instance.Inventory.RemoveMaterial(MaterialType.Wood,-unlockedManager.Payment(money));
            }

        }
        if (other.gameObject.tag == "PatrolAreaUI" && canIntract)
        {
            var patrolAreaController = other.GetComponent<PatrolAreaUIOpenner>();
            patrolAreaController.TimerDecrease(1);

        }
       
        //if (other.gameObject.tag == "MoneyArea")
        //{
        //    var moneyAreaController = other.gameObject.transform.parent.GetComponent<MoneyAreaController>();
        //    if (moneyAreaController.moneys.Count > 0)
        //    {
        //        moneyTimer += Time.deltaTime;
        //        if (moneyTimer > moneyElapsed)
        //        {
        //            moneyTimer = 0;
        //            moneyMoverForUI.StartCoinMove(moneyAreaController.moneys.Last().transform.position, 100);
        //            moneyAreaController.removeMoney();
        //        }
        //    }
        //}
        //if (other.gameObject.tag == "Coin")
        //{
        //   moneyMoverForUI.StartCoinMove(other.transform.position, 1000);
        //   Destroy(other.gameObject);
        //}
        if (other.gameObject.tag == "CanTakeDamage" && !other.GetComponent<Health>().isDead) 
            weaponController.HitWithSword();
        if (other.gameObject.tag == "Tree") weaponController.HitWithAxe();
        if (other.gameObject.tag == "Mine") weaponController.HitWithPickaxe();
    }

    public void HitForMaterial()
    {
        if (currMaterialCreator != null) currMaterialCreator.createMaterial();
    }

    public void Teleport(Vector3 TeleportPos)
    {
        StartCoroutine(startTeleport(TeleportPos));
    }

    IEnumerator startTeleport(Vector3 TeleportPos)
    {
        var tempSmoothSpeed = cameraFollower.smoothSpeed;
        cameraFollower.smoothSpeed = 0.1f;
        movment.cameraAnimation = true;
        //characterController.enabled = false;
        cameraFollower.isAnimationStart = true;
        transform.position = TeleportPos;
        GameSingleton.Instance.Sounds.PlayOneShot(GameSingleton.Instance.Sounds.Teleport);
        while (!cameraFollower.isMoveFinished()){yield return null;}
        //characterController.enabled = true;
        cameraFollower.isAnimationStart = false;
        movment.cameraAnimation = false;
        cameraFollower.smoothSpeed = tempSmoothSpeed;
        yield return null;
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpgradeHelper") GameSingleton.Instance.UI.UpgradeHelper.SetActive(true);
        if (other.gameObject.tag == "UpgradePlayer") GameSingleton.Instance.UI.UpgradePlayer.SetActive(true);
        if (other.gameObject.tag == "Tower")
        {
            var tower = other.GetComponent<ActionButton>();
            var actionButton = new ActionButton();
            actionButton.Parent = tower.gameObject;
            actionButton.OpenUI = TowerUI;
            actionButton.ID = tower.ID;
            actionButton.actionButton = tower.actionButton;
            actionButtonController.addButton(actionButton);
            other.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameSingleton.Instance.UI.UpgradeHelper.SetActive(false);
        GameSingleton.Instance.UI.UpgradePlayer.SetActive(false);
        if (other.gameObject.tag == "Tower")
        {
            var tower = other.GetComponent<ActionButton>();
            actionButtonController.removeButton(tower.ID);
            other.GetComponent<Outline>().enabled = false;
        }
    }
    /*internal void setCollectSpeedLevel(int collectSpeedLevel)
    {
        this.collectSpeedLevel = collectSpeedLevel;
        refillElapsed = collectSpeedLevelAmount[--collectSpeedLevel];
        collectElapsed = collectSpeedLevelAmount[collectSpeedLevel];
    }

    internal void setSpeedLevel(int speedLevel)
    {
        this.speedLevel = speedLevel;
        movment.speed = speedLevelAmount[--speedLevel];
    }*/
    int CalculateGivenMoney(float Curency,float totalPrice, float needed)

    {
        float timer = purchaseTimer;
        if (purchaseTimer < 0.02f)
            timer = 0.02f;
        float givePerTime = UnlockStationTimer / (timer);
        int willGive = (int)(totalPrice / givePerTime);

        var money = Curency;
        if (willGive==0)
            return (int)money;
        else if (money >= willGive && needed >= willGive)
        {
            purchaseElapsed = 0;
            return willGive;
        }
        else if (money >= 1000 && needed >= 1000)
        {
            purchaseElapsed = 0;
            return 1000;
        }
        else if (money >= 100 && needed >= 100)
        {
            purchaseElapsed = 0;
            return 100;
        }
        else if (money >= 10 && needed >= 10)
        {
            purchaseElapsed = 0;
            return 10;
        }
        else if (money >= 1 && needed >= 1)
        {
            purchaseElapsed = 0;
            return 1;
        }
        return 0;
    }

    public void Respawn()
    {
        variableJoystick.OnPointerUp(new PointerEventData(EventSystem.current));
        variableJoystick.enabled = false;
        characterController.enabled = false;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1);
        sequence.Append(transform.DOMoveY(transform.position.y - 2, 0.5f));
        sequence.OnComplete(() =>
        {
            animator.SetBool("Dead", false);
        });
        Invoke("ReSpawnCharacter", 3f);
    }

    void ReSpawnCharacter()
    {
        health.ReSpawn();
        transform.position = GameSingleton.Instance.levelManager.currLevelNeeds.StartPos.position;
        health.isDead = false;
        health.isDying = false;
      
        variableJoystick.enabled = true;
        characterController.enabled = true;

    }

    public void FootSteps()
    {
        audioSource.PlayOneShot(GameSingleton.Instance.Sounds.FootStep,.3f);
    }
}

