using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        IdleBreaker,
        Walk,
        Run,
        Jump,
        Attack
    }
    
    public Animator _characterAnimator;
    private PlayerState _currentState;

    void Start()
    {
        SetState(PlayerState.Idle);
    }

    void Update()
    {
        PlayerState newState = DeterminateState();
        if (newState != _currentState)
            SetState(newState);
    }

    private void SetState(PlayerState newState)
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", false);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", false);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", false);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Run", false);
                break;
            case PlayerState.Jump:
                _characterAnimator.SetBool("Jump", false);
                break;
            case PlayerState.Attack:
                _characterAnimator.SetBool("Attack", false);
                break;
        }

        switch (newState)
        {
            case PlayerState.Idle:
                _characterAnimator.SetBool("Idle", true);
                break;
            case PlayerState.IdleBreaker:
                _characterAnimator.SetBool("IdleBreaker", true);
                break;
            case PlayerState.Walk:
                _characterAnimator.SetBool("Walk", true);
                break;
            case PlayerState.Run:
                _characterAnimator.SetBool("Run", true);
                break;
            case PlayerState.Jump:
                _characterAnimator.SetBool("Jump", true);
                break;
            case PlayerState.Attack:
                _characterAnimator.SetBool("Attack", true);
                break;
        }

        _currentState = newState;
    }

    private PlayerState DeterminateState()
    {
        if (Input.GetKeyDown(KeyCode.F)) return PlayerState.IdleBreaker;
        else if (Input.GetKeyDown(KeyCode.Space)) return PlayerState.Jump;
        else if (Input.GetKeyDown(KeyCode.Mouse0)) return PlayerState.Attack;
        else if (IsRunning())
        {
            return PlayerState.Run;
        }
        else if (IsWalking())
        {
            return PlayerState.Walk;
        }
        else
        {
            return PlayerState.Idle;
        }
    }
    private bool IsWalking()
    {
        return Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift);
    }
}
