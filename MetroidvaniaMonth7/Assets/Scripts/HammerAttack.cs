﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour, IHammer, IAbility
{
    [HideInInspector] float attackRange = 0.8f;
    [HideInInspector] float swingSpeed = 0.3f;
    [SerializeField] LayerMask finalLayerMask;
    [SerializeField] PlayerStateSO playerState;
    ICharacterMovement charMove;
    private Ray2D ray;
    bool isAbilityInUse = false;
    Animator animator;

    public event Action OnAbilityStart = delegate { };
    public event Action OnAbilityEnd = delegate { };

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    public bool IsAbilityInUse
    {
        get => isAbilityInUse;
        set => isAbilityInUse = value;
    }

    public float SwingSpeed
    {
        get => swingSpeed;
        set => swingSpeed = value;
    }


    // Start is called before the first frame update
    public void Initialize()
    {
        charMove = GetComponent<ICharacterMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnAttack()
    {
        if (isAbilityInUse || playerState.IsPlayerReady())
        {
            OnAbilityStart();
            isAbilityInUse = true;
            animator.SetBool("IsSwinging", true);
            TryHit();
            //implement hammer attack sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Objects/Hammer-Swing");
        }
    }

// HammerAttackTick is Called from the HammerAbiltySO Update
    public void HammerAttackTick()
    {
        if (isAbilityInUse || playerState.IsPlayerReady())
        {
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //
            //     OnAbilityStart();
            //     isAbilityInUse = true;
            //     animator.SetBool("IsSwinging", true);
            //     TryHit();
            //     //implement hammer attack sound
            //     FMODUnity.RuntimeManager.PlayOneShot("event:/Objects/Hammer-Swing");
            // }
            //
        }
    }


    void TryHit()
    {
        ray = new Ray2D(transform.position, charMove.GetCurrentMoveDirection());
        Debug.DrawRay(ray.origin, ray.direction, Color.red, attackRange);
        var hit = Physics2D.RaycastAll(ray.origin, ray.direction, attackRange, finalLayerMask);
        foreach (RaycastHit2D obj in hit)
        {
            ProcessAttack(obj);
        }

        StartCoroutine(HitDelay());
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(swingSpeed);
        animator.SetBool("IsSwinging", false);
        OnAbilityEnd();
        isAbilityInUse = false;
    }


    void ProcessAttack(RaycastHit2D obj)
    {
        var hittable = obj.collider.transform.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.ProcessHit();
            hittable.ProcessHit(transform);
            //implement hammer attack hit sound
        }
    }
}