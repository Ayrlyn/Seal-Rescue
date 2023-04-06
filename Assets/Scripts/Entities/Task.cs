using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    #region local variables
    int _minutesRemaining;
    int _minutesRequired;
    int _quantityRequired;
    Resources _resourceRequired;
    #endregion

    #region getters and setters
    public int MinutesRemaining { get { return _minutesRemaining; } set { _minutesRemaining = value; } }
    public int MinutesRequired { get { return _minutesRequired; } }
    public int QuantityRequired { get { return _quantityRequired; } }
    public Resources ResourceRequired { get { return _resourceRequired; } }
    #endregion

    public Task(int minutesRequired, int quantityRequired, Resources resourceRequired)
    {
        _minutesRemaining = minutesRequired;
        _minutesRequired = minutesRequired;
        _quantityRequired = quantityRequired;
        _resourceRequired = resourceRequired;
    }
}
