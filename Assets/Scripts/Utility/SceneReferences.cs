using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    #region serializable variables
    [Header("UI Elements")]
    [SerializeField] Canvas _canvas;
    [SerializeField] EmployeeInfoPanel _employeeInfoPanel;
    [SerializeField] Transform _eventsButtonParent;
    [SerializeField] GameEventInfoDisplay _gameEventInfoDisplay;
    [SerializeField] PopupNotificationScript _popupNotificationScript;
    [SerializeField] SealInfoPanel _sealInfoPanel;
    [SerializeField] GameObject _weeklyChoices;
    [SerializeField] GameObject _tutorial;
    [Header("Buildings")]
    [SerializeField] FirstPool _firstPool;
    [SerializeField] Nursery _nursery;
    [SerializeField] SealHospital _sealHospital;
    [SerializeField] VisitorCentre _visitorCentre;
    [Header("Data")]
    [SerializeField] List<string> _employeeNames;
    [SerializeField] List<string> _sealNames;
    [SerializeField]List<SealSpeciesData> _sealSpecies;
    [Header("Sprites")]
    [SerializeField] Sprite _communitySprite;
    [SerializeField] Sprite _fishSprite;
    [SerializeField] Sprite _materialsSprite;
    [SerializeField] Sprite _medicineSprite;
    [SerializeField] Sprite _moneySprite;
    [Header("Colours")]
    [SerializeField] Color _communityColour = Color.cyan;
    [SerializeField] Color _handyColour = Color.grey;
    [SerializeField] Color _managementColour = Color.magenta;
    [SerializeField] Color _medicineColour = Color.blue;
    #endregion

    #region local variables
    Game _game;
    GameDateTime _gameDateTime;
    HireController _hireController;
    Resources _resources;
    UpkeepController _upkeepController;
    #endregion

    #region getters and setters
    public Canvas Canvas { get { return _canvas; } }
    public Sprite CommunitySprite { get { return _communitySprite; } }
    public EmployeeInfoPanel EmployeeInfoPanel { get { return _employeeInfoPanel; } }
    public List<string> EmployeeNames { get { return _employeeNames; } }
    public Transform EventsButtonParent { get { return _eventsButtonParent; } }
    public FirstPool FirstPool { get { return _firstPool; } }
    public Sprite FishSprite { get { return _fishSprite; } }
    public Game Game { get { if (_game == null) { _game = Game.Instance; } return _game; } }
    public GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    public GameEventInfoDisplay GameEventInfoDisplay { get { return _gameEventInfoDisplay; } }
    public HireController HireController { get { if(_hireController == null) { _hireController = HireController.Instance; } return _hireController; } }
    public Sprite MaterialsSprite { get { return _materialsSprite; } }
    public Sprite MedicineSprite { get { return _medicineSprite; } }
    public Sprite MoneySprite { get { return _moneySprite; } }
    public Nursery Nursery { get { return _nursery; } }
    public PopupNotificationScript PopupNotificationScript { get { return _popupNotificationScript; } }
    public Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
    public SealHospital SealHospital { get { return _sealHospital; } }
    public SealInfoPanel SealInfoPanel { get { return _sealInfoPanel; } }
    public List<string> SealNames { get { return _sealNames; } }
    public List<SealSpeciesData> SealSpecies { get { return _sealSpecies; } }
    public GameObject Tutorial { get { return _tutorial; } }
    public UpkeepController UpkeepController { get { if (_upkeepController == null) { _upkeepController = UpkeepController.Instance; } return _upkeepController; } }
    public VisitorCentre VisitorCentre { get { return _visitorCentre; } }
    public GameObject WeeklyChoices { get { return _weeklyChoices; } }
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

    public Color GetSpeciastColour(WorkerSkills skill)
    {
        switch (skill)
        {
            case WorkerSkills.Community: return _communityColour;
            case WorkerSkills.Medicine: return _medicineColour;
            case WorkerSkills.Management: return _managementColour;
            case WorkerSkills.Handy: return _handyColour;
            default: return Color.black;
        }
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
