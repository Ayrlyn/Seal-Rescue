using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHospital : Singleton<SealHospital>
{
    #region local variables
    List<Seal> _residentSeals = new List<Seal>();
    Dictionary<Seal, Task> _sealsAndTasks = new Dictionary<Seal, Task>();
    int _sealCapacity = 2;
    List<Task> _tasks = new List<Task>();
    #endregion

    #region getters and setters
    public bool HasSpaceForSeal { get { return ResidentSeals.Count <= SealCapacity; } }
    public List<Seal> ResidentSeals { get { return _residentSeals; } }
    public int SealCapacity { get { return _sealCapacity; } }
    #endregion

    #region unity methods
    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; } 
        catch { }
    }
    #endregion

    #region local methods
    void CheckSealStatus(Seal seal)
    {
        switch (seal.Health)
        {
            case SealHealth.Healthy:
                break;
            case SealHealth.Injured:
                break;
            case SealHealth.Sick:
                break;
            case SealHealth.Starving:
                break;
        }

        switch (seal.Mood)
        {
            case SealMood.Hungry:
                break;
        }
    }

    void OnTimePassed(TimePassed time)
    {
        switch (time)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
                foreach (Seal seal in _residentSeals)
                {
                    if (!_sealsAndTasks.ContainsKey(seal)) { CheckSealStatus(seal); }
                }
                break;
            case TimePassed.Day:
                break;
            case TimePassed.Week:
                break;
            case TimePassed.Month:
                break;
            case TimePassed.Year:
                break;
            default:
                break;
        }
    }
    #endregion

    #region public methods
    public void Init()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;
    }
    #endregion
}
