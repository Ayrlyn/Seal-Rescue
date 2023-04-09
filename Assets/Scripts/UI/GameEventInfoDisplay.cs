using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEventInfoDisplay : Singleton<GameEventInfoDisplay>
{
	#region serialiazable variables
	[SerializeField] TMP_Text _descriptionText;
	[SerializeField] TMP_Text _titleText;
    #endregion

    #region local variables
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
    #endregion

    #region public methods
    public void ShowEventInfo(GameEventInfo eventInfo)
    {
		this.gameObject.SetActive(!this.gameObject.activeSelf);
        _titleText.text = eventInfo.EventName;
        _descriptionText.text = eventInfo.EventDescription;
    }
	#endregion

	#region coroutines
	#endregion
}
