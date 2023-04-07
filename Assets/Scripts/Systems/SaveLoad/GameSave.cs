using System;
using System.Collections.Generic;

[Serializable]
public class GameSave
{
    public DateTimeSave _dateTimeSave;
    public HashSet<string> _oneOffGameEvents;
    public ResourcesSave _resourcesSave;
    public UpkeepSave _upkeepSave;

    public GameSave(DateTimeSave dateTimeSave, HashSet<string> oneOffGameEvents, ResourcesSave resourcesSave, UpkeepSave upkeepSave)
    {
        _dateTimeSave = dateTimeSave;
        _oneOffGameEvents = oneOffGameEvents;
        _resourcesSave = resourcesSave;
        _upkeepSave = upkeepSave;
    }
}
