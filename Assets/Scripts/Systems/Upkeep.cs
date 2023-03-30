using System;
using System.Collections.Generic;
using UnityEngine;

public class Upkeep : MonoBehaviour
{
    #region serializable variables
    [SerializeField] List<UpkeepData> upkeeps;
    #endregion

    #region local variables
    HashSet<UpkeepData> _hourlyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _dailyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _weeklyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _monthlyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _yearlyUpkeeps = new HashSet<UpkeepData>();

    Resources _resources;
    #endregion

    #region unity methods
    void Awake()
    {
        _resources = Resources.Instance;
    }

    void Start()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        SortUpkeeps();
    }

    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; }
        catch { }
    }
    #endregion

    #region local methods
    void OnTimePassed(TimePassed period)
    {
        switch (period)
        {
            case TimePassed.Hour:
                foreach (UpkeepData upkeepData in _hourlyUpkeeps)
                {
                    SpendResource(upkeepData);
                }
                break;
            case TimePassed.Day:
                foreach (UpkeepData upkeepData in _dailyUpkeeps)
                {
                    SpendResource(upkeepData);
                }
                break;
            case TimePassed.Week:
                foreach (UpkeepData upkeepData in _weeklyUpkeeps)
                {
                    SpendResource(upkeepData);
                }
                break;
            case TimePassed.Month:
                foreach (UpkeepData upkeepData in _monthlyUpkeeps)
                {
                    SpendResource(upkeepData);
                }
                break;
            case TimePassed.Year:
                foreach (UpkeepData upkeepData in _yearlyUpkeeps)
                {
                    SpendResource(upkeepData);
                }
                break;
        }
    }

    void SortUpkeeps()
    {
        foreach (UpkeepData data in upkeeps)
        {
            switch (data.Frequency)
            {
                case TimePassed.Hour:
                    _hourlyUpkeeps.Add(data);
                    break;
                case TimePassed.Day:
                    _dailyUpkeeps.Add(data);
                    break;
                case TimePassed.Week:
                    _weeklyUpkeeps.Add(data);
                    break;
                case TimePassed.Month:
                    _monthlyUpkeeps.Add(data);
                    break;
                case TimePassed.Year:
                    _yearlyUpkeeps.Add(data);
                    break;
            }
        }
    }

    void SpendResource(UpkeepData upkeepData)
    {
        if(_resources == null) { _resources = Resources.Instance; }
        switch (upkeepData.ResourceType)
        {
            case ResourceTypes.Food:
                _resources.SpendFood(upkeepData.Quantity);
                break;
            case ResourceTypes.Materials:
                _resources.SpendMaterials(upkeepData.Quantity);
                break;
            case ResourceTypes.Medicine:
                _resources.SpendMedicine(upkeepData.Quantity);
                break;
            case ResourceTypes.Money:
                _resources.SpendMoney(upkeepData.Quantity);
                break;
        }
    }
    #endregion
}

[Serializable]
public class UpkeepData
{
    #region serializable variables
    [SerializeField] string _name;
    [SerializeField] TimePassed _frequency;
    [SerializeField] int _quantity;
    [SerializeField] ResourceTypes _resourceType;
    #endregion

    #region getters and setters
    public TimePassed Frequency { get { return _frequency; } }
    public int Quantity { get { return _quantity; } set { _quantity = value; } }
    public ResourceTypes ResourceType { get { return _resourceType; } }
    #endregion

    public UpkeepData(TimePassed frequency, int quantity, ResourceTypes resourceType)
    {
        _frequency = frequency;
        _quantity = quantity;
        _resourceType = resourceType;
    }
}
