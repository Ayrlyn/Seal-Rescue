using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seals/Species")]
public class SealSpeciesData : ScriptableObject
{
    [SerializeField] string speciesName;
    [SerializeField] Vector2 birthLengthRange;
    [SerializeField] Vector2 birghtWeightRange;
    [SerializeField] List<Month> earlyPuppingMonths;
    [SerializeField] List<Month> latePuppingMonths;
    [SerializeField] List<Month> puppingMonths;
    [SerializeField] Vector2 releaseWeightRange;
    [SerializeField] Vector2 weaningTimeRange;
}
