using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>, ISave<GameSave>
{
    #region local variables
    GameDateTime _gameDateTime;
    Resources _resources;
    SaveManager _saveManager;
    UpkeepController _upkeepController;
    #endregion

    #region unity methods
    public override void Awake()
    {
        base.Awake();
        _gameDateTime = GameDateTime.Instance;
        _resources = Resources.Instance;
        _saveManager = SaveManager.Instance;
        _upkeepController = UpkeepController.Instance;
    }

    void Start()
    {
        _saveManager.Load();
    }

    void OnApplicationQuit()
    {
        _saveManager.Save();
    }
    #endregion

    #region save load
    public void Load(GameSave state)
    {
        _gameDateTime.Load(state._dateTimeSave);
        _resources.Load(state._resourcesSave);
        _upkeepController.Load(state._upkeepSave);
    }

    public GameSave Save()
    {
        DateTimeSave dateTimeSave = _gameDateTime.Save();
        ResourcesSave resourcesSave = _resources.Save();
        UpkeepSave upkeepSave = _upkeepController.Save();
        return new GameSave(dateTimeSave, resourcesSave, upkeepSave);
    }
    #endregion
}
