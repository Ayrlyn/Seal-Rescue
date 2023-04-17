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
	SceneReferences _sceneReferences;
	#endregion

	#region getters and setters
	SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	void OnClick()
    {
		SceneReferences.GameEventInfoDisplay.ShowEventInfo(_eventInfo);
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
