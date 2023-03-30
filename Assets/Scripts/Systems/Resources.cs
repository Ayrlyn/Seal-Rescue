using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : Singleton<Resources>, ISave<ResourcesSave>
{
    #region local variables
    int _food = 1000;
    int _materials = 1000;
    int _medicine = 1000;
    int _money = 1000;
    #endregion

    #region getter and setters
    public int Food { get { return _food; } }
    public int Materials { get { return _materials; } }
    public int Medicine { get { return _medicine; } }
    public int Money { get { return _money; } }
    #endregion

    #region public methods
    public void SetResources(int? food = null, int? materials = null, int? medicine = null, int?money = null)
    {
        _food = food ?? _food;
        _materials = materials ?? _materials;
        _medicine = medicine ?? _medicine;
        _money = money ?? _money;
    }

    public bool SpendFood(int quantity)
    {
        if(quantity > Food) { return false; }
        _food -= quantity;
        return true;
    }

    public bool SpendMaterials(int quantity)
    {
        if (quantity > Materials) { return false; }
        _materials -= quantity;
        return true;
    }

    public bool SpendMedicine(int quantity)
    {
        if (quantity > Medicine) { return false; }
        _medicine -= quantity;
        return true;
    }

    public bool SpendMoney(int quantity)
    {
        if (quantity > Money) { return false; }
        _money -= quantity;
        return true;
    }
    #endregion

    #region save load
    public void Load(ResourcesSave resourcesSave)
    {
        SetResources(resourcesSave._food, resourcesSave._materials, resourcesSave._medicine, resourcesSave._money);
    }

    public ResourcesSave Save()
    {
        return new ResourcesSave(Food, Materials, Medicine, Money);
    }
    #endregion
}
