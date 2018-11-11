using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class EventWithMsg : UnityEvent<string> { }

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent> eventDictionary;

    public const string INIT_GAME = "init game";
    public const string START_GAME = "start game";
    public const string GAME_OVER = "game over";
    public const string PAUSE_GAME = "pause game";
    public const string RESUME_GAME = "resume game";

    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(EventManager)) as EventManager;

            if (instance == null) //still null ?!
                throw new System.NullReferenceException("EventManger need to be added to game object on scene");
            else
                instance.Init();

            return instance;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void ListenTo(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (instance == null) return;
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}