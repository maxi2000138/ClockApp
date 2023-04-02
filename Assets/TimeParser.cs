using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TimeParser : MonoBehaviour
{
    private const string webApi = "https://ipgeolocation.abstractapi.com/v1/?api_key=d43650076edd4d8ba80502b410c9dc1d&fields=timezone";

    public event Action OnTimeParse;
    public int Hours = 0;
    public int Minutes = 0;
    public int Seconds = 0;

    private void Start()
    {
        StartCoroutine(makeRequest());
    }


    IEnumerator makeRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(webApi);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Root timezone = JsonUtility.FromJson<Root>(webRequest.downloadHandler.text);
            string time = timezone.timezone.current_time;
            ToInts(time);
        }
    }

    void ToInts(string st)
    {
        int hours = Int32.Parse(st.Substring(0, 2));
        int minutes = Int32.Parse(st.Substring(3, 2));
        int seconds = Int32.Parse(st.Substring(6, 2));
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        OnTimeParse?.Invoke();
    }
    
    
}

public class Root
{
    public Timezone timezone;
}