using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    #region serializable variables
    [Header("UI Elements")]
    [SerializeField] Canvas _canvas;
    [SerializeField] Transform _eventsButtonParent;
    [SerializeField] SealInfoPanel _sealInfoPanel;
    [Header("Buildings")]
    [SerializeField] Nursery _nursery;
    [SerializeField] SealHospital _sealHospital;
    [SerializeField] VisitorCentre _visitorCentre;
    [Header("Data")]
    [SerializeField] List<string> _sealNames;
    [SerializeField]List<SealSpeciesData> _sealSpecies;
    #endregion

    #region local variables
    Game _game;
    GameDateTime _gameDateTime;
    Resources _resources;
    #endregion

    #region getters and setters
    public Canvas Canvas { get { return _canvas; } }
    public Transform EventsButtonParent { get { return _eventsButtonParent; } }
    public Game Game { get { if (_game == null) { _game = Game.Instance; } return _game; } }
    public GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    public Nursery Nursery { get { return _nursery; } }
    public Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
    public SealHospital SealHospital { get { return _sealHospital; } }
    public SealInfoPanel SealInfoPanel { get { return _sealInfoPanel; } }
    public List<string> SealNames { get { return _sealNames; } }
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

    public List<Sprite> GetIcons(SealSpecies species, bool isPup)
    {
        foreach (SealSpeciesData speciesData in SealSpecies)
        {
            if(speciesData.SealSpecies == species)
            {
                if (isPup) { return speciesData.PupIcons; }
                return speciesData.AdultIcons;
            }
        }
        Debug.LogError($"Invalid seal species: {species.ToString()}");
        return new List<Sprite>();
    }

    public SealSpeciesData GetSpeciesData(SealSpecies species)
    {
        foreach (SealSpeciesData speciesData in SealSpecies)
        {
            if(speciesData.SealSpecies == species) { return speciesData; }
        }
        Debug.LogError($"Invalid seal species: {species.ToString()}");
        return null;
    }
    #endregion
}
