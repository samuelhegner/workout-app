using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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


    public void SaveAllWorkouts()
    {
        for(int i = 0; i < WorkoutList.instance.workouts.Count; i++)
        {
            SaveWorkout(WorkoutList.instance.workouts[i], i);
        }
    }

    public void SaveWorkout(WorkoutSO workoutSo, int index)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(persistentDataPath);
        }


        string workoutPath = persistentDataPath + "/workout_"+ index;

        if (!Directory.Exists(workoutPath))
        {
            Directory.CreateDirectory(workoutPath);
        }

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(workoutPath + "/" + workoutSo.name + ".txt");

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
        
        string workoutPath = persistentDataPath + "/workout_"+ indexChecker;
        
        while (Directory.Exists(workoutPath))
        {
            //TODO deserialize the workout and all of its components and push it into the WorkoutList list
            string[] files = Directory.GetFiles(workoutPath);

            foreach (var name in files)
            {
                print(name);
            }
            
            indexChecker++;
            workoutPath = persistentDataPath + "/workout_"+ indexChecker;
        }

    }

    public void LoadWorkout()
    {
        
    }
}