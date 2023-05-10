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
    SceneReferences _sceneReferences;
    Task _task;
    #endregion

    #region getters and setters
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } return _sceneReferences; } }
    #endregion

    #region unity methods
    void Update()
    {
        if(_task != null) 
        { 
            _progressBar.fillAmount = (float)_task.MinutesRemaining / (float)_task.MinutesRequired;
            _popupTimeRemaining.text = $"Time Remaining: {(int)_task.MinutesRemaining}";
        }
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    public void Init(Task task)
    {
		this.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
        _infoPopup.GetComponent<VerticalLayoutGroup>().enabled = false;
        _infoPopup.GetComponent<VerticalLayoutGroup>().enabled = true;
        _progressBar.fillAmount = 1f;
        _task = task;
        if(_task.Seal != null) { _taskIcon.sprite = _task.Seal.IconAdult; }
        else
        {
            switch (task.TaskType)
            {
                case TaskType.Clean:
                    _taskIcon.sprite = SceneReferences.MaterialsSprite;
                    break;
                case TaskType.Maintenance:
                    _taskIcon.sprite = SceneReferences.MaterialsSprite;
                    break;
                case TaskType.Tourism:
                    _taskIcon.sprite = SceneReferences.MoneySprite;
                    break;
            }
        }
    }

    public void OnClick()
    {
        if (_infoPopup.activeSelf)
        {
            if (_infoPopup.GetComponent<UIEffects>() != null) { _infoPopup.GetComponent<UIEffects>().Close(); }
            else { _infoPopup.SetActive(false); }
        }
        else { { _infoPopup.SetActive(true); } }

        if (!_infoPopup.activeSelf) { return; }

        _popupTitle.text = _task.TaskTypeString;

        if(_task.ResourcesRequired.Count == 0) 
        {
            _popupResources.gameObject.SetActive(false);
            return; 
        }

        string resourcesString = "Requirements:";
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
