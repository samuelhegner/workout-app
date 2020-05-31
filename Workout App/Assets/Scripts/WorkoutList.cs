using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutList : MonoBehaviour
{
    public List<WorkoutSO> workouts;

    public static WorkoutList instance;

    private void Awake()
    {
        instance = this;
    }
}
