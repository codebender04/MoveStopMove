using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private BotSight botSight;
    [SerializeField] private GameObject targetedIndicator;
    [SerializeField] private Material[] AllSkinColor;
    private IState<Bot> currentState;
    private Vector3 destination;
    private enum SkinColor { Cyan = 0, Yellow = 1, Red = 2, Blue = 3, BrightYellow = 4, Brown = 5, Pink = 6, }
    private SkinColor skinColor;
    private void Start()
    {
        ChangeState(new PatrolState());

        SetColor((SkinColor)Random.Range(0, Enum.GetNames(typeof(SkinColor)).Length));
        SetHat((Hat)Random.Range(0, Enum.GetNames(typeof(Hat)).Length));
        SetPants((Pants)Random.Range(0, Enum.GetNames(typeof(Pants)).Length));
        SetWeapon((WeaponType)Random.Range(0, Enum.GetNames(typeof(WeaponType)).Length));

        currentWeapon.SetSkin(Random.Range(0, 2));
    }

    private void SetColor(SkinColor skinColor)
    {
        this.skinColor = skinColor;
        characterRenderer.material = AllSkinColor[(int)skinColor];
    }
    
    protected override void Update()
    {
        base.Update();
        if (isDead)
        {
            return;
        }
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    protected override void Die()
    {
        base.Die();
        DarkenColor(0.4f);
        agent.SetDestination(transform.position);
        targetedIndicator.SetActive(false);
        ChangeAnimation(Constants.ANIM_DIE);
        Invoke(nameof(Despawn), 1.5f); 
    }
    private void Despawn()
    {
        BotManager.Instance.RemoveBot(this);
        Destroy(gameObject);
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
        if (botSight.GetTargetInSightList()[0] != null)
        {
            SetDestination(botSight.GetTargetInSightList()[0].transform.position);
        }
    }
    public bool HasTargetInSight()
    {
        return botSight.GetNumberOfTarget() > 0;
    }
    public void StopMovement()
    {
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

