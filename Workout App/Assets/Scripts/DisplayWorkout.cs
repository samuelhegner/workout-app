using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWorkout : MonoBehaviour
{
    public TextMeshProUGUI _componentName;
    public TextMeshProUGUI _componentSubname;
    public TextMeshProUGUI _componentTimeLeft;

    [SerializeField] private WorkoutExecutor _executor;
    
    void Awake()
    {
        FindObjectOfType<WorkoutExecutor>().newComponentEvent += ChangeDisplay;
    }

    private void Update()
    {
        if(_executor.TimeLeft > 0)
            _componentTimeLeft.text = ConvertTimeLeftFormat(_executor.TimeLeft);
    }


    void ChangeDisplay(WorkoutComponentSO newComponent)
    {
        _componentName.text = newComponent._name;
        _componentSubname.text = newComponent._subName;
    }

    //converts float to minutes and seconds format string
    string ConvertTimeLeftFormat(float timeLeft)
    {
        string minutesAndSeconds;
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.CeilToInt(timeLeft - minutes * 60);
        minutesAndSeconds = string.Format("{0:00}:{1:00}", minutes, seconds);

        return minutesAndSeconds;
    }
}
