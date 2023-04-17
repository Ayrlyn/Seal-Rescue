using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : Singleton<HUDController>
{
    #region serializable variables
    [SerializeField] GameObject _costsLayoutGroup;
    [SerializeField] TMP_Text _gameDateTimeText;
    [Header("Resources")]
    [SerializeField] TMP_Text _foodText;
    [SerializeField] TMP_Text _materialsText;
    [SerializeField] TMP_Text _medicineText;
    [SerializeField] TMP_Text _moneyText;
    [SerializeField] TMP_Text _upkeepText;
    #endregion

    #region local variables
    GameDateTime _gameDateTime;
    Resources _resources;
    SceneReferences _sceneReferences;
    #endregion

    #region getters and setters
    GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    #endregion

    #region unity methods
    void Update()
    {
        UpdateGameDateTimeText();
        UpdateResourcesText();
    }
    #endregion

    #region local methods
    void UpdateGameDateTimeText()
    {
        _gameDateTimeText.text = 
            $"{GameDateTime.CurrentDay.ToString().Substring(0,3)}. " +
            $"{GameDateTime.CurrentMonth.Value:D2} {GameDateTime.CurrentMonth.Key.ToString().Substring(0,3)}. " +
            $"{GameDateTime.CurrentHour:D2}:{GameDateTime.CurrentMinute:D2} ";
    }

    void UpdateResourcesText()
    {
        _foodText.text = $"{Resources.Food:D4}";
        _materialsText.text = $"{Resources.Materials:D4}";
        _medicineText.text = $"{Resources.Medicine:D4}";
        _moneyText.text = $"{Resources.Money:D4}";
    }
    #endregion

    #region public methods
    public void OnClickMonthlyCosts()
    {
        _costsLayoutGroup.SetActive(!_costsLayoutGroup.activeSelf);

        if (!_costsLayoutGroup.activeSelf) { return; }

        string costs = "";
        foreach (UpkeepData upkeepData in SceneReferences.UpkeepController.AllUpkeeps)
        {
            switch (upkeepData.ResourceType)
            {
                case ResourceTypes.Food:
                    costs += $"{upkeepData.Quantity} Fish each {upkeepData.Frequency}\n";
                    break;
                case ResourceTypes.Materials:
                    costs += $"{upkeepData.Quantity} General Materials each {upkeepData.Frequency}\n";
                    break;
                case ResourceTypes.Medicine:
                    costs += $"{upkeepData.Quantity} Medicines each {upkeepData.Frequency}\n";
                    break;
                case ResourceTypes.Money:
                    costs += $"${upkeepData.Quantity} each {upkeepData.Frequency}\n";
                    break;
            }
        }
        int totalSalaries = 0;
        foreach (Employee employee in SceneReferences.Game.Employees)
        {
            totalSalaries += employee.MonhtlySalary;
        }
        costs += $"Monthly salaries: ${totalSalaries}";
        _upkeepText.text = costs;
    }
    #endregion
}
