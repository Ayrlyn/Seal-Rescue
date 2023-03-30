using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>, ISave<GameSave>
{
    #region local variables
    GameDateTime _gameDateTime;
    Resources _resources;
    SaveManager _saveManager;
    #endregion

    #region unity methods
    public override void Awake()
    {
        base.Awake();
        _gameDateTime = GameDateTime.Instance;
        _resources = Resources.Instance;
        _saveManager = SaveManager.Instance;
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
        DateTimeSave timeDateSave = state._timeDateSave;
        _gameDateTime.SetGameDateTime(
            (Day)timeDateSave._currentDay,
            timeDateSave._currentHour, 
            timeDateSave._currentMinute, 
            new KeyValuePair<Month, int>((Month)timeDateSave._currentMonthMonth, timeDateSave._currentMonthDay));

        ResourcesSave resourcesSave = state._resourcesSave;
        _resources.SetResources(resourcesSave._food, resourcesSave._materials, resourcesSave._medicine, resourcesSave._money);
    }

    public GameSave Save()
    {
        DateTimeSave dateTimeSave = new DateTimeSave(_gameDateTime.CurrentDay, _gameDateTime.CurrentHour, _gameDateTime.CurrentMinute, _gameDateTime.CurrentMonth);
        ResourcesSave resourcesSave = new ResourcesSave(_resources.Food, _resources.Materials, _resources.Medicine, _resources.Money);
        return new GameSave(dateTimeSave, resourcesSave);
    }
    #endregion
}
