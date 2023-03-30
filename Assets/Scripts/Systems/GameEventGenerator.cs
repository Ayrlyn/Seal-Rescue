using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WorldData")]
public class GameEventGenerator : ScriptableObject
{
    [SerializeField] List<SealSpottingChance> _sealSpottingChances;
}

[Serializable]
public class SealSpottingChance
{
    [SerializeField] string _name;
    [SerializeField] int _hour;
    [SerializeField, Range(1, 100)] int _chance;
}
