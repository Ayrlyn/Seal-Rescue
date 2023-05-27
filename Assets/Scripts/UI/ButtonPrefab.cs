using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefab : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] TMP_Text _buttonText;
	#endregion

	#region local variables
	Button _button;
	Image _image;
	#endregion

	#region getters and setters
	public Button Button { get { if(_button == null) { _button = this.GetComponent<Button>(); } return _button; } }
	public Image Image { get { if (_image == null) { _image = this.GetComponent<Image>(); } return _image; } }
	public TMP_Text ButtonText { get { return _buttonText; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	#endregion

	#region coroutines
	#endregion
}
