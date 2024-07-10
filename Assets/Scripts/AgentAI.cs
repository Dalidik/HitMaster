using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AgentAI : MonoBehaviour
{
	
	public Int32 CurrentWayPointIndex = 0;
	
	public List<Transform> WayPoints;
	
	private NavMeshAgent _navMeshAgent;

	private SliderManager _sm;

	 Animator _animator;

	private void Awake()
	{
		_sm = GameObject.FindGameObjectWithTag("Slider").GetComponent<SliderManager>();
	}

	void Start()
    {
	    _animator = gameObject.GetComponent<Animator>();
	    _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
	    _sm.Slider.value = Mathf.Round(_sm.ScoreCount);
	    Walking();

	    if (_navMeshAgent.isOnOffMeshLink)
	    {
		    _navMeshAgent.speed = 1;
	    }
	    else
	    {
		    _navMeshAgent.speed = 3;
	    }
    }
    
    void Walking()
    {
	  
	    if (WayPoints.Count == 0)
	    {
		    return;
	    }

	    Single distanceToWayPoint = Vector3.Distance(WayPoints[CurrentWayPointIndex].position, transform.position);
	    
	    if (distanceToWayPoint <= 0.1 )
	    {
		    if (GameObject.FindGameObjectsWithTag("Enemies1").Length.Equals(0))
		    {
			    
			    CurrentWayPointIndex = 1;
			    _sm.ScoreCount = 30;

		    } 
		    if (CurrentWayPointIndex == 1 && GameObject.FindGameObjectsWithTag("Enemies2").Length.Equals(0))
		    {
			    CurrentWayPointIndex = 2;
			   
		    }
		    if (CurrentWayPointIndex == 2 )
		    {
			    CurrentWayPointIndex = 3;
			    _sm.ScoreCount = 60;
		    }
		    
	    }
	  

	    _navMeshAgent.SetDestination(WayPoints[CurrentWayPointIndex].position);
    }
    private void OnTriggerEnter(Collider other)
    {
	    
	    _animator.SetTrigger("Idle");
	    if (other.gameObject.CompareTag("Finish"))
	    {
		    _sm.ScoreCount = 100;
		    StartCoroutine(ReloadScene());
	    }
    }
    private void OnTriggerExit(Collider other)
    {
	    _animator.SetTrigger("Run");
    }
    
    IEnumerator ReloadScene()
    {
	    yield return new WaitForSeconds(3);
	    SceneManager.LoadScene(0);
    }
}
