using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	#region public variables
	public Transform _employeeParent;
	public List<Employee> _employees = new List<Employee>();
	public string _prefabName;
	public TaskUI _taskUI;
	public List<Task> _tasks = new List<Task>();
	#endregion

	#region local variables
	Game _game;
    #endregion

    #region getters and setters
    public Task CurrentTask
	{
		get
		{
			if (_tasks.IsEmpty()) { return null; }
			return _tasks[0];
		}
        set
		{
			if (_tasks.IsEmpty()) { return; }
			_tasks[0] = value;
        }
	}
	public Transform EmployeeParent { get { return _employeeParent; } }
	public List<Employee> Employees { get { return _employees; } }
	public Game Game { get { if (_game == null) { _game = Game.Instance; } return _game; } }
	#endregion

	#region unity methods
	void Update()
	{
		_taskUI.gameObject.SetActive(CurrentTask != null);
	}

	void OnDestroy()
	{
		try { EventMessenger.Instance.OnTimeAndDateChange -= OnTimePassed; }
		catch { }
	}
	#endregion

	#region local methods
	void DoTask()
    {
		if(CurrentTask == null || Employees.IsEmpty()) { return; }
		if (Game.Resources.HasRescources(CurrentTask.ResourcesRequired))
        {
			CurrentTask.MinutesRemaining--;
			if(_employees.Count > 1 && CurrentTask.BonusProgress(_employees)) { CurrentTask.MinutesRemaining -= 0.75f; }
			else if (_employees.Count > 1 ) { CurrentTask.MinutesRemaining -= 0.4f; }
			else if (CurrentTask.BonusProgress(_employees)) { CurrentTask.MinutesRemaining -= 0.5f; }

			if (CurrentTask.MinutesRemaining <= 0) 
			{
				CurrentTask.CompleteTask();
				_tasks.Remove(CurrentTask);
				if(CurrentTask != null)
                {
					_taskUI.Init(CurrentTask);
                }
                else
                {
					CheckTasks();
                }
			}
        }
    }
	#endregion

	#region public methods
	public virtual void CheckTasks()
    {

    }
	public virtual bool HasSpaceForSeal() { return false; }
	public virtual void OnTimePassed(TimePassed timePassed) 
	{
		if(timePassed == TimePassed.Minute) { DoTask(); }
		
	}

	public virtual void ReceiveEmployee(EmployeeIconPrefab employeeIconPrefab)
    {
		employeeIconPrefab.transform.SetParent(_employeeParent);
		employeeIconPrefab.transform.localPosition = Vector3.zero;

		Employee employee = employeeIconPrefab.Employee;
		if(employee.CurrentBuilding != null)
		{
			employee.CurrentBuilding.RemoveEmployee(employee);
		}
		_employees.Add(employee);
		employee.CurrentBuilding = this;
    }

	public void RemoveEmployee(Employee employee)
    {
		if (employee == null) { return; }
		_employees.Remove(employee);
    }
	#endregion

	#region coroutines
	#endregion
}
