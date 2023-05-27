using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : ISave<EmployeeSave>
{
    #region local variables
    Building _currentBuilding;
    bool _hired;
    string _location;
	int _monthlySalary;
	string _name;
    SceneReferences _sceneReferences;
    WorkerSkills _specialty;
    #endregion

    #region getters and setters
    public Building CurrentBuilding { get { return _currentBuilding; } set { _currentBuilding = value; } }
    public bool Hired { get { return _hired; } set { _hired = value; } }
    public string Location { get { return _location; } set { _location = value; } }
    public int MonhtlySalary { get { return _monthlySalary; } }
    public string Name { get { return _name; } }
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    public WorkerSkills Specialty { get { return _specialty; } }
    public Sprite SpecialtySprite
    {
        get
        {
            switch (Specialty)
            {
                case WorkerSkills.Community: return SceneReferences.CommunitySprite;
                case WorkerSkills.Medicine: return SceneReferences.MedicineSprite;
                case WorkerSkills.Management: return SceneReferences.MoneySprite;
                case WorkerSkills.Handy: return SceneReferences.MaterialsSprite;
                default: return null;
            }
        }
    }
    public string SpecialtyString
    {
        get
        {
            switch (Specialty)
            {
                case WorkerSkills.Community: return "People Skills";
                case WorkerSkills.Medicine: return "Medicine";
                case WorkerSkills.Management: return "Administration";
                case WorkerSkills.Handy: return "Maintenance";
                default: return "";
            }
        }
    }
    #endregion

    #region constructors
    public Employee(string name, string location, int salary, WorkerSkills specialty, bool hired)
    {
        _hired = hired;
        _name = name;
        _location = location;
        _monthlySalary = salary;
        _specialty = specialty;
    }

    public Employee(EmployeeSave employeeSave)
    {
        Load(employeeSave);
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    #endregion

    #region save load
    public EmployeeSave Save()
    {
        return new EmployeeSave(_name, _location, _monthlySalary, (int)_specialty, Hired);
    }

    public void Load(EmployeeSave save)
    {
        _name = save._name;
        _location = save._location;
        _monthlySalary = save._monthlySalary;
        _specialty = (WorkerSkills)save._specialty;
        _hired = bool.Parse(save._hired);
    }
    #endregion
}
