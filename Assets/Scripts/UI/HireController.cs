using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HireController : Singleton<HireController>
{
    #region serialiazable variables
    [SerializeField] TMP_Text _applicationCounter;
    [SerializeField] ButtonPrefab _buttonPrefab;
	[SerializeField] Transform _layoutGroup;
    #endregion

    #region local variables
    List<ButtonPrefab> _buttonPrefabs = new List<ButtonPrefab>();
    Dictionary<Employee, ButtonPrefab> _employeesAndPrefabs = new Dictionary<Employee, ButtonPrefab>();
    List<Employee> _potentialEmployees = new List<Employee>();
	SceneReferences _sceneReferences;
	#endregion

	#region getters and setters
    public List<Employee> PotentialEmployees { get { return _potentialEmployees; } set { _potentialEmployees = value; } }
	public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    #endregion

    #region unity methods
    void Start()
    {
        FillAllSpecialties();
    }

    void Update()
    {
        if(_potentialEmployees.IsEmpty()) { _layoutGroup.gameObject.SetActive(false); }
        _applicationCounter.text = _potentialEmployees.Count.ToString();
    }
    #endregion

    #region local methods
    void CreatePotentialEmployee(WorkerSkills skill)
    {
        Employee employee = new Employee(
            SceneReferences.EmployeeNames.GetRandomElement(),
            "VisitorCentre",
            50,
            skill,
            false);
        _potentialEmployees.Add(employee);
    }

    void FillAllSpecialties()
    {
        bool communityWorker = false;
        bool medicineWorker = false;
        bool managementWorker = false;
        bool handyWorker = false;
        foreach (Employee potentialEmployee in _potentialEmployees)
        {
            switch (potentialEmployee.Specialty)
            {
                case WorkerSkills.Community:
                    communityWorker = true;
                    break;
                case WorkerSkills.Medicine:
                    medicineWorker = true;
                    break;
                case WorkerSkills.Management:
                    managementWorker = true;
                    break;
                case WorkerSkills.Handy:
                    handyWorker = true;
                    break;
                default:
                    Debug.LogError($"Invalid employee specialty: {potentialEmployee.Specialty}");
                    break;
            }
        }
        if (!communityWorker) { CreatePotentialEmployee(WorkerSkills.Community); }
        if (!medicineWorker) { CreatePotentialEmployee(WorkerSkills.Medicine); }
        if (!managementWorker) { CreatePotentialEmployee(WorkerSkills.Management); }
        if (!handyWorker) { CreatePotentialEmployee(WorkerSkills.Handy); }
    }
    #endregion

    #region public methods
    public void ClickHireButton()
    {
        bool buttonsActive = _layoutGroup.gameObject.activeSelf;
        _layoutGroup.gameObject.SetActive(!buttonsActive);

        if (!buttonsActive)
        {
            foreach (Employee employee in _potentialEmployees)
            {
                ButtonPrefab hireButton = Instantiate(_buttonPrefab, _layoutGroup);
                hireButton.ButtonText.text = employee.Name;
                _buttonPrefabs.Add(hireButton);
                _employeesAndPrefabs.Add(employee, hireButton);
                hireButton.Button.onClick.AddListener(() =>
                {
                    SceneReferences.EmployeeInfoPanel.ShowEmployee(employee);
                    ClickHireButton();
                });
                hireButton.Image.color = SceneReferences.GetSpeciastColour(employee.Specialty);
            }
        }
        else
        {
            foreach (ButtonPrefab gameEventButtonPrefab in _buttonPrefabs)
            {
                Destroy(gameEventButtonPrefab.gameObject);
            }
            _buttonPrefabs.Clear();
            _employeesAndPrefabs.Clear();
        }
    }
    #endregion

    #region coroutines
    #endregion
}
