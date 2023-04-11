using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seals/Species")]
public class SealSpeciesData : ScriptableObject
{
    [Header("Data"), SerializeField] SealSpecies sealSpecies;
    [SerializeField] string speciesName;
    [SerializeField] Vector2 birthLengthRange;
    [SerializeField] Vector2 birghtWeightRange;
    [SerializeField] List<Month> earlyPuppingMonths;
    [SerializeField] List<Month> latePuppingMonths;
    [SerializeField] List<Month> puppingMonths;
    [SerializeField] Vector2 releaseWeightRange;
    [SerializeField] Vector2 weaningTimeRange;
    [Header("Graphics"), SerializeField] List<Sprite> adultIcons;
    [SerializeField] List<Sprite> pupIcons;

    #region getters and setters
    public List<Sprite> AdultIcons { get { return adultIcons; } }
    public List<Sprite> PupIcons { get { return pupIcons; } }
    public string SpeciesName { get { return speciesName; } }
    public SealSpecies SealSpecies { get { return sealSpecies; } }
    #endregion
}
