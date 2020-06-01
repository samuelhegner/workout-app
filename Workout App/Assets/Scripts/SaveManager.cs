using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Class that saves and loads workouts at runtime
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public static string persistentDataPath;

    void Awake()
    {
        instance = this;
        persistentDataPath = Application.persistentDataPath + "/workout_save";

        LoadAllWorkouts();
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(persistentDataPath);
    }

    private void OnDisable()
    {
        SaveAllWorkouts();
    }


    public void SaveAllWorkouts()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(persistentDataPath);
        }

        string[] oldDirectoryPaths = Directory.GetDirectories(persistentDataPath);

        print(oldDirectoryPaths[0]);

        foreach (var oldDirectory in oldDirectoryPaths)
        {
            Directory.Delete(oldDirectory, true);
        }

        for (int i = 0; i < WorkoutList.instance.workouts.Count; i++)
        {
            SaveWorkout(WorkoutList.instance.workouts[i], i);
        }
    }

    public void SaveWorkout(WorkoutSO workoutSo, int index)
    {
        string workoutPath = persistentDataPath + "/workout_" + index;

        if (!Directory.Exists(workoutPath))
        {
            Directory.CreateDirectory(workoutPath);
        }

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(workoutPath + "/" + workoutSo._name + ".txt");

        var json = JsonUtility.ToJson(workoutSo);

        bf.Serialize(file, json);

        file.Close();

        for (int i = 0; i < workoutSo._components.Count; i++)
        {
            string componentPath = workoutPath + "/" + workoutSo._components[i]._name;

            if (!Directory.Exists(componentPath))
            {
                Directory.CreateDirectory(componentPath);
            }

            BinaryFormatter bf2 = new BinaryFormatter();

            FileStream componentFile = File.Create(componentPath + "/component_" + i + ".txt");

            var json2 = JsonUtility.ToJson(workoutSo._components[i]);

            bf2.Serialize(componentFile, json2);
            componentFile.Close();
        }
    }

    public void LoadAllWorkouts()
    {
        if (!IsSaveFile())
            return;

        int indexChecker = 0;

        string workoutPath = persistentDataPath + "/workout_" + indexChecker;

        BinaryFormatter bf = new BinaryFormatter();

        while (Directory.Exists(workoutPath))
        {
            string[] files = Directory.GetFiles(workoutPath);
            WorkoutSO savedWorkoutSo = ScriptableObject.CreateInstance<WorkoutSO>();

            foreach (var foundFile in files)
            {
                FileStream file = File.Open(foundFile, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), savedWorkoutSo);
                file.Close();
            }
            
            savedWorkoutSo._components.Clear();
            
            string[] componentDirectoryPaths = Directory.GetDirectories(workoutPath);

            foreach (var foundComponentDirectoryPath in componentDirectoryPaths)
            {
                string[] componentFiles = Directory.GetFiles(foundComponentDirectoryPath);

                foreach (var component in componentFiles)
                {
                    WorkoutComponentSO savedWorkoutComponentSo = ScriptableObject.CreateInstance<WorkoutComponentSO>();
                    FileStream file = File.Open(component, FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), savedWorkoutComponentSo);
                    file.Close();

                    savedWorkoutSo._components.Add(savedWorkoutComponentSo);
                }
            }

            WorkoutList.instance.workouts.Add(savedWorkoutSo);
            indexChecker++;
            workoutPath = persistentDataPath + "/workout_" + indexChecker;
        }
    }
}