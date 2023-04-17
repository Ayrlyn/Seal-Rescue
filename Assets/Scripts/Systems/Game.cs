using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : Singleton<Game>, ISave<GameSave>
{
    #region serializable variables
    [Header("Prefabs")]
    [SerializeField] EmployeeIconPrefab _employeeIconPrefab;
    [SerializeField] SealIconPrefab _sealIconPrefab;
    #endregion

    #region local variables
    HashSet<Employee> _employees = new HashSet<Employee>();
    Dictionary<Employee, EmployeeIconPrefab> _employeesAndPrefabs = new Dictionary<Employee, EmployeeIconPrefab>();
    GameDateTime _gameDateTime;
    GameEventController _gameEventController;
    GameEventsUI _gameEventsUI;
    HashSet<string> _oneOffGameEvents = new HashSet<string>();
    Resources _resources;
    SaveManager _saveManager;
    SceneReferences _sceneReferences;
    SealHospital _sealHospital;
    HashSet<Seal> _seals = new HashSet<Seal>();
    Dictionary<Seal, SealIconPrefab> _sealsAndPrefabs = new Dictionary<Seal, SealIconPrefab>();
    int _sealSpottedChance = 1;
    UpkeepController _upkeepController;
    #endregion

    #region getters and setters
    public HashSet<Employee> Employees { get { return _employees; } set { _employees = value; } }
    public GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    public GameEventController GameEventController { get { if (_gameEventController == null) { _gameEventController = GameEventController.Instance; } return _gameEventController; } }
    public GameEventsUI GameEventsUI { get { if (_gameEventsUI == null) { _gameEventsUI = GameEventsUI.Instance; } return _gameEventsUI; } }
    public HashSet<string> OneOffGameEvents { get { return _oneOffGameEvents; } set { _oneOffGameEvents = value; } }
    public Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
    public SaveManager SaveManager { get { if (_saveManager == null) { _saveManager = SaveManager.Instance; } return _saveManager; } }
    public SceneReferences SceneReferences { get { if(_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    public HashSet<Seal> Seals { get { return _seals; } set { _seals = value; } }
    public int SealSpottedChance { get { return _sealSpottedChance; } }
    public UpkeepController UpkeepController { get { if (_upkeepController == null) { _upkeepController = UpkeepController.Instance; } return _upkeepController; } }
    #endregion

    #region unity methods
    void Start()
    {
        SaveManager.Load();
        GameEventController.Init();
        SceneReferences.SealHospital.Init();
        SceneReferences.VisitorCentre.Init();
        UpkeepController.Init();
        GameEventsUI.Init();
    }

    void OnApplicationQuit()
    {
        SaveManager.Save();
    }
    #endregion

    #region local methods
    void GenerateEmployee(Employee employee)
    {
        Employees.Add(employee);
        EmployeeIconPrefab iconPrefab = Instantiate(_employeeIconPrefab);
        iconPrefab.Init(employee);
        if (!_employeesAndPrefabs.ContainsKey(employee)) { _employeesAndPrefabs.Add(employee, iconPrefab); }
        else if(_employeesAndPrefabs[employee] == null) { _employeesAndPrefabs[employee] = iconPrefab; }

        switch (employee.Location)
        {
            case "SealHospital":
                SceneReferences.SealHospital.ReceiveEmployee(iconPrefab);
                break;
            case "VisitorCentre":
                SceneReferences.VisitorCentre.ReceiveEmployee(iconPrefab);
                break;
            default:
                break;
        }
        iconPrefab.transform.localPosition = Vector3.zero;
        iconPrefab.transform.localScale = Vector3.one;
    }

    void GenerateSeal(Seal seal)
    {
        Seals.Add(seal);
        SealIconPrefab iconPrefab = Instantiate(_sealIconPrefab);
        Button button = iconPrefab.GetComponent<Button>();
        iconPrefab.Init(seal);
        if (!_sealsAndPrefabs.ContainsKey(seal)) { _sealsAndPrefabs.Add(seal, iconPrefab); }
        else if(_sealsAndPrefabs[seal] == null) { _sealsAndPrefabs[seal] = iconPrefab; }

        switch (seal.RescueProgress)
        {
            case SealRescueProgress.Arrival:
                iconPrefab.transform.SetParent(SceneReferences.SealHospitalParent, false);
                break;
            case SealRescueProgress.Quarantine:
                iconPrefab.transform.SetParent(SceneReferences.SealHospitalParent, false);
                break;
            case SealRescueProgress.TubeFeeding:
                break;
            case SealRescueProgress.ForceFeeding:
                break;
            case SealRescueProgress.HandFeeding:
                break;
            case SealRescueProgress.FishSchool:
                break;
            case SealRescueProgress.FreeFeeding:
                break;
            case SealRescueProgress.NurseryPool:
                break;
            case SealRescueProgress.RockPool:
                break;
            case SealRescueProgress.PhysioPool:
                break;
            case SealRescueProgress.PreReleasePool:
                break;
            case SealRescueProgress.Release:
                break;
            default:
                break;
        }
        iconPrefab.transform.localPosition = Vector3.zero;
        button.onClick.AddListener(() => SceneReferences.SealInfoPanel.ShowSeal(seal));
    }
    #endregion

    #region save load
    public void Load(GameSave state)
    {
        GameDateTime.Load(state._dateTimeSave);
        foreach (string gameEvent in state._oneOffGameEvents)
        {
            OneOffGameEvents.Add(gameEvent);
        }
        foreach (EmployeeSave employeeSave in state._employeeSaves)
        {
            Employee loadedEmployee = new Employee(employeeSave);
            GenerateEmployee(loadedEmployee);
        }
        Resources.Load(state._resourcesSave);
        foreach (SealSave sealSave in state._sealSaves)
        {
            Seal loadedSeal = new Seal(sealSave);
            GenerateSeal(loadedSeal);
        }
        UpkeepController.Load(state._upkeepSave);
    }

    public GameSave Save()
    {
        DateTimeSave dateTimeSave = GameDateTime.Save();
        List<EmployeeSave> savedEmployees = new List<EmployeeSave>();
        foreach (Employee employee in Employees)
        {
            savedEmployees.Add(employee.Save());
        }
        ResourcesSave resourcesSave = Resources.Save();
        List<SealSave> savedSeals = new List<SealSave>();
        foreach (Seal seal in Seals)
        {
            savedSeals.Add(seal.Save());
        }
        UpkeepSave upkeepSave = UpkeepController.Save();
        return new GameSave(dateTimeSave, savedEmployees, OneOffGameEvents, resourcesSave, savedSeals, upkeepSave);
    }
    #endregion
}
