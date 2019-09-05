//https://answers.unity.com/questions/1361740/trouble-enablingreading-compass-input-on-android.html
using System.Collections.Generic;
using UnityEngine;

public static class DeviceCompass
{
    public static bool compassInitialized = false;

    /// <summary>
    /// Verifies if current device has compass functionalities;
    /// </summary>
    public static bool HasCompass
    {
        get
        {
            return Input.location.isEnabledByUser;
        }
    }

    /// <summary>
    /// Gets the current angle offset to north;
    /// </summary>
    /// <returns>Float with the angle, or 0 as default</returns>
    public static float Get()
    {
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            return -1f;
        }

        if (!compassInitialized)
        {
            InitGyro();
        }

        return HasCompass ? ReadCompassAngle()
                            : 0;
    }

    /// <summary>
    /// Starts the compass service in the application
    /// </summary>
    private static void InitGyro()
    {
        if (HasCompass)
        {
            if(Input.location.status == LocationServiceStatus.Stopped)
            { 
                Input.location.Start();
            }
            Input.compass.enabled = true;
        }
        compassInitialized = true;
    }

    private static float ReadCompassAngle()
    {
        return Input.compass.trueHeading;
    }

    //https://rosettacode.org/wiki/Averages/Mean_angle
    /// <summary>
    /// Smooths the readings from compass, calculating the median of the last readings;
    /// </summary>
    /// <param name="lastAngles">A list with the last readings</param>
    /// <returns>A flaot the median angle</returns>
    public static float GetMedianAngle(List<float> lastAngles)
    {
        float sinSum = 0;
        float cosSum = 0;
        int count = lastAngles.Count;
        foreach (float angle in lastAngles)
        {
            sinSum += Mathf.Sin(angle * Mathf.Deg2Rad);
            cosSum += Mathf.Cos(angle * Mathf.Deg2Rad);
        }
        return Mathf.Atan2(sinSum / count, cosSum / count) * Mathf.Rad2Deg;
    }
}