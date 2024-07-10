using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public TextMeshProUGUI TapToPlayText;
    void Start()
    {
        Time.timeScale = 0;
    }

    
    void Update()
    {
	    if (Input.touchCount > 0)
	    {
		    TapToPlayText.enabled = false;
		    Time.timeScale = 1;
	    } 
    }
}
