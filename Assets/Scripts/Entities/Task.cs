using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    #region local variables
    float _minutesRemaining;
    int _minutesRequired;
    Resources _resources;
    List<KeyValuePair<ResourceTypes, int>> _resourcesRequired;
    SceneReferences _sceneReferences;
    Seal _seal;
    TaskType _taskType;
    #endregion

    #region getters and setters
    public float MinutesRemaining { get { return _minutesRemaining; } set { _minutesRemaining = value; } }
    public int MinutesRequired { get { return _minutesRequired; } }
    public Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    public List<KeyValuePair<ResourceTypes, int>> ResourcesRequired { get { return _resourcesRequired; } }
    public Seal Seal { get { return _seal; } }
    public TaskType TaskType { get { return _taskType; } }
    public string TaskTypeString
    {
        get
        {
            switch (TaskType)
            {
                case TaskType.Clean: return $"Clean up after the seals";
                case TaskType.Feed: return $"Time to feed {Seal.Name}";
                case TaskType.Maintenance: return $"This buildings needs maintenance";
                case TaskType.Tourism: return $"Visitors have come to see the centre";
                case TaskType.Transfer: return $"Moving {Seal.Name} to the next level of care";
                case TaskType.TreatIllness: return $"{Seal.Name} is sick";
                case TaskType.TreatInjury: return $"Treat {Seal.Name}'s injury";
                default:
                    Debug.LogError($"INVALID TASK TYPE: {TaskType}");
                    return "";
            }
        }
    }
    #endregion

    #region constructors
    public Task()
    {

    }

    public Task(int minutesRequired, List<KeyValuePair<ResourceTypes, int>> resourcesRequired, TaskType taskType, Seal seal = null)
    {
        _minutesRemaining = minutesRequired;
        _minutesRequired = minutesRequired;
        _resourcesRequired = resourcesRequired;
        _taskType = taskType;
        _seal = seal;
    }
    #endregion

    #region local methods
    void ReleaseSeal()
    {
        GameEventInfo gameEventInfo = new GameEventInfo(
            $"You have successfully raised {Seal.Name} and now they are ready to be released into the wild.",
            $"Release {Seal.Name}",
            GameEventType.SealRelease);
        SceneReferences.GameEventInfoDisplay.ShowEventInfo(gameEventInfo);
    }

    void TransferSeal()
    {
        Seal.IncreaseProgress();
        switch (Seal.RescueProgress)
        {
            case SealRescueProgress.Rescue:
                SceneReferences.SealHospital.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.Arrival:
                SceneReferences.SealHospital.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.Quarantine:
                SceneReferences.SealHospital.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.TubeFeeding:
                SceneReferences.Nursery.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.ForceFeeding:
                SceneReferences.Nursery.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.HandFeeding:
                SceneReferences.Nursery.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.FishSchool:
                SceneReferences.Nursery.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.FreeFeeding:
                SceneReferences.Nursery.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.NurseryPool:
                SceneReferences.FirstPool.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.RockPool:
                SceneReferences.FirstPool.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.PhysioPool:
                SceneReferences.FirstPool.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.PreReleasePool:
                SceneReferences.FirstPool.ReceiveSeal(SceneReferences.Game.SealsAndPrefabs[Seal]);
                break;
            case SealRescueProgress.Release:
                ReleaseSeal();
                break;
            default:
                break;
        }
    }
    #endregion

    #region public methods
    public void CompleteTask()
    {
        foreach (KeyValuePair<ResourceTypes, int> cost in ResourcesRequired)
        {
            Resources.SpendResource(cost.Key, cost.Value);
        }
        switch (_taskType)
        {
            case TaskType.Clean:
                break;
            case TaskType.Feed:
                _seal.Feed();
                break;
            case TaskType.Maintenance:
                break;
            case TaskType.Tourism:
                int spending = Random.Range(30, 151);
                Resources.GainMoney(spending);
                break;
            case TaskType.Transfer:
                TransferSeal();
                break;
            case TaskType.TreatIllness:
                _seal.TreatIllness();
                break;
            case TaskType.TreatInjury:
                _seal.TreatWound();
                break;
            default:
                break;
        }
    }

    public bool RequiredSpecialisation(List<Employee> employees)
    {
        foreach (Employee employee in employees)
        {
            switch (_taskType)
            {
                case TaskType.Maintenance:
                    if (employee.Specialty == WorkerSkills.Handy) { return true; }
                    break;
                case TaskType.Tourism:
                    if (employee.Specialty == WorkerSkills.Community) { return true; }
                    break;
                case TaskType.TreatIllness:
                    if (employee.Specialty == WorkerSkills.Medicine) { return true; }
                    break;
                case TaskType.TreatInjury:
                    if (employee.Specialty == WorkerSkills.Medicine) { return true; }
                    break;
                default:
                    return true;
            }
        }
        return false;
    }
    #endregion
}
