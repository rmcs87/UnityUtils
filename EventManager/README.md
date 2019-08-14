# EventManager

This repository contains an EventManger component that implements the Observer Design Pattern:

"The observer pattern is a software design pattern in which an object, called the subject, maintains a list of its dependents, called observers, and notifies them automatically of any state changes, usually by calling one of their methods." - From Wikipedia

The Observer pattern provides you with the following advantages (Learning Python Design Patterns - Second Edition by Chetan Giridhar):

1. It supports the principle of loose coupling between objects that interact with each other
1. It allows sending data to other objects effectively without any change in the Subject or Observer classes
1. Observers can be added/removed at any point in time

Some disadvantages of the Observer pattern are:
1. Memory leaks caused by Lapsed listener problem because of explicit register and unregistering of observers.
1. If not correctly implemented, the Observer can add complexity and lead to inadvertent performance issues.
1. Tests may became harder to apply.

## How to use it

The **EventManagerUtils** folder contains the **EventManager** and **EventInfo** classes. You won't need to modify them, but if you want, explore them to understanding better what's going on.

First in the Events folder you'll find the **EventNames.cs** . This enumaration contains the names of the events you shall use in your application. Edit it including your events names. 

Then for each Event you can create a Class containing all information about the event. These classes shall inherit from EventInfo one (examples can be found in Events folder). If you don't want to specialize the event, just use the EventInfo, as we will talk later. With the event names and our personalized (or not) events, we can go on.

Now we need do register our listeners and trigger our events. To register a listener just call:
```c#
EventManager.StartListening(EventNames.EventName1, EventListener1);
```
This will guarantee that when EventNames. EventName1 is triggered, method EventListener1 will be called. Don't forget to include:

```c#
using EventManagerUtils;
```

The following code is an example of such method. It receives an EventInfo that is casted to our specialized class Event1InfoDataExample, that contains specific data for our event.
```c#
private void EventListener1(EventInfo e)
    {
        Event1InfoDataExample et = e as Event1InfoDataExample;
        Debug.Log("Received Event 1 on Listener 1 with data:" + et.Data);
    }
```
Triggering an Event is quite as easy: just instatiate the desired EventInfo, and call the EventManager as listening to it:
```c#
        //Instantiate the EvenInfos, in this case, a subclass.
        Event1InfoDataExample e1 = new Event1InfoDataExample(EventNames.EventName1);
        e1.Data = 987;        
        // Trigger Event
        EventManager.TriggerEvent(e1);
```

When you don't need to linten to the observer anymore, remember to unregister your listener:

```c#
EventManager.StopListening(EventNames.EventName1, EventListener1);
```


## Example

1. Import the Package into your Unity Project:
    * eventManager.unitypackage (https://github.com/rmcs87/UnityUtils/blob/master/EventManager/eventManager.unitypackage)
1. Add both prefabs to your scen:
    * EventManager and Example
1. Execute the scene, and you should see in the console the result of an example.
1. Jump into the example and explore it.
