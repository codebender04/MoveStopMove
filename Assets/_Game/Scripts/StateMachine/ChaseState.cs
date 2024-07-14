using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
    }

    public void OnExecute(Bot bot)
    {
        if (bot.HasTargetInRange())
        {
            bot.ChangeState(new AttackState());
        }
        else if (bot.HasTargetInSight())
        {
            bot.GoToTarget();
        }
        else
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
