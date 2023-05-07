using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Easing;

public enum uiEffect
{
	Appear,
	Disappear
}
public class UIEffects : MonoBehaviour
{
    #region serialiazable variables
    [SerializeField] List<UIEffect> _onOpenEffects;
    [SerializeField] List<UIEffect> _onCloseEffects;
    #endregion

    #region local variables
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    void OnEnable()
    {
        foreach (UIEffect uIEffect in _onOpenEffects)
        {
            StartCoroutine(UIEffectRoutine(uIEffect));
        }
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    public void Close()
    {
        foreach (UIEffect uIEffect in _onCloseEffects)
        {
            StartCoroutine(UIEffectRoutine(uIEffect));
        }
    }
    #endregion

    #region coroutines
    IEnumerator UIEffectRoutine(UIEffect uIEffect)
    {
        switch (uIEffect.Effect)
        {
            case uiEffect.Appear:
                this.transform.localScale = Vector3.zero;
                break;
            case uiEffect.Disappear:
                this.transform.localScale = Vector3.one;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(uIEffect.Delay);
        float v = 0;
        Function easingFunction = GetEasingFunction(uIEffect.EaseType);
        while (v < uIEffect.Duration)
        {
            switch (uIEffect.Effect)
            {
                case uiEffect.Appear:
                    this.transform.localScale = Vector3.one * easingFunction(0, 1, v);
                    break;
                case uiEffect.Disappear:
                    this.transform.localScale = Vector3.one * easingFunction(1, 0, v);
                    break;
                default:
                    break;
            }
            v += Time.deltaTime;
            yield return null;
        }
        switch (uIEffect.Effect)
        {
            case uiEffect.Appear:
                this.transform.localScale = Vector3.one;
                break;
            case uiEffect.Disappear:
                this.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion
}

[Serializable]
public class UIEffect
{
	#region serialiazable variables
	[SerializeField] uiEffect _uIEffect;
	[SerializeField] Ease _easeType;
	[SerializeField] float _duration;
	[SerializeField] float _delay;
	#endregion

	#region getters and setters
	public uiEffect Effect { get { return _uIEffect; } }
	public Ease EaseType { get { return _easeType; } }
	public float Duration { get { return _duration; } }
	public float Delay { get { return _delay; } }
	#endregion
}
