using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    
    public GameObject BulletPrefab;
    
    public Int32 InitialSize = 30;

    private List<GameObject> _pool = new List<GameObject>();

    void Start()
    {
        for (Int32 i = 0; i < InitialSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(BulletPrefab);
        newObject.SetActive(false);
        _pool.Add(newObject);
        return newObject;
    }

    public GameObject GetObject()
    {
        foreach (var obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return CreateNewObject();
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
