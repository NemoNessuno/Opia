using UnityEngine;
using System.Collections;
using System;

public class DisablingController : MonoBehaviour {

    public TimerController Timer;
    public Action OnDisabledEnd;

	// Use this for initialization
	void Start () {
        Timer.OnTimerEnd += () =>
        {
            OnDisabledEnd();
            gameObject.SetActive(false);
        };
	}
	
}