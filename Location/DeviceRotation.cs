using UnityEngine;

public static class DeviceRotation
{
    public static bool gyroInitialized = false;

    /// <summary>
    /// Verifies if current device has Gyro functionalities;
    /// </summary>
    public static bool HasGyroscope
    {
        get
        {
            return SystemInfo.supportsGyroscope;
        }
    }

    /// <summary>
    /// Gets the Quaternion relative to the phone rotations;
    /// </summary>
    /// <returns>A Quaternion with the phone rotations, converted to the camera axis.</returns>
    public static Quaternion Get()
    {
        if (!gyroInitialized)
        {
            InitGyro();
        }


        return HasGyroscope ? ReadGyroRotation()
                            : Quaternion.identity;
    }

    /// <summary>
    /// Starts the Gyro service in the application
    /// </summary>
    private static void InitGyro()
    {
        if (HasGyroscope)
        {
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.167f;
        }
        gyroInitialized = true;
    }

    private static Quaternion ReadGyroRotation()
    {
        return new Quaternion(.5f, .5f, -.5f, .5f) 
                * Input.gyro.attitude 
                * new Quaternion(0, 0, 1, 0);
    }

    
}
