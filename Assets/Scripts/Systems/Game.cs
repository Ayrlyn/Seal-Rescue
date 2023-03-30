using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>, ISave<GameSave>
{
    #region local variables
    GameDateTime _gameDateTime;
    SaveManager _saveManager;
    #endregion

    #region unity methods
    public override void Awake()
    {
        base.Awake();
        _gameDateTime = GameDateTime.Instance;
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
        TimeDateSave timeDateSave = state._timeDateSave;
        _gameDateTime.SetGameDateTime(
            (Day)timeDateSave._currentDay,
            timeDateSave._currentHour, 
            timeDateSave._currentMinute, 
            new KeyValuePair<Month, int>((Month)timeDateSave._currentMonthMonth, timeDateSave._currentMonthDay));
    }

    public GameSave Save()
    {
        TimeDateSave timeDateSave = new TimeDateSave(_gameDateTime.CurrentDay, _gameDateTime.CurrentHour, _gameDateTime.CurrentMinute, _gameDateTime.CurrentMonth);
        return new GameSave(timeDateSave);
    }
    #endregion
}
