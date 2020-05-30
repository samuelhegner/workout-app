using System;
using System.Collections;
using UnityEngine;

public class WorkoutExecutor : MonoBehaviour
{

    public WorkoutSO workoutToExecute;

    public event Action<WorkoutComponentSO> newComponentEvent;

    void Start()
    {
        ExecuteWorkout(workoutToExecute);
    }

    void ExecuteWorkout(WorkoutSO workoutToExecute)
    {
        StartCoroutine(RunWorkout(workoutToExecute));
    }

    //Coroutine Runs through the workout
    IEnumerator RunWorkout(WorkoutSO workoutSo)
    {
        //for every component in the workout, loop through each component and run through the sets and the alocated setTime and breakTime
        foreach (WorkoutComponentSO workoutComponentSo in workoutSo._components)
        {
            //new component started
            if (newComponentEvent != null)
            {
                newComponentEvent(workoutComponentSo);
            }

            for (int i = 0; i < workoutComponentSo._setNumber; i++)
            {
                float workTimer = workoutComponentSo._setLengthTime;

                while (workTimer > 0)
                {
                    //count down the set time
                    workTimer -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                
                //skip this if the components break time is 0
                if (workoutComponentSo._setBreakTime != 0)
                {
                    //count down the break time
                    float breakTimer = workoutComponentSo._setBreakTime;

                    while (breakTimer > 0)
                    {
                        workTimer -= Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }
                    
                }
            }
        }

        yield return null;
    }
}
