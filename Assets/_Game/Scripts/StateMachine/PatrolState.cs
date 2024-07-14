using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.SetBoolAnim(Constants.ANIM_IDLE, false);
        Patrol(bot);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsAtDestination())
        {
            bot.ChangeState(new IdleState());
        }
        if (bot.HasTargetInSight())
        {
            bot.ChangeState(new ChaseState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
    private void Patrol(Bot bot)
    {
        Vector3 point;
        if (RandomPoint(bot, out point)) //pass in our centre point and radius of area
        {
            bot.SetDestination(point);
        }
    }
    private bool RandomPoint(Bot bot, out Vector3 result)
    {

        Vector3 randomPoint = Random.insideUnitSphere * 8f; //random point in a sphere 
        randomPoint += bot.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 4f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
