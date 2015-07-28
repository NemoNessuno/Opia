using UnityEngine;
using System.Collections;
using System;

public class TimerController : MonoBehaviour {

    public GameObject TimerFrame;
    public TextMesh TimerText;

    public Action OnTimerEnd;

    private const int TIME = 10; //TODO: In Seconds <- Make configurable?
    private int _timer;

    void Start()
    {
        _timer = TIME;
        InvokeRepeating("DecreaseTimer", 1f, 1f);
    }

    void Update()
    {
        TimerFrame.transform.Rotate(new Vector3(0,0,1) * 36*Time.deltaTime);                
    }

    void DecreaseTimer()
    {
        _timer--;
        TimerText.text = ""+_timer;
        if (_timer <= 0) OnTimerEnd();
    }
}
