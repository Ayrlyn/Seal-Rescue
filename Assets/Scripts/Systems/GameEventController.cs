using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : Singleton<GameEventController>
{
    #region local methods
    Game _game;
    #endregion

    #region getters and setters
    Game Game { get { if(_game == null) { _game = Game.Instance; } return _game; } }
    #endregion

    #region unity methods
    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed; }
        catch { }        
    }
    #endregion

    #region local methods
    void OnTimePassed(TimePassed timePassed)
    {
        switch (timePassed)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
                break;
            case TimePassed.Day:
                break;
            case TimePassed.Week:
                break;
            case TimePassed.Month:
                break;
            case TimePassed.Year:
                break;
        }
    }
    #endregion

    #region public methods
    public void Init()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        if (Game.OneOffGameEvents.Add("FirstSealSpotted"))
        {
            Seal firstSeal = new Seal(0, SealHealth.Injured, 50, 50, SealMood.Lethargic, "TutoriSeal", SealRescueProgress.Rescue, SealSpecies.CommonSeal, 12.2f);
            KeyValuePair<Month, int> date = GameDateTime.Instance.CurrentMonth;
            EventMessenger.Instance.SendSealSpottedMessage(date.Key, date.Value, firstSeal);
            Game.Instance.Seals.Add(firstSeal);
        }
    }
    #endregion
}
