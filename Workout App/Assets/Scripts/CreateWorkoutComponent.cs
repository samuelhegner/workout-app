using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Save Input information into workout component
/// </summary>
public class CreateWorkoutComponent : MonoBehaviour
{
    public WorkoutSO CurrentWorkoutSo;
    
    public TMP_InputField _nameInput;
    public TMP_InputField _subNameInput;

    public Slider _setCountInput;
    
    public TMP_InputField[] _setLengthTimeInput;
    public TMP_InputField[] _setBreakTimeInput;
    
    /// <summary>
    /// Collects the information required to form an Workout Component Package
    /// </summary>
    public void CollectInformation()
    {
        WorkoutComponentPackage collectedInfoPackage;

        collectedInfoPackage._nameInfo = _nameInput.text;
        collectedInfoPackage._subNameInfo = _subNameInput.text;
        collectedInfoPackage._setCountInfo = (int)_setCountInput.value;
        collectedInfoPackage._setLengthTimeInfo = TimeConverter.ConvertIntoSeconds(_setLengthTimeInput[0].text, _setLengthTimeInput[1].text);
        collectedInfoPackage._setBreakTimeInfo = TimeConverter.ConvertIntoSeconds(_setBreakTimeInput[0].text, _setBreakTimeInput[1].text);
        
        SaveWorkoutComponent(collectedInfoPackage);
    }

    void SaveWorkoutComponent(WorkoutComponentPackage package)
    {
        WorkoutComponentSO newComponentSo = WorkoutComponentSO.CreateInstance(package);
        CurrentWorkoutSo._components.Add(newComponentSo);
    }


    
}

/// <summary>
/// Package that contains all info required for a Workout Component
/// </summary>
public struct WorkoutComponentPackage
{
    public string _nameInfo;
    public string _subNameInfo;
    public int _setCountInfo;
    public int _setLengthTimeInfo;
    public int _setBreakTimeInfo;
}
