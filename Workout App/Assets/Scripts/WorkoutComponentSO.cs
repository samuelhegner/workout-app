using UnityEngine;

[CreateAssetMenu(fileName = "New Workout Component", menuName = "Workout/Component", order = 1)]
public class WorkoutComponentSO : ScriptableObject
{
    public string _name;
    public string _subName;
    public int _setLengthTime;
    [Range(1, 50)] public int _setNumber;
    public int _setBreakTime;

    public void Init(string name, string subname, int componentTimeLength, int setNumber, int setBreakTime)
    {
        this._name = name;
        this._subName = subname;
        this._setLengthTime = componentTimeLength;
        this._setNumber = setNumber;
        this._setBreakTime = setBreakTime;
    }

    public static WorkoutComponentSO CreateInstance(string name, string subname, int componentTimeLength, int setNumber, int setBreakTime)
    {
        var data = ScriptableObject.CreateInstance<WorkoutComponentSO>();
        data.Init(name, subname, componentTimeLength, setNumber, setBreakTime);
        return data;
    }
}