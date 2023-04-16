using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EmployeeSave
{
	public string _name;
	public string _location;
	public int _monthlySalary;
	public int _specialty;

	public EmployeeSave(string name, string location, int salary, int specialty)
    {
		_name = name;
		_location = location;
		_monthlySalary = salary;
		_specialty = specialty;
    }
}
