using EventManagerUtils;

/// <summary>
/// This class inherits form EventInfo specifying new values that the event should carry. 
/// </summary>
public class Event1InfoDataExample : EventInfo
{    
    /// <summary>
    /// The Class Constructor
    /// </summary>
    /// <param name="eventName">This is the event name from EventNames. Add a new one if necessary</param>
    public Event1InfoDataExample(EventNames eventName) : base(eventName)
    {
    }

    int data = 10;
    /// <summary>An example of value to be passed, like the position of the event in 3D world.</summary>    
    public int Data { get => data; set => data = value; }
}
