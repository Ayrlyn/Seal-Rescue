using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class GameSave
{
    public DateTimeSave _dateTimeSave;
    public List<string> _oneOffGameEvents;
    public ResourcesSave _resourcesSave;
    public List<SealSave> _sealSaves;
    public UpkeepSave _upkeepSave;

    public GameSave(DateTimeSave dateTimeSave, HashSet<string> oneOffGameEvents, ResourcesSave resourcesSave, List<SealSave> sealSaves, UpkeepSave upkeepSave)
    {
        _dateTimeSave = dateTimeSave;
        _oneOffGameEvents = oneOffGameEvents.ToList();
        _resourcesSave = resourcesSave;
        _sealSaves = sealSaves;
        _upkeepSave = upkeepSave;
    }
}
