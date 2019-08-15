using UnityEngine;

/// <summary>
/// This class inherits form EventInfo specifying new values that the event should carry. 
/// </summary>
public class GPSEvent2 : IEvent
{    
    /// <summary>
    /// The Class Constructor
    /// </summary>   
    public GPSEvent2(float lat, float lon)
    {
        coords = new Vector2 ( lat, lon );
    }

    Vector2 coords;
    /// <summary>{Latitude, Longitude}</summary>    
    public Vector2 Coords { get => coords; }    

}
