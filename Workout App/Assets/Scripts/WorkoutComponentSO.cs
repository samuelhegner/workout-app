using UnityEngine;

[CreateAssetMenu(fileName = "New Workout Component", menuName = "Workout/Component", order = 1)]
public class WorkoutComponentSO : ScriptableObject
{
    public string _name;
    public string _subName;
    public int _setLengthTime;
    [Range(1, 50)] public int _setNumber;
    public int _setBreakTime;

    public void Init(WorkoutComponentPackage infoPackage)
    {
        this._name = infoPackage._nameInfo;
        this._subName = infoPackage._subNameInfo;
        this._setLengthTime = infoPackage._setLengthTimeInfo;
        this._setNumber = infoPackage._setCountInfo;
        this._setBreakTime = infoPackage._setBreakTimeInfo;
    }

    public static WorkoutComponentSO CreateInstance(WorkoutComponentPackage infoPackage)
    {
        var data = ScriptableObject.CreateInstance<WorkoutComponentSO>();
        data.Init(infoPackage);
        return data;
    }
}