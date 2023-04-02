using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlarmSetter : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _hourArrow;
    [SerializeField] private GameObject _minuteArrow;

    private int _alarmHour;
    private int _alarmMinute;

    private float sum = 0;
    private Vector3 _lastVector = Vector3.zero;

    private GameObject _currentTargetObject;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _currentTargetObject = eventData.pointerCurrentRaycast.gameObject;
        if (_currentTargetObject != _hourArrow && _currentTargetObject != _minuteArrow)
            _currentTargetObject = null;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 eventDataPosition = eventData.position;
        Vector3 dataPosition = (eventDataPosition - _currentTargetObject.transform.position).normalized;

        Vector3 angles = Quaternion.FromToRotation(Vector3.up, dataPosition).eulerAngles;
        _currentTargetObject.transform.eulerAngles = angles;
        float curAngle = angles.z;

        float deltaAngle = Quaternion.FromToRotation(_lastVector, dataPosition).eulerAngles.z;
        
        if (deltaAngle >= 180f)
            deltaAngle -= 360f;

        
        MoveHourArrow(deltaAngle/12f);
        _lastVector = dataPosition;
    }
    
    void MoveHourArrow(float delta)
    {
        Vector3 eulerAngles = _hourArrow.transform.eulerAngles;
        eulerAngles.z += delta;
        _hourArrow.transform.eulerAngles = eulerAngles;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _alarmHour = (int)_hourArrow.transform.eulerAngles.z;
        _alarmMinute = (int)_minuteArrow.transform.eulerAngles.z;
        Debug.Log(_alarmHour + "  " + _alarmMinute);
    }
}
