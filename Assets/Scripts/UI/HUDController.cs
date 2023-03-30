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

    #region unity methods
    void Awake()
    {
        _gameDateTime = GameDateTime.Instance;
        _resources = Resources.Instance;
    }

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
            $"{_gameDateTime.CurrentDay.ToString().Substring(0,3)}. " +
            $"{_gameDateTime.CurrentMonth.Value:D2} {_gameDateTime.CurrentMonth.Key.ToString().Substring(0,3)}. " +
            $"{_gameDateTime.CurrentHour:D2}:{_gameDateTime.CurrentMinute:D2} ";
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
