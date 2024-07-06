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
    private float distance;
    protected Character target;
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
    protected virtual void Update()
    {
        CheckClosestTarget();
    }
}
