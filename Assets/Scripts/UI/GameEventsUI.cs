using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsUI : MonoBehaviour
{

}

public class GameEventInfo
{
    #region local variables
    string _eventDescription;
    string _eventName;
    #endregion

    #region getters and setters
    public string EventDescription { get { return _eventDescription; } }
    public string EventName { get { return _eventName; } }
    #endregion

    #region constructors
    public GameEventInfo(string eventDescription, string eventName)
    {
        _eventDescription = eventDescription;
        _eventName = eventName;
    }
    #endregion
}
