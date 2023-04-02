using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private TimeParser _parser;
    private TimeDisplayer _timeDisplayer;

    private int _hours = 0;
    private int _minutes = 0;
    private int _seconds = 0;
    private double _floatSeconds = 0;

    private void Awake()
    {
        _parser = GetComponent<TimeParser>();
        _timeDisplayer = GetComponent<TimeDisplayer>();
        _parser.OnTimeParse += OnTimeParse;
    }

    private void Update()
    {
        _floatSeconds += Time.deltaTime;

        if (_floatSeconds >= 1)
        {
            UpdateSecondsAndDisplayTime();
            ChangeSecondPoints();
        } 
    }

    private void UpdateSecondsAndDisplayTime()
    {
        _seconds++;
        _floatSeconds -= 1.0;

        if (_seconds >= 60)
        {
            _seconds = 0;
            _minutes++;
            if (_minutes >= 60)
            {
                _minutes = 0;
                _hours++;
                if (_hours >= 24)
                {
                    _hours = 0;
                }
            }
            DisplayTimeDigitalClock();
        }
        
        DisplayTimeMechanicClock();
    }


    private void OnTimeParse()
    {
        _hours = _parser.Hours;
        _minutes = _parser.Minutes;
        _seconds = _parser.Seconds;
        _floatSeconds = 0;
        DisplayTimeDigitalClock();
        DisplayTimeMechanicClock();
    }

    public void ChangeSecondPoints() => 
        _timeDisplayer.ChangeSecondPoints();

    public void DisplayTimeDigitalClock() => 
        _timeDisplayer.DisplayDigitalTime(_hours,_minutes);
    
    public void DisplayTimeMechanicClock() => 
        _timeDisplayer.DisplayMechanicTime(_hours,_minutes);
}
