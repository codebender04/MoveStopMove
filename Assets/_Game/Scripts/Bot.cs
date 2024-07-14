using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private BotSight botSight;
    private IState<Bot> currentState;
    private Vector3 destination;
    private void Start()
    {
        ChangeState(new PatrolState());
        RandomizeWeapon();
    }
    protected override void Update()
    {
        base.Update();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    private void RandomizeWeapon()
    {
        SetWeapon((WeaponType)Random.Range(0, Enum.GetNames(typeof(WeaponType)).Length));
    }
    public BotSight GetBotSight()
    {
        return botSight;
    }
    public void SetBoolAnim(string animName, bool paramValue)
    {
        animator.SetBool(animName, paramValue);
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }
    public bool IsAtDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
    public void GoToTarget()
    {
        SetDestination(botSight.GetTargetInSightList()[0].transform.position); //TODO: Randomize target to chase
    }
    public bool HasTargetInSight()
    {
        return botSight.GetNumberOfTarget() > 0;
    }
    public void StopMovement()
    {
        animator.SetBool(Constants.ANIM_IDLE, true);
        SetDestination(transform.position);
    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
        Debug.Log(state.ToString());
    }

}

