using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    #region editor variables
    [SerializeField] GameDateTime _gameDateTime;
    [SerializeField] TMP_Text _gameDateTimeText;
    #endregion

    #region local variables
    void UpdateGameDateTimeText()
    {
        _gameDateTimeText.text = 
            $"{_gameDateTime.CurrentDay.ToString().Substring(0,3)}. " +
            $"{_gameDateTime.CurrentMonth.Value:D2} {_gameDateTime.CurrentMonth.Key.ToString().Substring(0,3)}. " +
            $"{_gameDateTime.CurrentHour:D2}:{_gameDateTime.CurrentMinute:D2} ";
    }
    #endregion

    #region unity methods
    void Update()
    {
        UpdateGameDateTimeText();
    }
    #endregion
}
