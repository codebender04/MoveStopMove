using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : Character, ISaveable
{
    public event EventHandler OnPlayerGrow;
    public event EventHandler<OnGoldChangedEventArgs> OnGoldChanged;
    public event EventHandler OnPlayerDeath;
    public class OnGoldChangedEventArgs : EventArgs { public int gold; }

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform tf;
    private enum PlayerState
    {
        Idling = 0,
        Moving = 1,
        Attacking = 2,
    }
    private PlayerState currentState = PlayerState.Idling;
    private int gold;
    private float shootRate = 1f;
    private float timer = 0f;
    private int currentWeaponSkinIndex;
    public bool[] WeaponsPurchased = new bool[3] { true, false, false };

    private FloatingJoystick joystick;
    private FloatingJoystick Joystick
    {
        get
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
            return joystick;
        }
    }
    private void Start()
    {
        UpdateGold(0);
        SetHat(Hat.None);
    }
    protected override void Update()
    {
        base.Update();
        HandleInput();
        HandleThrowingWeapon();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grow();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetWeapon(WeaponType.hammer);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetWeapon(WeaponType.knife);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetWeapon(WeaponType.boomerang);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UpdateGold(100);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateGold(-100);
        }
    }
    public void Initialize()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(true);

    }
    public void UpdateGold(int amount)
    {
        gold += amount;
        if (gold < 0) gold = 0;
        OnGoldChanged?.Invoke(this, new OnGoldChangedEventArgs { gold = gold });
    }
    private void HandleThrowingWeapon()
    {
        if (currentState == PlayerState.Idling && target != null)
        {
            timer += Time.deltaTime;
            if (timer > shootRate)
            {
                timer -= shootRate;
                StartCoroutine(nameof(PerformAttack));
            }
        }
    }
    private IEnumerator PerformAttack()
    {
        ChangeAnimation(Constants.ANIM_ATTACK);
        currentState = PlayerState.Attacking;
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        
        yield return new WaitForSeconds(0.3f);
        if (currentState == PlayerState.Attacking)
        {
            Attack();
        }
        currentState = PlayerState.Idling;
    }
    private void CancelAttack()
    {
        StopCoroutine(nameof(PerformAttack));
        currentState = PlayerState.Idling;
    }
    private void HandleInput()
    {
        if (Joystick == null) return;
        if (Mathf.Abs(Joystick.Horizontal) > 0.1f || Mathf.Abs(Joystick.Vertical) > 0.1f)
        {
            if (currentState == PlayerState.Attacking)
            {
                CancelAttack();
            }
            currentState = PlayerState.Moving;
            rb.velocity = new Vector3(Joystick.Horizontal, rb.velocity.y, Joystick.Vertical) * speed;
            tf.rotation = Quaternion.LookRotation(rb.velocity);
            ChangeAnimation(Constants.ANIM_RUN);
        }
        else
        {
            if (currentState != PlayerState.Attacking)
            {
                currentState = PlayerState.Idling;
                ChangeAnimation(Constants.ANIM_IDLE);
            }
            rb.velocity = new Vector3 (0f, rb.velocity.y, 0f);
        }
    }
    public void SetWeaponSkin(int index)
    {
        currentWeaponSkinIndex = index;
        currentWeapon.SetSkin(index);
    }
    public void SetWeaponColor(int index)
    {

    }
    protected override void Grow()
    {
        base.Grow();
        OnPlayerGrow?.Invoke(this, EventArgs.Empty);
    }
    protected override void Die()
    {
        gameObject.SetActive(false);
        UpdateGold(point);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
    public int GetGold()
    {
        return gold;
    }
    public WeaponType GetWeaponType()
    {
        return weaponType;
    }
    public int GetCurrentSkinIndex()
    {
        return currentWeaponSkinIndex;
    }
    public void PopulateSaveData(SaveData saveData)
    {
        saveData.gold = gold;
        saveData.currentSkinIndex = currentWeaponSkinIndex;
        saveData.currentWeaponIndex = (int)weaponType;
        saveData.weaponsPurchased = (bool[])WeaponsPurchased.Clone();
    }
    public void LoadFromSaveData(SaveData saveData)
    {
        gold = saveData.gold;
        SetWeapon((WeaponType)saveData.currentWeaponIndex);
        SetWeaponSkin(saveData.currentSkinIndex);
        WeaponsPurchased = (bool[])saveData.weaponsPurchased.Clone();
    }
}
