using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventController : Singleton<GameEventController>
{
    #region unity methods
    void Start()
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        if (Game.Instance.OneOffGameEvents.Add("FirstSealSpotted"))
        {
            Seal firstSeal = new Seal(0, SealHealth.Injured, 50, SealMood.Lethargic, "TutoriSeal", SealRescueProgress.Rescue, SealSpecies.CommonSeal, 12.2f);
            KeyValuePair<Month, int> date = GameDateTime.Instance.CurrentMonth;
            EventMessenger.Instance.SendSealSpottedMessage(date.Key, date.Value, firstSeal);
        }
    }

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
}
