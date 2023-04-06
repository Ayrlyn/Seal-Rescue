using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : MonoBehaviour
{
    #region local variables
    int _age;
    KeyValuePair<Month, int> _birthday;
    SealHealth _health;
    float _hunger;
    SealMood _mood;
    string _name;
    KeyValuePair<Month, int> _rescueDay;
    SealRescueProgress _rescueProgress;
    float _weight;
    #endregion

    #region getters and setters
    public int Age { get { return _age; } }
    public SealHealth Health { get { return _health; } }
    public SealMood Mood { get { return _mood; } }
    public KeyValuePair<Month, int> RescueDay { get { return RescueDay; } }
    public SealRescueProgress RescueProgress { get { return _rescueProgress; } }
    #endregion
}
