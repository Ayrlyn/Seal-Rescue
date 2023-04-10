using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SealInfoPanel : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] Image _sealPortrait;
	[SerializeField] TMP_Text _sealAgeText;
    [SerializeField] TMP_Text _sealCurrentProgressText;
	[SerializeField] TMP_Text _sealHealthText;
	[SerializeField] TMP_Text _sealNameText;
	[SerializeField] TMP_Text _sealRescuedDateText;
	[SerializeField] TMP_Text _sealSpeciesText;
	[SerializeField] TMP_Text _sealWeightText;
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
	public void ShowSeal(Seal seal)
    {
		this.gameObject.SetActive(true);
		_sealAgeText.text += $"{seal.Age} months";
		_sealCurrentProgressText.text += $"{seal.RescueProgressString}";
		_sealHealthText.text += $"{seal.Health}";
		_sealNameText.text += $"{seal.Name}";
		_sealRescuedDateText.text += $"{seal.RescueDate.Key} ´{seal.RescueDate.Value}";
		_sealSpeciesText.text += $"{SceneReferences.Instance.GetDisplayName(seal.SealSpecies)}";
		_sealWeightText.text += $"{seal.Weight} kg";
    }
	#endregion

	#region coroutines
	#endregion
}
