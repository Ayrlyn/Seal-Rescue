using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventInfoDisplay : Singleton<GameEventInfoDisplay>
{
    #region serialiazable variables
    [SerializeField] Button _buttonAccept;
    [SerializeField] Button _buttonDecline;
	[SerializeField] TMP_Text _descriptionText;
    [SerializeField] SealInfoPanel _sealInfoPanel;
	[SerializeField] TMP_Text _titleText;
    #endregion

    #region local variables
    GameEventInfo _eventInfo;
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region local methods
    void AcceptButtonConfiguration()
    {
        switch (_eventInfo.GameEventType)
        {
            case GameEventType.SealSpotted:
                _buttonAccept.GetComponentInChildren<TMP_Text>().text = "Rescue!";
                _buttonAccept.interactable = SealHospital.Instance.HasSpaceForSeal;
                _buttonAccept.onClick.AddListener(() => RescueSeal(_eventInfo.Seal));
                break;
        }
    }

    void RescueSeal(Seal seal)
    {
        this.gameObject.SetActive(false);
        seal.RescueMe();
        GameEventsUI.Instance.RemoveSealEvent(seal);
        _sealInfoPanel.ShowSeal(seal);
    }
    #endregion

    #region public methods
    public void ShowEventInfo(GameEventInfo eventInfo)
    {
		this.gameObject.SetActive(!this.gameObject.activeSelf);
        _descriptionText.text = eventInfo.EventDescription;
        _titleText.text = eventInfo.EventName;
        _eventInfo = eventInfo;
        AcceptButtonConfiguration();
    }
	#endregion

	#region coroutines
	#endregion
}
