using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Easing;

public enum uiEffect
{
	Appear,
	Disappear,
    AppearFromBottom,
    AppearFromTop,
    DisappearToBottom,
    DisappearToTop
}
public class UIEffects : MonoBehaviour
{
    #region serialiazable variables
    [SerializeField] List<UIEffect> _onOpenEffects;
    [SerializeField] List<UIEffect> _onCloseEffects;
    #endregion

    #region local variables
    float _startingX;
    float _startingY;
    float _startingZ;
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    void OnEnable()
    {
        foreach (UIEffect uIEffect in _onOpenEffects)
        {
            switch (uIEffect.Effect)
            {
                case uiEffect.Appear:
                    StartCoroutine(AppearRoutine(uIEffect));
                    break;
                case uiEffect.Disappear:
                    StartCoroutine(DisappearRoutine(uIEffect));
                    break;
                case uiEffect.AppearFromBottom:
                    StartCoroutine(AppearFromBottomRoutine(uIEffect));
                    break;
                case uiEffect.DisappearToBottom:
                    StartCoroutine(DisappearToBottomRoutine(uIEffect));
                    break;
                default:
                    Debug.LogError($"On Enable UI effect: {uIEffect} has not yet been implemented");
                    break;
            }
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
            if (this.gameObject.activeSelf) 
            {
                switch (uIEffect.Effect)
                {
                    case uiEffect.Disappear:
                        StartCoroutine(DisappearRoutine(uIEffect));
                        break;
                    case uiEffect.DisappearToBottom:
                        StartCoroutine(DisappearToBottomRoutine(uIEffect));
                        break;
                    default:
                        Debug.LogError($"On Close UI effect: {uIEffect.ToString()} has not yet been implemented");
                        break;
                }
            }
        }
    }
    #endregion

    #region coroutines
    IEnumerator AppearRoutine(UIEffect appearEffect)
    {
        this.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(appearEffect.Delay);
        float v = 0;
        Function easingFunction = GetEasingFunction(appearEffect.EaseType);
        while (v < appearEffect.Duration)
        {
            this.transform.localScale = Vector3.one * easingFunction(0, 1, v);
            v += Time.deltaTime;
            yield return null;
        }
        this.transform.localScale = Vector3.one;
    }

    IEnumerator DisappearRoutine(UIEffect disappearEffect)
    {
        yield return new WaitForSeconds(disappearEffect.Delay);
        this.transform.localScale = Vector3.one;
        float v = 0;
        Function easingFunction = GetEasingFunction(disappearEffect.EaseType);
        while (v < disappearEffect.Duration)
        {
            this.transform.localScale = Vector3.one * easingFunction(1, 0, v);
            v += Time.deltaTime;
            yield return null;
        }
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    IEnumerator AppearFromBottomRoutine(UIEffect appearEffect)
    {
        _startingX = this.transform.position.x;
        _startingY = this.transform.position.y;
        _startingZ = this.transform.position.z;
        float y = 0;
        this.transform.position = new Vector3(_startingX, y, _startingZ);
        yield return new WaitForSeconds(appearEffect.Delay);

        float v = 0;
        Function easingFunction = GetEasingFunction(appearEffect.EaseType);
        while (v < appearEffect.Duration)
        {
            y = easingFunction(0, _startingY, v);
            this.transform.position = new Vector3(_startingX, y, _startingZ);
            v += Time.deltaTime;
            yield return null;
        }
        this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
    }

    IEnumerator DisappearToBottomRoutine(UIEffect disappearEffect)
    {
        yield return new WaitForSeconds(disappearEffect.Delay);
        _startingX = this.transform.position.x;
        _startingY = this.transform.position.y;
        _startingZ = this.transform.position.z;
        float y = 0;
        this.transform.position = new Vector3(_startingX, _startingY, _startingZ);

        float v = 0;
        Function easingFunction = GetEasingFunction(disappearEffect.EaseType);
        while (v < disappearEffect.Duration)
        {
            y = easingFunction(_startingY, 0, v);
            this.transform.position = new Vector3(_startingX, y, _startingZ);
            v += Time.deltaTime;
            yield return null;
        }
        this.transform.position = new Vector3(_startingX, 0, _startingZ);
        this.gameObject.SetActive(false);
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
