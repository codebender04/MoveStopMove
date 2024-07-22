using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRange : AttackRange
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.TryGetComponent<Bot>(out Bot bot))
        {
            bot.ShowTargetedIndicator();
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.TryGetComponent<Bot>(out Bot bot))
        {
            bot.HideTargetedIndicator();
        }
    }
}
