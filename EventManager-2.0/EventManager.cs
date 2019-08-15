using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// This Singleton is responsible for managing all Events,
/// registering and triggering them.
/// </summary>
public class EventManager : MonoBehaviour
{
    //Dictinoary to register Events and Listeners
    private Dictionary<Type, Action<IEvent>> eventDictionary;

    //Singleton Instance;
    public static EventManager instance = null;

    //On Awake, the singleton is started (if not already) and
    //regitered do be kept alive among scenes
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first state 
        Init();
    }

    //Starts the necessary resources, in this casse, just the Dictionary
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<Type, Action<IEvent>>();
        }
        
    }
    /// <summary>
    /// Register a Method (listener) to be called when a specific event occurs. 
    /// Remember to uregister it when you don't need it anymore.
    /// </summary>
    /// <param name="eventName">This is the event name from EventNames. Add a new one if necessary.</param>
    /// <param name="listener">The method to be called when the specified event is triggered.</param>
    ///<remarks>Add a EventName and create a new EventInfo (or something that inherits form it) if necessary.</remarks>
    public static void StartListening<T>(Action<IEvent> listener) where T : IEvent
    {
        Action<IEvent> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(typeof(T), out thisEvent))
        {
            thisEvent += listener;
            //Update the Dictionary
            instance.eventDictionary[typeof(T)] = thisEvent;
        }
        else
        {
            thisEvent += listener;            
            instance.eventDictionary.Add(typeof(T), thisEvent);
        }
    }
    /// <summary>
    /// Unregister a Method (listener) from being called when a specific event occurs.
    /// </summary>
    /// <param name="eventName">This is the event name from EventNames.</param>
    /// <param name="listener">The method to stop calling when the specified event is triggered.</param>
    public static void StopListening<T>(Action<IEvent> listener) where T : IEvent
    {
        if (instance == null) return;
        Action<IEvent> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(typeof(T), out thisEvent))
        {
            thisEvent -= listener;
            //Update the Dictionary
            instance.eventDictionary[typeof(T)] = thisEvent;
        }
    }

    //public static void TriggerEvent(EventNames eventName, EventInfo eventParam = default)
    /// <summary>
    /// Triggers the Event.
    /// </summary>
    /// <param name="ei">An intance of EventInfo containing all neccessary information about the event.
    /// Extend the EventInfo class to personalize an EventInfo for each event tha occurs.</param>
    public static void TriggerEvent<T>(T ev) where T : IEvent
    {
        Action<IEvent> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(typeof(T), out thisEvent))
        {
            if(thisEvent != null)
                thisEvent.Invoke(ev);
        }
    }
}

