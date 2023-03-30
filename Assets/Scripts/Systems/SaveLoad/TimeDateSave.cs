using System;
using System.Collections.Generic;

[Serializable]
public class TimeDateSave
{
    public int _currentDay;
    public int _currentHour;
    public int _currentMinute;
    public int _currentMonthDay;
    public int _currentMonthMonth;

    public TimeDateSave(Day day, int hour, int minute, KeyValuePair<Month, int> month)
    {
        _currentDay = (int)day;
        _currentHour = hour;
        _currentMinute = minute;
        _currentMonthDay = (int)month.Key;
        _currentMonthMonth = month.Value;
    }
}
