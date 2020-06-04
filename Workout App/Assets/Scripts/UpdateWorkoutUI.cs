using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateWorkoutUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameDisplay;
    [SerializeField] private TextMeshProUGUI _componentDisplay;
    [SerializeField] private TextMeshProUGUI _timeDisplay;

    public void UpdateInformation(WorkoutSO _workoutSo)
    {
        _nameDisplay.text = _workoutSo._name;

        string totalComponents = "Total Components: " + _workoutSo._components.Count;
        _componentDisplay.text = totalComponents;


        string totalTime = "Total Time: " + CalculateTotalComponentTime(_workoutSo);
        _timeDisplay.text = totalTime;
    }

    private string CalculateTotalComponentTime(WorkoutSO _workoutSo)
    {
        int totalSeconds = 0;
        foreach (WorkoutComponentSO component in _workoutSo._components)
        {
            totalSeconds += (component._setLengthTime * component._setNumber) + (component._setBreakTime * component._setNumber);
        }

        string totalMinSec = TimeConverter.ConvertTimeLeftFormat(totalSeconds);
        return totalMinSec;
    }
}


public static class TimeConverter
{
    /// <summary>
    /// Parses two strings into a total second count
    /// </summary>
    /// <param name="minutes"> Minute string </param>
    /// <param name="seconds"> Second string </param>
    /// <returns> Returns the total number of seconds </returns>
    public static int ConvertIntoSeconds(string minutes, string seconds)
    {
        int.TryParse(minutes, out int minuteInt);
        int.TryParse(seconds, out int secondInt);

        if (minutes == String.Empty)
        {
            minuteInt = 0;
        }

        if (seconds == String.Empty)
        {
            secondInt = 0;
        }

        int totalSeconds = (minuteInt * 60) + secondInt;
        return totalSeconds;
    }
    
    /// <summary>
    /// Function that converts float number of seconds into Minute:Second format
    /// </summary>
    /// <param name="timeToConvert"> The float value to convert </param>
    /// <returns> Returns the float value as a MIN:SEC string</returns>
    public static string ConvertTimeLeftFormat(float timeToConvert)
    {
        int minutes = Mathf.FloorToInt(timeToConvert / 60f);
        int seconds = Mathf.CeilToInt(timeToConvert - minutes * 60);
        string minutesAndSeconds = string.Format("{0:00}:{1:00}", minutes, seconds);

        return minutesAndSeconds;
    }
}
