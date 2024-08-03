using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private float timer;
    private float shootRate = 1f;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        bot.StopMovement();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > shootRate)
        {
            bot.Attack();
            bot.ChangeState(new PatrolState());
        }
        if (!bot.HasTargetInRange())
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
