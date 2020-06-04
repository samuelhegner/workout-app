using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateWorkoutScroll : MonoBehaviour
{
    [SerializeField] private GameObject _workoutUIPrefab;

    [SerializeField] private float _initialYValue = -50f;
    [SerializeField] private float _gapYValue = 100;

    private RectTransform _rectTransform;
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        InitialPopulation();
    }

    void InitialPopulation()
    {
        for (int i = 0; i < WorkoutList.instance.workouts.Count; i++)
        {
            GameObject newWorkoutUI = Instantiate(_workoutUIPrefab, Vector3.zero, Quaternion.identity, transform);
            
            newWorkoutUI.transform.localPosition = new Vector3(0, _initialYValue + (-_gapYValue * i), 0);
            
            newWorkoutUI.GetComponent<UpdateWorkoutUI>().UpdateInformation(WorkoutList.instance.workouts[i]);

            _rectTransform.offsetMin = _rectTransform.offsetMin + new Vector2(0, -_gapYValue);
        }
    }
}
