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
    [SerializeField] private GameObject targetedIndicator;
    [SerializeField] private Material[] AllSkinColor;
    [SerializeField] private Renderer meshRenderer;
    private IState<Bot> currentState;
    private Vector3 destination;
    private enum SkinColor { Cyan = 0, Yellow = 1, Red = 2 }
    private SkinColor skinColor;
    private void Start()
    {
        ChangeState(new PatrolState());
        RandomizeWeapon();
        SetColor((SkinColor)Random.Range(0, Enum.GetNames(typeof(SkinColor)).Length));
    }
    private void SetColor(SkinColor skinColor)
    {
        this.skinColor = skinColor;
        meshRenderer.material = AllSkinColor[(int)skinColor];
    }
    protected override void Update()
    {
        base.Update();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    protected override void Die()
    {
        BotManager.Instance.RemoveBot(this);
    }
    private void RandomizeWeapon()
    {
        SetWeapon((WeaponType)Random.Range(0, Enum.GetNames(typeof(WeaponType)).Length));
    }
    public BotSight GetBotSight()
    {
        return botSight;
    }
    public void ShowTargetedIndicator()
    {
        targetedIndicator.SetActive(true);
    }
    public void HideTargetedIndicator()
    {
        targetedIndicator.SetActive(false);
    }
    public void SetDestination(Vector3 destination)
    {
        ChangeAnimation(Constants.ANIM_RUN);
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
        //SetBoolAnim(Constants.ANIM_IDLE, true);
        SetDestination(transform.position);
        ChangeAnimation(Constants.ANIM_IDLE);
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
    }

}

