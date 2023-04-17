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
	#endregion

	#region override methods
	public override void OnTimePassed(TimePassed timePassed)
	{
		base.OnTimePassed(timePassed);
        switch (timePassed)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
				if(GameDateTime.CurrentHour >= 9 && GameDateTime.CurrentHour <= 17)
                {
					Task newTask = new Task(120, new List<KeyValuePair<ResourceTypes, int>>(), TaskType.Tourism);
					int randomInt = Random.Range(0, 100);
					if(!DoesTaskExist(newTask.TaskType) && randomInt < 30) 
					{ 
						_tasks.Add(newTask);
						if (_tasks.Count == 1) { _taskUI.Init(newTask); }
					}
                }
                break;
            case TimePassed.Day:
                break;
            case TimePassed.Week:
                break;
            case TimePassed.Month:
                break;
            case TimePassed.Year:
                break;
            default:
                break;
        }
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
