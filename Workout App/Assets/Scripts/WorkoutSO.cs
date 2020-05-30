using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Workout", menuName = "Workout/Workout")]
public class WorkoutSO : ScriptableObject
{
    public List<WorkoutComponentSO> _components;
}