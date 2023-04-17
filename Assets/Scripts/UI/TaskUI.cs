using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    #region serialiazable variables
    [SerializeField] GameObject _infoPopup;
    [SerializeField] TMP_Text _popupResources;
    [SerializeField] TMP_Text _popupTimeRemaining;
    [SerializeField] TMP_Text _popupTitle;
	[SerializeField] Image _progressBar;
	[SerializeField] Image _taskIcon;
    #endregion

    #region local variables
    Task _task;
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    void Update()
    {
        if(_task != null) { _progressBar.fillAmount = (float)_task.MinutesRemaining / (float)_task.MinutesRequired; }
        _popupTimeRemaining.text = $"Time Remaining: {(int)_task.MinutesRemaining}";
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    public void Init(Task task)
    {
		this.gameObject.SetActive(true);
		_progressBar.fillAmount = 1f;
        _task = task;
        if(_task.Seal != null) { _taskIcon.sprite = _task.Seal.IconPup; }
    }

    public void OnClick()
    {
        _infoPopup.SetActive(!_infoPopup.activeSelf);
        if (!_infoPopup.activeSelf) { return; }

        _popupTitle.text = _task.TaskType.ToString();

        if(_task.ResourcesRequired.Count == 0) 
        {
            _popupResources.gameObject.SetActive(false);
            return; 
        }

        string resourcesString = "Resources:";
        foreach (KeyValuePair<ResourceTypes, int> resourceRequired in _task.ResourcesRequired)
        {
            resourcesString += $"\n - {resourceRequired.Value} {resourceRequired.Key.ToString()}";
        }
        _popupResources.gameObject.SetActive(true);
        _popupResources.text = resourcesString;
    }
	#endregion

	#region coroutines
	#endregion
}
