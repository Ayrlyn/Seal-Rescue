using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHospital : MonoBehaviour
{
    #region local variables
    List<Seal> _residentSeals = new List<Seal>();
    Dictionary<Seal, Task> _sealsAndTasks = new Dictionary<Seal, Task>();
    int _sealCapacity = 2;
    List<Task> _tasks = new List<Task>();
    #endregion

    #region unity methods
    void Start()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;
    }

    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; } 
        catch { }
    }
    #endregion

    #region local methods
    void CheckSealStatus(Seal seal)
    {

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
}
