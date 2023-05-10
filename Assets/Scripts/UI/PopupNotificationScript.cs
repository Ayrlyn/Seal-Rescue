using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotificationScript : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] TMP_Text _notificationText;
	#endregion

	#region local variables
	#endregion

	#region getters and setters
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void SetNotificationText(string text)
    {
		_notificationText.text = text;
		this.gameObject.SetActive(true);
		Canvas.ForceUpdateCanvases();
		this.GetComponent<VerticalLayoutGroup>().enabled = false;
		this.GetComponent<VerticalLayoutGroup>().enabled = true;
	}
	#endregion

	#region coroutines
	#endregion
}
