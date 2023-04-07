using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : MonoBehaviour
{
    #region local variables
    int _age;
    SealHealth _health;
    int _healthValue;
    float _hunger;
    SealMood _mood;
    string _name;
    KeyValuePair<Month, int> _rescueDay;
    SealRescueProgress _rescueProgress;
    SealSpecies _sealSpecies;
    float _weight;
    #endregion

    #region getters and setters
    public int Age { get { return _age; } }
    public SealHealth Health { get { return _health; } }
    public SealMood Mood { get { return _mood; } }
    public string Name { get { return _name; } }
    public KeyValuePair<Month, int> RescueDay { get { return RescueDay; } }
    public SealRescueProgress RescueProgress { get { return _rescueProgress; } }
    public SealSpecies SealSpecies { get { return _sealSpecies; } }
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
}
