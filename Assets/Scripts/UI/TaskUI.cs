using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
	#region serialiazable variables
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
        _progressBar.fillAmount = (float)_task.MinutesRemaining / (float)_task.MinutesRequired;
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
    }
	#endregion

	#region coroutines
	#endregion
}
