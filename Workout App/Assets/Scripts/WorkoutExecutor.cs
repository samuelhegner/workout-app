using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class that can execute any workout
/// </summary>
public class WorkoutExecutor : MonoBehaviour
{
    public WorkoutSO workoutToExecute;

    private float _timeLeft;

    public float TimeLeft
    {
        get => _timeLeft;
    }

    /// <summary>
    /// Event gets called when a new Workout component starts
    /// </summary>
    public event Action<WorkoutComponentSO> newComponentEvent;
    
    /// <summary>
    /// Event gets called when a new set starts
    /// </summary>
    public event Action<WorkoutComponentSO, int> newSetEvent;

    void Start()
    {
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
                newSetEvent(workoutComponentSo, i + 1);
                print("Timer Started");
                float workTimer = workoutComponentSo._setLengthTime;

                while (workTimer > 0)
                {
                    //count down the set time
                    workTimer -= Time.deltaTime;
                    _timeLeft = workTimer;
                    yield return new WaitForEndOfFrame();
                }
                
                //skip this if the components break time is 0
                if (workoutComponentSo._setBreakTime != 0)
                {
                    print("Break Started");

                    //count down the break time
                    float breakTimer = workoutComponentSo._setBreakTime;

                    while (breakTimer > 0)
                    {
                        breakTimer -= Time.deltaTime;
                        _timeLeft = breakTimer;
                        yield return new WaitForEndOfFrame();
                    }
                    
                }
            }
        }

        yield return null;
    }
}
