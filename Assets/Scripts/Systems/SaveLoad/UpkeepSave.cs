using System;
using System.Collections.Generic;

[Serializable]
public class UpkeepSave
{
    public List<int> _upkeepFrequencies = new List<int>();
    public List<int> _upkeepQuantities = new List<int>();
    public List<int> _upkeepResources = new List<int>();

    public UpkeepSave(List<UpkeepData> upkeeps)
    {
        foreach (UpkeepData upkeepData in upkeeps)
        {
            _upkeepFrequencies.Add((int)upkeepData.Frequency);
            _upkeepQuantities.Add(upkeepData.Quantity);
            _upkeepResources.Add((int)upkeepData.ResourceType);
        }
    }
}
