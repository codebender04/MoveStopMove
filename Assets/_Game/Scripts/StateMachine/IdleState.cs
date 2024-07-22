using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer;
    private float randomTime;
    public void OnEnter(Bot bot)
    {
        timer = 0f;
        randomTime = Random.Range(0.5f, 2f);
        bot.StopMovement();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            bot.ChangeState(new PatrolState());
        }
        if (bot.HasTargetInSight())
        {
            bot.ChangeState(new ChaseState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }

}
