using EventManagerUtils;
using UnityEngine;

/// <summary>
/// This class inherits form EventInfo specifying new values that the event should carry. 
/// </summary>
public class Event2InfoDataExample : EventInfo
{
    Vector3 v3 = new Vector3(5.0f,6.0f,7.1f);
    /// <summary>
    /// The Class Constructor
    /// </summary>
    /// <param name="eventName">This is the event name from EventNames. Add a new one if necessary</param>
    public Event2InfoDataExample(EventNames eventName) : base(eventName)
    {
    }
    
    /// <summary>An example of value to be passed, like the position of the event in 3D world.</summary>    
    public Vector3 V3 { get => v3; set => v3 = value; }
}
