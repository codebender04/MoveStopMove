using System.Collections;
using System.Collections.Generic;
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
    protected Character target;

    private float distance;
    private List<AttackRange> attackRangeList = new List<AttackRange>();
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
    protected void SetWeapon(WeaponType weaponType)
    {
        currentWeapon = weaponArraySO.GetWeapon(weaponType);
    }
    public bool HasTargetInRange()
    {
        return attackRange.GetNumberOfTarget() > 0;
    }
    public AttackRange GetAttackRange()
    {
        return attackRange;
    }
    public void ThrowWeapon()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        animator.SetBool(Constants.ANIM_ATTACK, true);
        Instantiate(currentWeapon, weaponThrowPosition.position, weaponThrowPosition.rotation).Initialize(this);
        Invoke(nameof(ResetAttack), 0.8f);
    }
    private void ResetAttack()
    {
        animator.SetBool(Constants.ANIM_ATTACK, false);
    }
    public void SetBoolAnim(string animName, bool paramValue)
    {
        animator.SetBool(animName, paramValue);
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
                attacker.GetAttackRange().RemoveTarget(this);
                if (attacker is Bot bot)
                {
                    bot.GetBotSight().RemoveTarget(this);
                }
                Destroy(gameObject);
                Destroy(weapon.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        foreach (AttackRange attackRange in attackRangeList)
        {
            attackRange.RemoveTarget(this);
        }
    }
    public void AddAttackRange(AttackRange attackRange)
    {
        attackRangeList.Add(attackRange);
    }
    public void RemoveAttackRange(AttackRange attackRange)
    {
        attackRangeList.Remove(attackRange);
    }
}
