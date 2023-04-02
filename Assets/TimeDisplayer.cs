using TMPro;
using UnityEngine;

public class TimeDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _digitalClockText;
    [SerializeField] private RectTransform _hourArrow;
    [SerializeField] private RectTransform _minuteArrow;
    [SerializeField] private float _minuteOffset = +5f;
    [SerializeField] private float _hourOffset = -5f;

    private Vector3 _hourArrowRotation;
    private Vector3 _minuteArrowRotation;

    private void Start()
    {
        _hourArrowRotation = _hourArrow.eulerAngles;
        _minuteArrowRotation = _minuteArrow.eulerAngles;
    }

    public void DisplayDigitalTime(int hours, int minutes) => 
        _digitalClockText.text = $"{((hours < 10) ? ("0" + hours) : hours)}:{((minutes < 10) ? ("0" + minutes) : minutes)}";
    
    public void DisplayMechanicTime(int hours, int minutes)
    {
        if (hours > 12)
            hours -= 12;
        
        float hoursAndMinutes = hours + minutes/60.0f;
        _hourArrowRotation.z = -(hoursAndMinutes / 12.0f) * 360 + _hourOffset;
        _minuteArrowRotation.z = -(minutes / 60.0f) * 360 + _minuteOffset;
        _hourArrow.eulerAngles = _hourArrowRotation;
        _minuteArrow.eulerAngles = _minuteArrowRotation;
    }
    
    public void ChangeSecondPoints()
    {
        char[] currentTime = _digitalClockText.text.ToCharArray();
        
        if (currentTime[2] == ' ')
            currentTime[2] = ':';
        else if (currentTime[2] == ':')
            currentTime[2] = ' ';

        _digitalClockText.text = currentTime.ArrayToString();
    }
}
