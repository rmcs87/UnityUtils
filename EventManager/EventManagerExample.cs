using UnityEngine;
using EventManagerUtils;

/// <summary>
/// Simple example for Using the EventManager
/// </summary>
public class EventManagerExample : MonoBehaviour
{
    void Start()
    {

        /*
         * Make sure you started the EventManager. If you downloaded the package, just
         * add the GameManager Prefab in the scene.         * 
         */

        // Adding a Listener for Event 1
        EventManager.StartListening(EventNames.EventName1, EventListener1);
        // Adding a Listener for Event 2
        EventManager.StartListening(EventNames.EventName2, EventListener2);
        // Adding another Listener for Event 1
        EventManager.StartListening(EventNames.EventName1, EventListener3);
        // Adding a Listener for Event 3
        EventManager.StartListening(EventNames.EventWithNoParams, EventListener4);

        //Instantiate the EvenInfos, in this case, a subclass from it.
        Event1InfoDataExample e1 = new Event1InfoDataExample(EventNames.EventName1);
        e1.Data = 987;        
        // Trigger Event 1
        Debug.Log("Sending Event 1:");
        EventManager.TriggerEvent(e1);

        //Instantiate the EvenInfos, in this case, a subclass from it.
        Event2InfoDataExample e2 = new Event2InfoDataExample(EventNames.EventName2);
        e2.V3 = new Vector3(-15.0f, 55.0f, 0f);
        // Trigger Event 2
        Debug.Log("Sending Event 2:");
        EventManager.TriggerEvent(e2);
        
        // Update data for the event
        e1.Data = 55;
        // Trigger Event 1 again
        Debug.Log("Sending Event 1 again:");
        EventManager.TriggerEvent(e1);
        
        // Trigger Event 3 with Default values inside info.
        Debug.Log("Sending Event 3:");
        EventManager.TriggerEvent(new EventInfo(EventNames.EventWithNoParams));

        /*         
         Unregister all events when done
         */
        EventManager.StopListening(EventNames.EventName1, EventListener1);
        EventManager.StopListening(EventNames.EventName2, EventListener2);
        EventManager.StopListening(EventNames.EventName1, EventListener3);
        EventManager.StopListening(EventNames.EventWithNoParams, EventListener4);

        // Trigger Event 1 again, but this time no one will listen.
        Debug.Log("Sending Event 1 but no one will hear :(");
        EventManager.TriggerEvent(e1);
    }

    private void EventListener1(EventInfo e)
    {
        Event1InfoDataExample et = e as Event1InfoDataExample;
        Debug.Log("Received Event 1 on Listener 1 with data:" + et.Data);
    }

    private void EventListener2(EventInfo e)
    {
        Event2InfoDataExample et = e as Event2InfoDataExample;
        Debug.Log("Received Event 2 on Listener 2 with data:" + et.V3);
    }
    // Handler
    private void EventListener3(EventInfo e)
    {
        Event1InfoDataExample et = e as Event1InfoDataExample;
        Debug.Log("Received Event 1 on Listener 3 with data:" + et.Data);
    }
    // Handler
    private void EventListener4(EventInfo e)
    {
        Debug.Log("Received Event 3 on Listener 4");
    }
}
