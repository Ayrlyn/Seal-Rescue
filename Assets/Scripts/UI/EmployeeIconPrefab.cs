using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmployeeIconPrefab : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region serialiazable variables
    [SerializeField] Image _ringOuter;
    #endregion

    #region local variables
    Canvas _canvas;
    Transform _currentParent;
    Employee _employee;
    Vector3 _originalPosition;
    SceneReferences _sceneReferences;
    #endregion

    #region getters and setters
    public Canvas Canvas { get { if (_canvas == null) { _canvas = SceneReferences.Canvas; } return _canvas; } }
    public Employee Employee { get { return _employee; } }
    public SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; } { return _sceneReferences; } } }
    #endregion

    #region unity methods
    #endregion

    #region local methods
    void OnTimePassed(TimePassed time)
    {
        switch (time)
        {
            case TimePassed.Minute:
                break;
            case TimePassed.Hour:
                break;
            case TimePassed.Day:
                break;
            case TimePassed.Week:
                break;
            case TimePassed.Month:
                if (!SceneReferences.Resources.SpendMoney(Employee.MonhtlySalary)) { }
                break;
            case TimePassed.Year:
                break;
            default:
                break;
        }
    }
    #endregion

    #region public methods
    public void Init(Employee employee)
    {
        _employee = employee;
        _ringOuter.color = SceneReferences.GetSpeciastColour(employee.Specialty);
    }
    #endregion

    #region pointer and drag handler methods
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)Canvas.transform,
            eventData.position,
            Canvas.worldCamera,
            out position);

        this.transform.position = Canvas.transform.TransformPoint(position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _originalPosition = this.transform.position;
        _currentParent = this.transform.parent;
        this.transform.SetParent(Canvas.transform);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultList);

        foreach (RaycastResult result in resultList)
        {
            if(result.gameObject.GetComponent<Building>())
            {
                Building building = result.gameObject.GetComponent<Building>();
                building.ReceiveEmployee(this);
                return;
            }
        }

        this.transform.SetParent(_currentParent);
        this.transform.position = _originalPosition;
    }
    #endregion

    #region coroutines
    #endregion
}
