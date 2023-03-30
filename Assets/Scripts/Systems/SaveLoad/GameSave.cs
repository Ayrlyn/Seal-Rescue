using System;
using System.Collections.Generic;

[Serializable]
public class GameSave
{
    public DateTimeSave _dateTimeSave;
    public ResourcesSave _resourcesSave;
    public UpkeepSave _upkeepSave;

    public GameSave(DateTimeSave dateTimeSave, ResourcesSave resourcesSave, UpkeepSave upkeepSave)
    {
        _dateTimeSave = dateTimeSave;
        _resourcesSave = resourcesSave;
        _upkeepSave = upkeepSave;
    }
}
