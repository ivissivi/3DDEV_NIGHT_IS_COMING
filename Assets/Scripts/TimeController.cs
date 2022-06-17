using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float multiplier;
    [SerializeField] private float startingHour;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI nightIsComingMessage;
    [SerializeField] private TextMeshProUGUI dayCountText;
    [SerializeField] private Light sun;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private DateTime currentTime;

    [SerializeField] private Color ambientLightDay;
    [SerializeField] private Color ambientLightNight;
    [SerializeField] private AnimationCurve lightCurve;

    [SerializeField] private float maxSunIntensity;

    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonIntensity;

    private int dayCount;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startingHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    private void UpdateLight()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down); //Gives a value between -1 and 1, considering how similiar the two vector directions are.
                                                                             //If sun is pointing down, then dotProduct is 1, if sun is pointing horizontally then 0 and
                                                                             //if it points up it is -1.
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, lightCurve.Evaluate(dotProduct));                                               
        moonLight.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(ambientLightNight, ambientLightDay, lightCurve.Evaluate(dotProduct));                                                        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * multiplier);
        if(text != null)
        {
            text.text = currentTime.ToString("HH:mm");
        }
        RotateSun();
        UpdateLight();
    }

    private TimeSpan CalculateDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
            nightIsComingMessage.text = "Night is coming!";
            if(difference.TotalSeconds < 43200)
            {
                nightIsComingMessage.text = "";
            }
        }
        else if(difference.TotalSeconds > 3 * multiplier)
        {
            nightIsComingMessage.text = "";
        }

        return difference;
    }

    private void RotateSun()
    {
        float sunlightRotation;
        
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime) //Day time rotation percentage
        {
            TimeSpan duration = CalculateDifference(sunriseTime, sunsetTime);
            TimeSpan timePassed = CalculateDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timePassed.TotalMinutes / duration.TotalMinutes;
        
            sunlightRotation = Mathf.Lerp(0, 180, (float)percentage); //Sets the rotation value to 0 at sunrise and increases as the day progresses and reaches 180 at the peak time, which is sunset.

        }
        else //Night time rotation percentage
        {
            TimeSpan duration = CalculateDifference(sunsetTime, sunriseTime);
            TimeSpan timePassed = CalculateDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timePassed.TotalMinutes / duration.TotalMinutes;

            if(percentage == 0.250) 
            {
                dayCount++;
            }
            sunlightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sun.transform.rotation = Quaternion.AngleAxis(sunlightRotation, Vector3.right);
    }
}
