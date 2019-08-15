using UnityEngine;

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

        EventManager.StartListening<GPSEvent>(EventListener1);
        EventManager.StartListening<GPSEvent2>(EventListener2);
        EventManager.StartListening<GPSEvent>(EventListener3);

        GPSEvent e1 = new GPSEvent(5.0f, 7.5f);        
        Debug.Log("Sending Event 1:");
        EventManager.TriggerEvent(e1);

        GPSEvent2 e2 = new GPSEvent2(8.0f, 5.5f);
        Debug.Log("Sending Event 2:");
        EventManager.TriggerEvent(e2);

        Debug.Log("Sending Event 1 again:");
        EventManager.TriggerEvent(e1);

        EventManager.StopListening<GPSEvent>(EventListener1);
        EventManager.StopListening<GPSEvent2>(EventListener2);
        EventManager.StopListening<GPSEvent>(EventListener3);

        Debug.Log("Sending Event 1 again but for nobody:");
        EventManager.TriggerEvent(e1);
    }

    private void EventListener1(IEvent e)
    {
        GPSEvent gpe = e as GPSEvent;
        Debug.Log("Received Event 1 on Listener 1 with data:" + gpe.Coords);
    }

    private void EventListener2(IEvent e)
    {
        GPSEvent2 gpe = e as GPSEvent2;
        Debug.Log("Received Event 2 on Listener 2 with data:" + gpe.Coords);
    }

    private void EventListener3(IEvent e)
    {
        GPSEvent gpe = e as GPSEvent;
        Debug.Log("Received Event 1 on Listener 3 with data:" + gpe.Coords);
    }
}