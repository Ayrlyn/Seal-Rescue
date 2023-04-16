using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Seal : ISave<SealSave>
{
    #region local variables
    int _age;
    GameDateTime _gameDateTime;
    SealHealth _health;
    [Range(1, 100)] int _healthValue;
    [Range(1, 100)] int _hunger;
    int _iconAdult;
    int _iconPup;
    SealMood _mood;
    string _name;
    KeyValuePair<Month, int> _rescueDate;
    SealRescueProgress _rescueProgress;
    SceneReferences _sceneReferences;
    SealSpecies _sealSpecies;
    float _weight;
    #endregion

    #region getters and setters
    public int Age { get { return _age; } }
    public string CurrentLocationString
    {
        get
        {
            switch (RescueProgress)
            {
                case SealRescueProgress.Rescue: return "In the Wild";
                case SealRescueProgress.Arrival: return "Seal Hospital";
                case SealRescueProgress.Quarantine: return "Seal Hospital";
                case SealRescueProgress.TubeFeeding: return "Seal Nursery";
                case SealRescueProgress.ForceFeeding: return "Seal Nursery";
                case SealRescueProgress.HandFeeding: return "Seal Nursery";
                case SealRescueProgress.FishSchool: return "Seal Nursery";
                case SealRescueProgress.FreeFeeding: return "Seal Nursery";
                case SealRescueProgress.NurseryPool: return "Nursery Pool";
                case SealRescueProgress.RockPool: return "Rock Pool";
                case SealRescueProgress.PhysioPool: return "Physio Pool";
                case SealRescueProgress.PreReleasePool: return "Pre-Release Pool";
                case SealRescueProgress.Release: return "In the Wild";
                default:
                    Debug.LogError($"INVALID RESCUE PROGRESS: {RescueProgress}");
                    return "";
            }
        }
    }
    public GameDateTime GameDateTime { get { if(_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    public SealHealth Health { get { return _health; } }
    public int HealthValue { get { return _healthValue; } }
    public int Hunger { get { return _hunger; } }
    public Sprite IconAdult { get { return SceneReferences.GetIcons(SealSpecies, false)[_iconAdult]; } }
    public Sprite IconPup { get { return SceneReferences.GetIcons(SealSpecies, true)[_iconPup]; } }
    public SealMood Mood { get { return _mood; } }
    public string Name { get { return _name; } }
    public KeyValuePair<Month, int> RescueDate { get { return _rescueDate; } }
    public SealRescueProgress RescueProgress { get { return _rescueProgress; } }
    public string RescueProgressString
    {
        get
        {
            switch (RescueProgress)
            {
                case SealRescueProgress.Arrival: return "Arrival";
                case SealRescueProgress.Quarantine: return "Quarantine";
                case SealRescueProgress.TubeFeeding: return "Tube Feeding";
                case SealRescueProgress.ForceFeeding: return "Force Feeding";
                case SealRescueProgress.HandFeeding: return "Hand Feeding";
                case SealRescueProgress.FishSchool: return "Fish School";
                case SealRescueProgress.FreeFeeding: return "Free Feeding";
                case SealRescueProgress.NurseryPool: return "Nurdery Pool";
                case SealRescueProgress.RockPool: return "Rock Pool";
                case SealRescueProgress.PhysioPool: return "Physio Pool";
                case SealRescueProgress.PreReleasePool: return "Pre-Release Pool";
                case SealRescueProgress.Release: return "Released";
                default:
                    Debug.LogError($"INVALID RESCUE PROGRESS: {RescueProgress}");
                    return "";
            }
        }
    }
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    public SealSpecies SealSpecies { get { return _sealSpecies; } }
    public float Weight { get { return _weight; } }
    #endregion

    #region constructor
    public Seal(int age, SealHealth health, int healthValue, int hunger, SealMood mood, string name, SealRescueProgress rescueProgress, SealSpecies sealSpecies, float weight)
    {
        _age = age;
        _health = health;
        _healthValue = healthValue;
        _hunger = hunger;
        _mood = mood;
        _name = name;
        _rescueProgress = rescueProgress;
        _sealSpecies = sealSpecies;
        _weight = weight;

        _iconAdult = Random.Range(0, SceneReferences.GetIcons(sealSpecies, false).Count);
        _iconPup = Random.Range(0, SceneReferences.GetIcons(sealSpecies, false).Count);

    }

    public Seal(SealSave sealSave)
    {
        Load(sealSave);
    }
    #endregion

    #region public methods
    public void DayPassed()
    {
        for (int i = 0; i < 8; i++)
        {
            HourPassed();
        }
        if(_hunger <= 0) { return; }
        if(_hunger <= 50) { _weight += Random.Range(0.06f, 0.08f); }
        else { _weight += Random.Range(0.07f, 0.11f); }

        if(GameDateTime.CurrentMonth.Value == RescueDate.Value) { _age++; }
    }

    public void Feed()
    {
        _hunger += Random.Range(20, 30);
        _hunger = Mathf.Min(100, _hunger);
        if(_hunger > 75) { _mood = SealMood.Sleepy; }
    }

    public void HourPassed()
    {
        int randomHunger = Random.Range(2, 5);
        _hunger -= randomHunger;
        _hunger = Mathf.Max(0, _hunger);
        if (_hunger <= 75) { _mood = SealMood.Hungry; }

        int randomHealth = Random.Range(0, 4);
        _healthValue -= randomHealth;
        _healthValue = Mathf.Max(0, _healthValue);
        if (_healthValue <= 50 && _health != SealHealth.Injured) { _health = SealHealth.Sick; }
    }

    public void RescueMe()
    {
        _rescueProgress = SealRescueProgress.Arrival;
        _rescueDate = GameDateTime.CurrentMonth;
    }

    public void TreatIllness()
    {
        _healthValue += Random.Range(40, 60);
        _healthValue = Mathf.Min(100, _healthValue);
        if (_healthValue <= 50 && _health != SealHealth.Injured) { _health = SealHealth.Sick; }
        else { _health = SealHealth.Healthy; }
    }

    public void TreatWound()
    {
        _healthValue += Random.Range(20, 30);
        _healthValue = Mathf.Min(100, _healthValue);
        if (_healthValue <= 50 && _health == SealHealth.Injured) { _health = SealHealth.Injured; }
        else if(_healthValue <= 75) { _health = SealHealth.Sick; }
        else { _health = SealHealth.Healthy; }
    }
    #endregion

    #region save load
    public void Load(SealSave save)
    {
        _age = save._age;
        _health = (SealHealth)save._health;
        _healthValue = save._healthValue;
        _hunger = save._hunger;
        _iconAdult = save._iconAdult;
        _iconPup = save._iconPup;
        _mood = (SealMood)save._mood;
        _name = save._name;
        _rescueDate = new KeyValuePair<Month, int>((Month)save._rescueMonth, save._rescueDay);
        _rescueProgress = (SealRescueProgress)save._rescueProgress;
        _sealSpecies = (SealSpecies)save._species;
        _weight = save._weight;
    }

    public SealSave Save()
    {
        return new SealSave(Age, Health, _healthValue, _hunger, _iconAdult, _iconPup, Mood, Name, RescueDate, RescueProgress, SealSpecies, Weight);
    }
    #endregion
}
