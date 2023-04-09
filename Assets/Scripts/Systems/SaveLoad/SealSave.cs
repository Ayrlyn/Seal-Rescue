using System;
using System.Collections.Generic;

[Serializable]
public class SealSave
{
    public int _age;
    public int _health;
    public int _hunger;
    public int _mood;
    public string _name;
    public int _rescueDay;
    public int _rescueMonth;
    public int _rescueProgress;
    public int _species;
    public float _weight;

    public SealSave(int age, SealHealth health, int hunger, SealMood mood, string name, KeyValuePair<Month, int> rescueDate, SealRescueProgress rescueProgress, 
        SealSpecies species, float weight)
    {
        _age = age;
        _health = (int)health;
        _hunger = hunger;
        _mood = (int)mood;
        _name = name;
        _rescueDay = rescueDate.Value;
        _rescueMonth = (int)rescueDate.Key;
        _rescueProgress = (int)rescueProgress;
        _species = (int)species;
        _weight = weight;
    }
}
