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
    ///Register a Method(listener) to be called when a specific event occurs.
    /// Remember to uregister it when you don't need it anymore.
    /// </summary>
    /// <typeparam name="T">The Object IEvent</typeparam>
    /// <param name="listener">The method to be called when the specified event is triggered.</param>
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
    /// <typeparam name="T">The Object IEvent</typeparam>
    /// <param name="listener">The method to stop being called when the specified event is triggered.</param>
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

    /// <summary>
    /// Triggers the Event.
    /// </summary>
    /// <typeparam name="T">The Object IEvent</typeparam>
    /// <param name="ev">An implementation of IEvent containing all neccessary information about the event.</param>
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

