using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    
    public Int32 Health = 3;
    
    private Animator _animator;

    private CharacterController _characterController;

    private enum EnemyState
    {
        Idle,
        Ragdoll
    }

    private Rigidbody[] _ragdollRigidbodies;
    
    private EnemyState _currentState = EnemyState.Idle;

    private void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        DisableRagdoll();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                IdleBehaviour();
                break;
            case EnemyState.Ragdoll:
                RagdollBehaviour(0);
                break;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            RagdollBehaviour(1);
        }
    }

    

    void Die()
    {
        
        Destroy(gameObject);
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.enabled = true;
        _characterController.enabled = true;
    }
    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        _animator.enabled = false;
        _characterController.enabled = false;
        StartCoroutine(DieCoroutine());
    }

    private void IdleBehaviour()
    {
        _currentState = EnemyState.Idle;
    }
    
    private void RagdollBehaviour(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            
            EnableRagdoll();
            
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
