using System;
using System.Collections.Generic;

[Serializable]
public class GameSave
{
    public TimeDateSave _timeDateSave;

    public GameSave(TimeDateSave timeDateSave)
    {
        _timeDateSave = timeDateSave;
    }
}
