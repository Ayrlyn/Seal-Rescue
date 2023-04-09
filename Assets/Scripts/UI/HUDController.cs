using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : Singleton<HUDController>
{
    #region serializable variables
    [SerializeField] TMP_Text _gameDateTimeText;
    [Header("Resources")]
    [SerializeField] TMP_Text _foodText;
    [SerializeField] TMP_Text _materialsText;
    [SerializeField] TMP_Text _medicineText;
    [SerializeField] TMP_Text _moneyText;
    #endregion

    #region local variables
    GameDateTime _gameDateTime;
    Resources _resources;
    #endregion

    #region getters and setters
    GameDateTime GameDateTime { get { if (_gameDateTime == null) { _gameDateTime = GameDateTime.Instance; } return _gameDateTime; } }
    Resources Resources { get { if (_resources == null) { _resources = Resources.Instance; } return _resources; } }
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
        _foodText.text = $"{_resources.Food:D4}";
        _materialsText.text = $"{_resources.Materials:D4}";
        _medicineText.text = $"{_resources.Medicine:D4}";
        _moneyText.text = $"{_resources.Money:D4}";
    }
    #endregion

    #region public methods

    #endregion
}
