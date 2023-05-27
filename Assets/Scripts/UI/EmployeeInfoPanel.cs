using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeInfoPanel : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] Button _closeButton;
	[SerializeField] Button _dontHireButton;
	[SerializeField] TMP_Text _employeeName;
	[SerializeField] Image _employeePortrait;
	[SerializeField] TMP_Text _employeeSalary;
	[SerializeField] TMP_Text _employeeSpecialty;
	[SerializeField] Button _hireButton;
	#endregion

	#region local variables
	SceneReferences _sceneReferences;
	#endregion

	#region getters and setters
	public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void ClosePopup()
	{
		if (this.GetComponent<UIEffects>() != null) { this.GetComponent<UIEffects>().Close(); }
		else { this.gameObject.SetActive(false); }
		_hireButton.onClick.RemoveAllListeners();
	}

	public void ShowEmployee(Employee employee)
	{
		this.gameObject.SetActive(true);
		_employeeName.text = $"Name: {employee.Name}";
		_employeePortrait.sprite = employee.SpecialtySprite;
		_employeeSalary.text = $"Salary: {employee.MonhtlySalary} per week";
		_employeeSpecialty.text = $"Specialty: {employee.SpecialtyString}";
		_closeButton.gameObject.SetActive(employee.Hired);
		_dontHireButton.gameObject.SetActive(!employee.Hired);
		_hireButton.gameObject.SetActive(!employee.Hired);
		_hireButton.onClick.AddListener(() => 
		{ 
			SceneReferences.Game.HireEmployee(employee);
			ClosePopup();
		});
    }
	#endregion

	#region coroutines
	#endregion
}
