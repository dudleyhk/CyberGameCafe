/*
 
 
 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public const string EVENT_APP_QUIT = "event_application_quit";


    private static bool initialised = false;
    private static bool appQuitting = false;

    // by having a list of event actions you can assign as many functions as you want to a single eventType
    private static Dictionary<string, List<EventAction>> actions; 


    class EventAction
    {
        public Action action;
        public EventAction(Action action)
        {
            this.action = action;
        }
    }


    private static void Init()
    {
        if (appQuitting) return;

        actions = new Dictionary<string, List<EventAction>>();
        initialised = true;


        Listen(EVENT_APP_QUIT, OnAppQuit);
    }


    private static void OnAppQuit()
    {
        Forget(Events.EVENT_APP_QUIT, OnAppQuit);
        Events.Send(Events.EVENT_APP_QUIT);
        appQuitting = true;
    }


    public static void Listen(string eventType, Action handler)
    {
        if (!initialised) Init();
        if (!actions.ContainsKey(eventType)) actions.Add(eventType, new List<EventAction>());

        List<EventAction> eventActions = actions[eventType];
        eventActions.Add(new EventAction(handler));
    }



    public static void Send(string eventType)
    {
        Debug.Log("Event Sent: " + eventType);

        if (!initialised) Init();
        if (appQuitting) return;
        if (!actions.ContainsKey(eventType)) return;

        // clone incase something modifies whilst we're doing this
        List<EventAction> eventActions = actions[eventType];
        foreach (EventAction action in actions[eventType])
        {
            // Calls all of the functions which have been asigned to the value which is the event name 
            if (action.action != null) action.action();
        }
    }

    public static void Forget(string eventType, Action handler)
    {
        if (!initialised) Init();
        if (appQuitting) return;
        if (!actions.ContainsKey(eventType)) return;

        List<EventAction> eventAction = actions[eventType];
        for(int i = 0; i < actions.Count; i++)
        {
            if(handler != null && eventAction[i].action == handler)
            {
                eventAction.RemoveAt(i);
                i--;
            }
        }
    }
}
