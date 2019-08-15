using UnityEngine;

/// <summary>
/// This class implements IEvent specifying new values that the event should carry. 
/// </summary>
public class GPSEvent : IEvent
{    
    /// <summary>
    /// The Class Constructor
    /// </summary>   
    public GPSEvent(float lat, float lon)
    {
        coords = new Vector2 ( lat, lon );
    }

    Vector2 coords;
    /// <summary>{Latitude, Longitude}</summary>    
    public Vector2 Coords { get => coords; }    

}
