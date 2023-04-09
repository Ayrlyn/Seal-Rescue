using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : MonoBehaviour, ISave<SealSave>
{
    #region local variables
    int _age;
    SealHealth _health;
    int _healthValue;
    int _hunger;
    SealMood _mood;
    string _name;
    KeyValuePair<Month, int> _rescueDate;
    SealRescueProgress _rescueProgress;
    SealSpecies _sealSpecies;
    float _weight;
    #endregion

    #region getters and setters
    public int Age { get { return _age; } }
    public SealHealth Health { get { return _health; } }
    public SealMood Mood { get { return _mood; } }
    public string Name { get { return _name; } }
    public KeyValuePair<Month, int> RescueDate { get { return _rescueDate; } }
    public SealRescueProgress RescueProgress { get { return _rescueProgress; } }
    public SealSpecies SealSpecies { get { return _sealSpecies; } }
    public float Weight { get { return _weight; } }
    #endregion

    #region constructor
    public Seal(int age, SealHealth health, int healthValue, SealMood mood, string name, SealRescueProgress rescueProgress, SealSpecies sealSpecies, float weight)
    {
        _age = age;
        _health = health;
        _healthValue = healthValue;
        _mood = mood;
        _name = name;
        _rescueProgress = rescueProgress;
        _sealSpecies = sealSpecies;
        _weight = weight;
    }
    #endregion

    #region save load
    public void Load(SealSave save)
    {
        _age = save._age;
        _health = (SealHealth)save._health;
        _hunger = save._hunger;
        _mood = (SealMood)save._mood;
        _name = save._name;
        _rescueDate = new KeyValuePair<Month, int>((Month)save._rescueMonth, save._rescueDay);
        _sealSpecies = (SealSpecies)save._species;
        _weight = save._weight;
    }

    public SealSave Save()
    {
        return new SealSave(Age, Health, _hunger, Mood, Name, RescueDate, RescueProgress, SealSpecies, Weight);
    }
    #endregion
}
