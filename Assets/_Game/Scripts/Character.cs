using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Transform handTransform;
    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] protected Transform weaponThrowPosition;
    [SerializeField] protected Animator animator;
    [SerializeField] private WeaponArraySO weaponArraySO;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private string currentAnimName = Constants.ANIM_IDLE;
    private Transform currentHoldingWeapon;
    private float growthMultiplier = 1.2f;
    private int point;
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

    protected void SetWeapon(WeaponType weaponType)
    {
        if (currentHoldingWeapon != null)
        {
            Destroy(currentHoldingWeapon.gameObject);
        }
        currentWeapon = weaponArraySO.GetWeapon(weaponType);
        currentHoldingWeapon = Instantiate(currentWeapon.weaponVisual, handTransform);
    }

    //WeaponBase cWeapon;
    //protected void ChangeWeapon(WeaponType weaponType)
    //{
    //    if (cWeapon != null)
    //    {
    //        Destroy(cWeapon.gameObject);
    //    }
    //    cWeapon = Instantiate(weaponArraySO.GetWeapon(weaponType), handTransform); 
    //}

    public bool HasTargetInRange()
    {
        return attackRange.GetNumberOfTarget() > 0;
    }
    public AttackRange GetAttackRange()
    {
        return attackRange;
    }
    private void IncreasePoint()
    {
        point++;
        textMeshProUGUI.text = point.ToString();
        if (point % 2 == 0)
        {
            Grow();
        }
    }
    public void ThrowWeapon()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        ChangeAnimation(Constants.ANIM_ATTACK);
        Instantiate(currentWeapon, weaponThrowPosition.position, weaponThrowPosition.rotation).Initialize(this);
        //TODO: sua lai tach thanh attack anim -? 0.4s sau ra dan
        //ngat attack khong sinh ra dan
        //run luon
    }
    protected virtual void Update()
    {
        CheckClosestTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_WEAPON))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            Character attacker = weapon.GetWeaponOwner();

            if (attacker != this)
            {
                if (attacker is Bot bot)
                {
                    bot.GetBotSight().RemoveTarget(this);
                }
                attacker.IncreasePoint();
                Destroy(gameObject);
                Destroy(weapon.gameObject);
            }
        }
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
            animator.SetTrigger(animName);
        }
    }
}
