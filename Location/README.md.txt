# Location Scripts

This repository contains three scripts used to handle Location Services in Unity.

## DeviceCompass

Adapted from:
https://answers.unity.com/questions/1361740/trouble-enablingreading-compass-input-on-android.html

It handles the functionalities to get the heading in degrees relative to the geographic North Pole. The value is always measured relative to the top of the screen in its current orientation. 

You shouldn't use the direct value read, but take the median of the last measures using the **GetMedianAngle** method.
```c#
//Use example
        float angle = DeviceCompass.Get();

        if (lastAngles.Count > 50)
        {
            lastAngles.RemoveAt(0);
        }
        lastAngles.Add(angle);
        Float NorthAngle =   	DeviceCompass.GetMedianAngle(lastAngles); 
```

## DeviceLocation

It handles the GPS sensor, getting the current coordinates of the phone.

Using **GetVirtualPosition** you can convert real world coordinates into Unity Coordinates.
```c#
//Use example
		Coords latlong = DeviceLocation.Get();
        if (latlong != null)
        {
            Vector3 virtualPosition = DeviceLocation.GetVirtualPosition(ZeroCoordinate
                                                                        , latlong);
            virtualPosition.y = transform.position.y;
            transform.position = virtualPosition;
        }
```

## DeviceRotation

It handles the Gyro functionalities of the phone. It returns the Quaternion relative to current phone orentation.

In the following example, the script aplied to a Camera will move the camera in the Virtual World according to the phone rotations in the Real World.

```c#
//Use example
		Quaternion deviceRotation = DeviceRotation.Get();
        transform.rotation = deviceRotation;
```


