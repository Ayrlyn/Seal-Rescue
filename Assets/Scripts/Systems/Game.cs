using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>, ISave<GameSave>
{
    #region local variables
    GameDateTime _gameDateTime;
    GameEventController _gameEventController;
    HashSet<string> _oneOffGameEvents = new HashSet<string>();
    Resources _resources;
    SaveManager _saveManager;
    SealHospital _sealHospital;
    HashSet<Seal> _seals = new HashSet<Seal>();
    UpkeepController _upkeepController;
    #endregion

    #region getters and setters
    GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    GameEventController GameEventController { get { if (_gameEventController == null) { _gameEventController = GameEventController.Instance; } return _gameEventController; } }
    public HashSet<string> OneOffGameEvents { get { return _oneOffGameEvents; } set { _oneOffGameEvents = value; } }
    Resources Resources { get { if(_resources == null) { _resources = Resources.Instance; } return _resources; } }
    SaveManager SaveManager { get { if (_saveManager == null) { _saveManager = SaveManager.Instance; } return _saveManager; } }
    SealHospital SealHospital { get { if (_sealHospital == null) { _sealHospital = SealHospital.Instance; } return _sealHospital; } }
    public HashSet<Seal> Seals { get { return _seals; } set { _seals = value; } }
    UpkeepController UpkeepController { get { if (_upkeepController == null) { _upkeepController = UpkeepController.Instance; } return _upkeepController; } }
    #endregion

    #region unity methods
    void Start()
    {
        SaveManager.Load();
        GameEventController.Init();
        SealHospital.Init();
        UpkeepController.Init();
    }

    void OnApplicationQuit()
    {
        SaveManager.Save();
    }
    #endregion

    #region save load
    public void Load(GameSave state)
    {
        GameDateTime.Load(state._dateTimeSave);
        OneOffGameEvents = state._oneOffGameEvents;
        Resources.Load(state._resourcesSave);
        foreach (SealSave sealSave in state._sealSaves)
        {
            Seal loadedSeal = new Seal(sealSave);
            Seals.Add(loadedSeal);
        }
        UpkeepController.Load(state._upkeepSave);
    }

    public GameSave Save()
    {
        DateTimeSave dateTimeSave = GameDateTime.Save();
        ResourcesSave resourcesSave = Resources.Save();
        List<SealSave> savedSeals = new List<SealSave>();
        foreach (Seal seal in Seals)
        {
            savedSeals.Add(seal.Save());
        }
        UpkeepSave upkeepSave = UpkeepController.Save();
        return new GameSave(dateTimeSave, OneOffGameEvents, resourcesSave, savedSeals, upkeepSave);
    }
    #endregion
}
