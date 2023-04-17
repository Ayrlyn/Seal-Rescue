using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHospital : Building
{
    #region serializable variables
    #endregion

    #region local variables
    int _sealCapacity = 2;
    #endregion

    #region getters and setters
    public List<Seal> ResidentSeals { get 
        {
            List<Seal> residentSeals = new List<Seal>();
            foreach (Seal seal in Game.Seals)
            {
                if(seal.RescueProgress == SealRescueProgress.Arrival || seal.RescueProgress == SealRescueProgress.Quarantine) { residentSeals.Add(seal); }
            }
            return residentSeals;
        } 
    }
    public int SealCapacity { get { return _sealCapacity; } }
    #endregion

    #region unity methods
    #endregion

    #region local methods
    void CheckSealStatus(Seal seal)
    {
        switch (seal.Health)
        {
            case SealHealth.Injured:
                CreateTask(TaskType.TreatInjury, seal);
                break;
            case SealHealth.Sick:
                CreateTask(TaskType.TreatIllness, seal);
                break;
        }
        switch (seal.Mood)
        {
            case SealMood.Hungry:
                CreateTask(TaskType.Feed, seal);
                break;
        }
        if(seal.IsHappyAndHealthy)
        {
            CreateTask(TaskType.Transfer, seal);
        }
    }

    void CreateTask(TaskType taskType, Seal seal = null)
    {
        Task newTask = new Task();
        switch (taskType)
        {
            case TaskType.Clean:
                newTask = new Task(60, new List<KeyValuePair<ResourceTypes, int>>(), taskType);
                break;
            case TaskType.Feed:
                newTask = new Task(
                    60, 
                    new List<KeyValuePair<ResourceTypes, int>>() 
                    { 
                        new KeyValuePair<ResourceTypes, int>(ResourceTypes.Food, 5), 
                        new KeyValuePair<ResourceTypes, int>(ResourceTypes.Medicine, 2) 
                    },
                    taskType, 
                    seal);
                break;
            case TaskType.Maintenance:
                newTask = new Task(
                    120,
                    new List<KeyValuePair<ResourceTypes, int>>() { new KeyValuePair<ResourceTypes, int>(ResourceTypes.Materials, 10) },
                    taskType);
                break;
            case TaskType.Transfer:
                newTask = new Task(
                    30,
                    new List<KeyValuePair<ResourceTypes, int>>(),
                    taskType,
                    seal);
                break;
            case TaskType.TreatIllness:
                newTask = new Task(
                    60,
                    new List<KeyValuePair<ResourceTypes, int>> { new KeyValuePair<ResourceTypes, int>(ResourceTypes.Medicine, 5) },
                    taskType,
                    seal);
                break;
            case TaskType.TreatInjury:
                newTask = new Task(
                    90,
                    new List<KeyValuePair<ResourceTypes, int>>() 
                    {
                        new KeyValuePair<ResourceTypes, int>(ResourceTypes.Medicine, 3),
                        new KeyValuePair<ResourceTypes, int>(ResourceTypes.Materials, 2)
                    },
                    taskType,
                    seal);
                break;
            default:
                Debug.LogError($"Invalid Task Type in Seal Hospital: {taskType}");
                break;
        }
        if(!DoesTaskExist(newTask.TaskType, newTask.Seal))
        {
            _tasks.Add(newTask);
            if(_tasks.Count == 1) { _taskUI.Init(newTask); }
        }
    }
    #endregion

    #region public methods
    public override void CheckTasks()
    {
        foreach (Seal seal in ResidentSeals)
        {
            CheckSealStatus(seal);
        }
    }

    public override bool HasSpaceForSeal()
    {
        return ResidentSeals.Count <= SealCapacity;
    }
    public void Init()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;
        foreach (Seal seal in ResidentSeals)
        {
            CheckSealStatus(seal);
        }
        _prefabName = "SealHospital";
    }

    public override void OnTimePassed(TimePassed time)
    {
        base.OnTimePassed(time);
        switch (time)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
                foreach (Seal seal in ResidentSeals)
                {
                    CheckSealStatus(seal);
                }
                break;
            case TimePassed.Day:
                CreateTask(TaskType.Clean);
                break;
            case TimePassed.Week:
                CreateTask(TaskType.Maintenance);
                break;
            case TimePassed.Month:
                break;
            case TimePassed.Year:
                break;
            default:
                break;
        }
    }

    public override void ReceiveEmployee(EmployeeIconPrefab employeeIconPrefab)
    {
        base.ReceiveEmployee(employeeIconPrefab);
        employeeIconPrefab.Employee.Location = "SealHospital";
    }
    #endregion
}
