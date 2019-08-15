# EventManager 2.0

This repository contains an EventManger component that implements the Observer Design Pattern. In this version:
- Use C# Actions instead of Unity ones;
- Use Generic Methods to identify the Event Type;

## How to use it

First in the Events folder you'll find the **EventNames.cs** for each Event you must create a Class that implements the **IEvent** interface. This class will contain all information that your event needs to carry. 

Now we need do register our listeners and trigger our events. To register a listener just call:
```c#
EventManager.StartListening<GPSEvent>(EventListener1);
```
**GPSEvent** represents the event and implements **IEvent.**
This will guarantee that when  we trigger GPSEvent, EventListener1 will be called.

The following code is an example of such method. It receives an IEvent that is casted to our GPSEvent, that contains specific data for our event.
```c#
private void EventListener1(IEvent e)
    {
        GPSEvent gpe = e as GPSEvent;
        Debug.Log("Received Event 1 on Listener 1 with data:" + gpe.Coords);
    }
```
Triggering an Event is quite as easy  as listening to it: just instatiate the desired IEvent, and call the EventManager:
```c#
GPSEvent2 e2 = new GPSEvent2(8.0f, 5.5f);
Debug.Log("Sending Event 2:");
EventManager.TriggerEvent(e2);
```

When you don't need to linten to the observer anymore, remember to unregister your listener:

```c#
EventManager.StopListening<GPSEvent>(EventListener1);
```
