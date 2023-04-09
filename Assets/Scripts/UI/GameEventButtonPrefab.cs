using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventButtonPrefab : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] TMP_Text _buttonText;
	#endregion

	#region local variables
	GameEventInfo _eventInfo;
	GameEventInfoDisplay _gameEventInfoDisplay;
	#endregion

	#region getters and setters
	GameEventInfoDisplay GameEventInfoDisplay { get { if (_gameEventInfoDisplay == null) { _gameEventInfoDisplay = GameEventInfoDisplay.Instance; } return _gameEventInfoDisplay; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	void OnClick()
    {
		GameEventInfoDisplay.ShowEventInfo(_eventInfo);
    }
	#endregion

	#region public methods
	public void Init(GameEventInfo eventInfo)
    {
		_eventInfo = eventInfo;
		_buttonText.text = eventInfo.EventName;
		this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }
	#endregion

	#region coroutines
	#endregion
}
