using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessenger : SingletonDontDestroy<EventMessenger>
{
    public event Action<Month, int, Seal> OnSealSpotted;
    public event Action<TimePassed> OnTimeAndDateChange;

    public void SendSealSpottedMessage(Month month, int day, Seal seal)
    {
        OnSealSpotted?.Invoke(month, day, seal);
    }

    public void SendTimeAndDateMessage(TimePassed timePassed)
    {
        OnTimeAndDateChange?.Invoke(timePassed);
    }
}
