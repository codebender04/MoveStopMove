using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Transform headTrasform;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Renderer pantsMeshRenderer;
    [SerializeField] private WeaponArraySO weaponArraySO;
    [SerializeField] private SkinArraySO skinArraySO;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] protected Transform weaponThrowPosition;
    [SerializeField] protected Animator animator;

    private string currentAnimName = Constants.ANIM_IDLE;
    private float growthMultiplier = 1.2f;
    private Pants pants;
    private GameObject currentHat;
    protected WeaponBase currentWeapon;
    protected int point;
    protected WeaponType weaponType;
    protected Character target;

    private float distance;
    private List<IRange> rangeList = new List<IRange>();
    private void CheckClosestTarget()
    {
        if (attackRange.GetNumberOfTarget() == 0)
        {
            target = null;
            return;
        }
        if (attackRange.GetNumberOfTarget() == 1)
        {
            target = attackRange.GetTargetInRangeList()[0];
            return;
        }
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < attackRange.GetNumberOfTarget(); i++)
        {
            if (attackRange.GetTargetInRangeList()[i] == null) continue;
            distance = Vector3.Distance(attackRange.GetTargetInRangeList()[i].transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = attackRange.GetTargetInRangeList()[i];
            }
        }
    }
    protected virtual void Grow()
    {
        transform.localScale *= growthMultiplier;
    }
    public void SetHat(Hat hat)
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
        currentHat = Instantiate(skinArraySO.GetHat(hat), headTrasform);
    }
    public void SetPants(Pants pants)
    {
        this.pants = pants;
        SkinManager.SetSkin(pantsMeshRenderer, skinArraySO.GetPants(pants));
    }
    public void SetWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponArraySO.GetWeapon(weaponType), handTransform);
        currentWeapon.Initialize(this);
    }

    public bool HasTargetInRange()
    {
        return attackRange.GetNumberOfTarget() > 0;
    }
    public AttackRange GetAttackRange()
    {
        return attackRange;
    }
    private void OnKill()
    {
        point++;
        textMeshProUGUI.text = point.ToString();
        if (point % 2 == 0)
        {
            Grow();
        }
    }
    //TODO: sua lai tach thanh attack anim -? 0.4s sau ra dan
    //ngat attack khong sinh ra dan
    //run luon
    public void Attack()
    {
        if (target != null)
        {
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            ChangeAnimation(Constants.ANIM_ATTACK);
            currentWeapon.OnFire(weaponThrowPosition);
        }
    }
    protected virtual void Update()
    {
        CheckClosestTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_WEAPON))
        {
            BulletBase weapon = other.GetComponent<BulletBase>();
            Character attacker = weapon.GetWeaponOwner();

            if (attacker != this)
            {
                if (attacker is Bot bot && attacker != null)
                {
                    bot.GetBotSight().RemoveTarget(this);
                }
                if (attacker != null)
                {
                    attacker.OnKill();
                }
                Die();
                Destroy(weapon.gameObject);
            }
        }
    }
    protected virtual void Die()
    {
    }
    protected virtual void OnDisable()
    {
        foreach (IRange range in rangeList)
        {
            range.RemoveTarget(this);
        }
    }
    public void AddRange(IRange range)
    {
        rangeList.Add(range);
    }
    public void RemoveRange(IRange range)
    {
        rangeList.Remove(range);
    }
    protected void ChangeAnimation(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
}
