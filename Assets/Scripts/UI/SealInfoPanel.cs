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
	[SerializeField] TMP_Text _titleText;
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
	public void ShowSeal(Seal seal, bool newSeal = false)
    {
		this.gameObject.SetActive(true);
		_sealAgeText.text = $"Age: {seal.Age} months";
		_sealCurrentProgressText.text = $"Progress: {seal.RescueProgressString}";
		_sealHealthText.text = $"Health: {seal.Health}";
		_sealNameText.text = $"Name: {seal.Name}";
		_sealRescuedDateText.text = $"Rescued: {seal.RescueDate.Key} {seal.RescueDate.Value}";
		_sealSpeciesText.text = $"Species: {SceneReferences.Instance.GetDisplayName(seal.SealSpecies)}";
		_sealWeightText.text = $"Weight: {seal.Weight:F1} kg";
		_titleText.text = newSeal? "New Seal" : $"{seal.CurrentLocationString}";
    }
	#endregion

	#region coroutines
	#endregion
}
