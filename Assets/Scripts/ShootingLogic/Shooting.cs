using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
   
    public ObjectPool BulletPool;
    
    public Transform ShootPoint;

    public Camera MainCamera;
    

    void Update()
    {
        
            if (Input.touchCount > 0 && Time.timeScale >= 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Shoot(touch.position);
                }
            }
            
    }

    void Shoot(Vector2 touchPosition)
    {
        Vector3 targetPosition = GetTouchWorldPosition(touchPosition);
        GameObject bullet = BulletPool.GetObject();
        bullet.transform.position = ShootPoint.position;
        bullet.GetComponent<Bullet>().SetTarget(targetPosition);
    }

    Vector3 GetTouchWorldPosition(Vector2 touchPosition)
    {
        Ray ray = MainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else
        {
            return ray.GetPoint(100); 
        }
    }
}
