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
                break;
            case SealRescueProgress.RockPool:
                break;
            case SealRescueProgress.PhysioPool:
                break;
            case SealRescueProgress.PreReleasePool:
                break;
            case SealRescueProgress.Release:
                break;
            default:
                break;
        }
    }
    #endregion

    #region public methods
    public bool BonusProgress(List<Employee> employees)
    {
        foreach (Employee employee in employees)
        {
            switch (_taskType)
            {
                case TaskType.Clean: 
                    if (employee.Specialty == WorkerSkills.Handy) { return true; }
                    break;
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
                    return false;
            }
        }
        return false;
    }

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
    #endregion
}
