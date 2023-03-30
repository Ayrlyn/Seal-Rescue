using System;
using System.Collections.Generic;

[Serializable]
public class ResourcesSave
{
    public int _food;
    public int _materials;
    public int _medicine;
    public int _money;

    public ResourcesSave(int food, int materials, int medicine, int money)
    {
        _food = food;
        _materials = materials;
        _medicine = medicine;
        _money = money;
    }
}
