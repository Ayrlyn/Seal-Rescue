using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SealIconPrefab : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] Image _icon;
	[SerializeField] Image _taskProgress;
    #endregion

    #region local variables
    Seal _seal;
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    void OnDestroy()
    {
        try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; }
        catch { }
    }
    #endregion

    #region local methods
    void OnTimePassed(TimePassed timePassed)
    {
        switch (timePassed)
        {
            case TimePassed.Hour:
                _seal.HourPassed();
                break;
            case TimePassed.Day:
                _seal.DayPassed();
                break;
        }
    }
	#endregion

	#region public methods
	public void Init(Seal seal)
    {
        EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;

        _seal = seal;
		_icon.sprite = seal.IconAdult;
    }
	#endregion

	#region coroutines
	#endregion
}
