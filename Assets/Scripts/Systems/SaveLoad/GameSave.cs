using System;
using System.Collections.Generic;

[Serializable]
public class GameSave
{
    public DateTimeSave _timeDateSave;
    public ResourcesSave _resourcesSave;

    public GameSave(DateTimeSave dateTimeSave, ResourcesSave resourcesSave)
    {
        _timeDateSave = dateTimeSave;
        _resourcesSave = resourcesSave;
    }
}
