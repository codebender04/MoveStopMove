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
    private Color skinColor;

    public bool[] WeaponsPurchased = new bool[3] { true, false, false };
    public bool[] HatsPurchased = new bool[11] { true, false, false ,false, false, false, false, false, false, false, false };
    public bool[] PantsPurchased = new bool[9] { false, false, false, false, false, false, false, false, false };

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
        skinColor = characterRenderer.material.color;
        UpdateGold(0);
    }
    protected override void Update()
    {
        base.Update();
        if (isDead) return;
        HandleInput();
        HandleThrowingWeapon();
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
        isDead = false;
        rb.velocity = Vector3.zero;
        rb.position = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        point = 0;
        pointText.text = "0";
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
            rb.velocity = new Vector3(Joystick.Horizontal, 0f, Joystick.Vertical) * speed;
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
            rb.velocity = new Vector3 (0f, 0f, 0f);
        }
    }
    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
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
        base.Die();
        StopMovement();
        ChangeAnimation(Constants.ANIM_DIE);
        UpdateGold(point);
        Invoke(nameof(Despawn), 1.5f);
    }
    public void OnVictory()
    {
        StopMovement();
        ChangeAnimation(Constants.ANIM_DANCE);
    }
    private void Despawn()
    {
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
    public int GetPoint()
    {
        return point;
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
        saveData.currentHatIndex = (int)hat;
        saveData.currentPantsIndex = (int)pants;
        saveData.weaponsPurchased = (bool[])WeaponsPurchased.Clone();
        saveData.hatsPurchased = (bool[])HatsPurchased.Clone();
        saveData.pantsPurchased = (bool[])PantsPurchased.Clone();
    }
    public void LoadFromSaveData(SaveData saveData)
    {
        gold = saveData.gold;
        SetWeapon((WeaponType)saveData.currentWeaponIndex);
        SetWeaponSkin(saveData.currentSkinIndex);
        SetHat((Hat)saveData.currentHatIndex);
        SetPants((Pants)saveData.currentPantsIndex);
        WeaponsPurchased = (bool[])saveData.weaponsPurchased.Clone();
        HatsPurchased = (bool[])saveData.hatsPurchased.Clone();
        PantsPurchased = (bool[])saveData.pantsPurchased.Clone();
    }
}
