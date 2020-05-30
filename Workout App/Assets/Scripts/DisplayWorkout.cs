﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class that updates UI to displays the information of the current workout component
/// </summary>
public class DisplayWorkout : MonoBehaviour
{
    public TextMeshProUGUI _componentName;
    public TextMeshProUGUI _componentSubname;
    public TextMeshProUGUI _componentSetIndicator;
    public TextMeshProUGUI _componentTimeLeft;

    [SerializeField] private WorkoutExecutor _executor;
    
    void Awake()
    {
        //subscribe to the newComponent event
        _executor.newComponentEvent += ChangeBasicDisplayInformation;
        _executor.newSetEvent += ChangeSetDisplayInformation;
    }

    private void Update()
    {
        if(_executor.TimeLeft > 0)
            _componentTimeLeft.text = ConvertTimeLeftFormat(_executor.TimeLeft);
    }


    /// <summary>
    /// Update UI elements to display the information of the new workout component that started
    /// </summary>
    /// <param name="newComponent">The new workout component that just started</param>
    void ChangeBasicDisplayInformation(WorkoutComponentSO newComponent)
    {
        _componentName.text = newComponent._name;
        _componentSubname.text = newComponent._subName;
    }
    
    
    void ChangeSetDisplayInformation(WorkoutComponentSO newComponent, int setCount)
    {
        string setIdicatorText = string.Format("Set: {0} of {1}", setCount,  newComponent._setNumber);
        _componentSetIndicator.text = setIdicatorText;
    }
    
    /// <summary>
    /// Function that converts float number of seconds into Minute:Second format
    /// </summary>
    /// <param name="timeToConvert"> The float value to convert </param>
    /// <returns> Returns the float value as a MIN:SEC string</returns>
    string ConvertTimeLeftFormat(float timeToConvert)
    {
        int minutes = Mathf.FloorToInt(timeToConvert / 60f);
        int seconds = Mathf.CeilToInt(timeToConvert - minutes * 60);
        string minutesAndSeconds = string.Format("{0:00}:{1:00}", minutes, seconds);

        return minutesAndSeconds;
    }

    void OnDisable()
    {
        //un-subscribe from the newComponent event
        _executor.newComponentEvent -= ChangeBasicDisplayInformation;
        _executor.newSetEvent -= ChangeSetDisplayInformation;
    }
}
