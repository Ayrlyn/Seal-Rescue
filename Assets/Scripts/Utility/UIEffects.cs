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
        yield return new WaitForSeconds(uIEffect.Delay);
        float x = 0;
        float y = 0;
        float z = 0;
        switch (uIEffect.Effect)
        {
            case uiEffect.Appear:
                this.transform.localScale = Vector3.zero;
                break;
            case uiEffect.Disappear:
                this.transform.localScale = Vector3.one;
                break;
            case uiEffect.AppearFromBottom:
                _startingX = this.transform.position.x;
                _startingY = this.transform.position.y;
                _startingZ = this.transform.position.z;
                y = 0;
                this.transform.position = new Vector3(_startingX, y, _startingZ);
                break;
            case uiEffect.AppearFromTop:
                _startingX = this.transform.position.x;
                _startingY = this.transform.position.y;
                _startingZ = this.transform.position.z;
                y = Screen.height;
                this.transform.position = new Vector3(_startingX, y, _startingZ);
                break;
            case uiEffect.DisappearToBottom:
                _startingX = this.transform.position.x;
                _startingY = this.transform.position.y;
                _startingZ = this.transform.position.z;
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
                break;
            case uiEffect.DisappearToTop:
                _startingX = this.transform.position.x;
                _startingY = this.transform.position.y;
                _startingZ = this.transform.position.z;
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
                break;
            default:
                break;
        }
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
                case uiEffect.AppearFromBottom:
                    y =  easingFunction(0, _startingY, v);
                    this.transform.position = new Vector3(_startingX, y, _startingZ);
                    break;
                case uiEffect.AppearFromTop:
                    y = easingFunction(Screen.height, _startingY, v);
                    this.transform.position = new Vector3(_startingX, y, _startingZ);
                    break;
                case uiEffect.DisappearToBottom:
                    y = easingFunction(_startingY, 0, v);
                    this.transform.position = new Vector3(_startingX, y, _startingZ);
                    break;
                case uiEffect.DisappearToTop:
                    y = easingFunction(_startingY, Screen.height, v);
                    this.transform.position = new Vector3(_startingX, y, _startingZ);
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
            case uiEffect.AppearFromBottom:
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
                break;
            case uiEffect.AppearFromTop:
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
                break;
            case uiEffect.DisappearToBottom:
                this.gameObject.SetActive(false);
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
                break;
            case uiEffect.DisappearToTop:
                this.gameObject.SetActive(false);
                this.transform.position = new Vector3(_startingX, _startingY, _startingZ);
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
