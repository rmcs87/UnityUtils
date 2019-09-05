using UnityEngine;

public static class DeviceLocation
{
    public static bool locationInitialized = false;

    private const float degreesLatitudeInMeters = 111132;
    private const float degreesLongitudeInMetersAtEquator = 111319.9f;

    /// <summary>
    /// Verifies if current device has GPS functionalities;
    /// </summary>
    public static bool HasGPS
    {
        get
        {
            return Input.location.isEnabledByUser;
        }
    }

    /// <summary>
    /// Gets the coordinates of the phone;
    /// </summary>
    /// <returns>Coordinates with Latitude and Longitude. Null if has reading problems.</returns>
    public static Coords Get()
    {
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            return new Coords(-1f, -1f);
        }

        if (!locationInitialized)
        {
            InitGPS();
        }

        return HasGPS ? ReadGPS()
                            : null;
    }

    /// <summary>
    /// Starts the GPS service in the application
    /// </summary>
    private static void InitGPS()
    {
        if (HasGPS)
        {
            if (Input.location.status == LocationServiceStatus.Stopped)
            {
                Input.location.Start();
            }
            Input.compass.enabled = true;
        }
        locationInitialized = true;
    }

    private static Coords ReadGPS()
    {
        return new Coords(Input.location.lastData.latitude, Input.location.lastData.longitude,
                            Input.location.lastData.altitude, Input.location.lastData.horizontalAccuracy);
    }

    public static float GetLongitudeDegreeDistance(float latitude)
    {
        return degreesLongitudeInMetersAtEquator * Mathf.Cos(latitude * (Mathf.PI / 180));
    }

    //https://blog.anarks2.com/Geolocated-AR-In-Unity-ARFoundation/
    /// <summary>
    /// Converts a Real World coordinate into a Unity World coordinate, using a know coordinate
    /// as reference. It returns the distance between the reference and the World coordinate, in Unity units;
    /// </summary>
    /// <param name="zeroCoords">Referencce Coordinate. Make this coordinate the equivalent to Unity (0,0,0)</param>
    /// <param name="objCoords">World Coordinate to be converted into Unity coordinates.</param>
    /// <returns></returns>
    public static Vector3 GetVirtualPosition(Coords zeroCoords, Coords objCoords)
    {
        // Real world position of object. Need to update with something near your own location.
        float latitude = objCoords.Latitude;
        float longitude = objCoords.Longitude;

        // Real GPS Position - This will be the world origin.
        var gpsLat = zeroCoords.Latitude;
        var gpsLon = zeroCoords.Longitude;

        // GPS position converted into unity coordinates
        var latOffset = (latitude - gpsLat) * degreesLatitudeInMeters;
        var lonOffset = (longitude - gpsLon) * GetLongitudeDegreeDistance(latitude);

        return new Vector3(lonOffset, 0, latOffset);
    }

}