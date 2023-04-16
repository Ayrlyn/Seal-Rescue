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
    public bool HasRescources(List<KeyValuePair<ResourceTypes, int>> resources)
    {
        foreach (KeyValuePair<ResourceTypes, int> resourceRequirement in resources)
        {
            switch (resourceRequirement.Key)
            {
                case ResourceTypes.Food:
                    if (Food < resourceRequirement.Value) { return false; }
                    break;
                case ResourceTypes.Materials:
                    if (Materials < resourceRequirement.Value) { return false; }
                    break;
                case ResourceTypes.Medicine:
                    if (Medicine < resourceRequirement.Value) { return false; }
                    break;
                case ResourceTypes.Money:
                    if (Money < resourceRequirement.Value) { return false; }
                    break;
                default:
                    Debug.LogError($"Invalid resource type: {resourceRequirement.Key}");
                    break;
            }
        }
        return true;
    }
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

    public bool SpendResource(ResourceTypes resourceType, int quantity)
    {
        switch (resourceType)
        {
            case ResourceTypes.Food: return SpendFood(quantity);
            case ResourceTypes.Materials: return SpendMaterials(quantity);
            case ResourceTypes.Medicine: return SpendMedicine(quantity);
            case ResourceTypes.Money: return SpendMoney(quantity);
            default:
                Debug.LogError($"Invalid Resource Tye: {resourceType}");
                return false;
        }
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
