using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpkeepController : Singleton<UpkeepController>, ISave<UpkeepSave>
{
    #region serializable variables
    [SerializeField] List<UpkeepData> _upkeeps;
    #endregion

    #region local variables
    HashSet<UpkeepData> _hourlyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _dailyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _weeklyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _monthlyUpkeeps = new HashSet<UpkeepData>();
    HashSet<UpkeepData> _yearlyUpkeeps = new HashSet<UpkeepData>();

    Resources _resources;
    #endregion

    #region getters and setters
    public List<UpkeepData> AllUpkeeps 
    {
        get
        {
            List<UpkeepData> upkeepDatas = new List<UpkeepData>();
            upkeepDatas.AddRange(HourlyUpkeeps.ToList());
            upkeepDatas.AddRange(DailyUpkeeps.ToList());
            upkeepDatas.AddRange(WeeklyUpkeeps.ToList());
            upkeepDatas.AddRange(MonthlyUpkeeps.ToList());
            upkeepDatas.AddRange(YearlyUpkeeps.ToList());

            return upkeepDatas;
        }
    }
    public HashSet<UpkeepData> HourlyUpkeeps { get { return _hourlyUpkeeps; } }
    public HashSet<UpkeepData> DailyUpkeeps { get { return _dailyUpkeeps; } }
    public HashSet<UpkeepData> WeeklyUpkeeps { get { return _weeklyUpkeeps; } }
    public HashSet<UpkeepData> MonthlyUpkeeps { get { return _monthlyUpkeeps; } }
    public HashSet<UpkeepData> YearlyUpkeeps { get { return _yearlyUpkeeps; } }
    #endregion

    #region unity methods
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
        foreach (UpkeepData data in _upkeeps)
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

    #region public methods
    public void Init()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        SortUpkeeps();
    }
    #endregion

    #region save load
    public void Load(UpkeepSave upkeepSave)
    {
        _upkeeps = new List<UpkeepData>();
        for (int i = 0; i < upkeepSave._upkeepFrequencies.Count; i++)
        {
            _upkeeps.Add(
                new UpkeepData((TimePassed)upkeepSave._upkeepFrequencies[i],
                upkeepSave._upkeepQuantities[i],
                (ResourceTypes)upkeepSave._upkeepResources[i]));
        }
        SortUpkeeps();
    }

    public UpkeepSave Save()
    {
        return new UpkeepSave(_upkeeps);
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
