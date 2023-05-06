using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateTime : Singleton<GameDateTime>, ISave<DateTimeSave>
{
    #region editor variables
    public float _timeToPassOneMinute = 0.5f;
    #endregion

    #region local variables
    Day _currentDay = Day.Monday;
    int _currentHour = 9;
    int _currentMinute = 0;
    KeyValuePair<Month, int> _currentMonth = new KeyValuePair<Month, int>(Month.May, 1);
    float _minuteTimePassed = 0f;
    Dictionary<Month, int> _months = new Dictionary<Month, int>()
    {
        { Month.January, 31 },
        { Month.February, 28 },
        {Month.March, 31 },
        {Month.April, 30 },
        {Month.May, 31 },
        {Month.June, 30 },
        {Month.July, 31 },
        {Month.August, 31 },
        {Month.September,30 },
        {Month.October,31 },
        { Month.November,30},
        {Month.December, 31 }
    };
    int _timeScaleMultiplier = 1;
    #endregion

    #region getters and setters
    public Day CurrentDay { get { return _currentDay; } }
    public int CurrentHour { get { return _currentHour; } }
    public int CurrentMinute { get { return _currentMinute; } }
    public KeyValuePair<Month, int> CurrentMonth { get { return _currentMonth; } }
    #endregion

    #region unity methods
    void Update()
    {
        PassTime();
    }
    #endregion

    #region local methods
    void PassTime()
    {
        _minuteTimePassed += Time.deltaTime * _timeScaleMultiplier;
        if(_minuteTimePassed < _timeToPassOneMinute) { return; }

        _minuteTimePassed = 0;
        _currentMinute++;
        EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Minute);
        if (_currentMinute < 60) { return; }

        _currentMinute = 0;
        _currentHour++;
        EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Hour);
        if(CurrentHour == 9) { SaveManager.Instance.Save(); }
        if(_currentHour < 20) { return; }

        _currentHour = 7;
        EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Day);
        if ((int)_currentDay < 7) { _currentDay = (Day)(int)(_currentDay + 1); }
        else 
        { 
            _currentDay = (Day)1;
            EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Week);
        }

        if (_currentMonth.Value < _months[_currentMonth.Key]) { _currentMonth = new KeyValuePair<Month, int>(_currentMonth.Key, _currentMonth.Value + 1); }
        else
        {
            if ((int)_currentMonth.Key < 12) 
            { 
                _currentMonth = new KeyValuePair<Month, int>((Month)(int)_currentMonth.Key + 1, 1);
                EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Month);
            }
            else 
            { 
                _currentMonth = new KeyValuePair<Month, int>((Month)1, 1);
                EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Month);
                EventMessenger.Instance.SendTimeAndDateMessage(TimePassed.Year);
            }
        }
    }
    #endregion

    #region public methods
    public void SetGameDateTime(Day day, int hour, int minute, KeyValuePair<Month, int> month)
    {
        _currentDay = day;
        _currentHour = hour;
        _currentMinute = minute;
        _currentMonth = new KeyValuePair<Month, int>(month.Key, month.Value);
    }

    public void SetTimeScaleMultiplier(int multiplier)
    {
        _timeScaleMultiplier = multiplier;
    }
    #endregion

    #region save load
    public void Load(DateTimeSave dateTimeSave)
    {
        SetGameDateTime(
            (Day)dateTimeSave._currentDay, 
            dateTimeSave._currentHour, 
            dateTimeSave._currentMinute, 
            new KeyValuePair<Month, int>((Month)dateTimeSave._currentMonthMonth, dateTimeSave._currentMonthDay));
    }

    public DateTimeSave Save()
    {
        return new DateTimeSave(CurrentDay, CurrentHour, CurrentMinute, CurrentMonth);
    }
    #endregion
}
