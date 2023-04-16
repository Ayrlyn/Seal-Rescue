using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorCentre : Building
{
	#region serialiazable variables
	#endregion

	#region local variables
	#endregion

	#region getters and setters
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void Init()
	{
		EventMessenger.Instance.OnTimeAndDateChange += OnTimePassed;
		_prefabName = "VisitorCentre";
	}

	public override void ReceiveEmployee(EmployeeIconPrefab employeeIconPrefab)
	{
		base.ReceiveEmployee(employeeIconPrefab);
		employeeIconPrefab.Employee.Location = "SealHospital";
	}
	#endregion

	#region coroutines
	#endregion
}
