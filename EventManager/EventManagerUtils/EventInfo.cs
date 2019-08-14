using System;

namespace EventManagerUtils
{
    /// <summary>
    /// This class contains default information for event. 
    /// It should be used in case you don't want to pass any specific value
    /// within the event.
    /// </summary>
    public class EventInfo
    {
        /// <summary>The timestamp to mark when this event was created.</summary>
        readonly DateTime timeStamp;
        EventNames _eventName;
        /// <summary>The EventName property represents the event's name.</summary>
        /// <value>The Name property gets/sets the value of the EventNames field, _eventName.</value>
        public EventNames EventName { get => _eventName; set => _eventName = value; }

        /// <summary>
        /// The Class Constructor
        /// </summary>
        /// <param name="eventName">This is the event name from EventNames. Add a new one if necessary</param>
        public EventInfo(EventNames eventName)
        {
            timeStamp = DateTime.Now;
            this.EventName = eventName;
        }        
    }
}
