using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    #region serializable variables
    [Header("UI Elements")]
    [SerializeField] Canvas _canvas;
    [SerializeField] Transform _eventsButtonParent;
    [SerializeField] Transform _sealHospitalParent;
    [SerializeField] SealInfoPanel _sealInfoPanel;
    [Header("Buildings")]
    [SerializeField] SealHospital _sealHospital;
    [SerializeField] VisitorCentre _visitorCentre;
    [Header("Data")]
    [SerializeField]List<SealSpeciesData> _sealSpecies;
    #endregion

    #region local variables
    #endregion

    #region getters and setters
    public Canvas Canvas { get { return _canvas; } }
    public Transform EventsButtonParent { get { return _eventsButtonParent; } }
    public SealHospital SealHospital { get { return _sealHospital; } }
    public Transform SealHospitalParent { get { return _sealHospitalParent; } }
    public SealInfoPanel SealInfoPanel { get { return _sealInfoPanel; } }
    public List<SealSpeciesData> SealSpecies { get { return _sealSpecies; } }
    public VisitorCentre VisitorCentre { get { return _visitorCentre; } }
    #endregion


    #region public methods
    public string GetDisplayName(SealSpecies species)
    {
        foreach (SealSpeciesData speciesData in SealSpecies)
        {
            if(speciesData.SealSpecies == species) { return speciesData.SpeciesName; }
        }
        Debug.LogError($"Invalid seal species: {species.ToString()}");
        return ("");
    }
    #endregion
}
