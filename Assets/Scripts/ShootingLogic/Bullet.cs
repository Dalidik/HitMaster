using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _targetPosition;
    
    private ObjectPool _objectPool;
    
    private Single _speed = 10f;

    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
    }

    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        Vector3 direction = (_targetPosition - transform.position).normalized;
        GetComponent<Rigidbody>().velocity = direction * _speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(WaitToReturn());
    }

    IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(2);
        _objectPool.ReturnObject(gameObject);
    }
}
