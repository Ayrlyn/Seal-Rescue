using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : ISave<EmployeeSave>
{
    #region local variables
    Building _currentBuilding;
    string _location;
	int _monthlySalary;
	string _name;
	WorkerSkills _specialty;
    #endregion

    #region getters and setters
    public Building CurrentBuilding { get { return _currentBuilding; } set { _currentBuilding = value; } }
    public string Location { get { return _location; } set { _location = value; } }
    public int MonhtlySalary { get { return _monthlySalary; } }
    public string Name { get { return _name; } }
    public WorkerSkills Specialty { get { return _specialty; } }
    #endregion

    #region constructors
    public Employee(string name, string location, int salary, WorkerSkills specialty)
    {
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
        return new EmployeeSave(_name, _location, _monthlySalary, (int)_specialty);
    }

    public void Load(EmployeeSave save)
    {
        _name = save._name;
        _location = save._location;
        _monthlySalary = save._monthlySalary;
        _specialty = (WorkerSkills)save._specialty;
    }
    #endregion
}
