using System;
using System.Collections.Generic;

public static class EventBroker
{
    private static readonly Dictionary<string, List<Action>> actionList = new();
    private static readonly Dictionary<string, List<Action<object>>> actionTList = new();

    public static void Subscribe(string key, Action action)
    {
        if (!actionList.ContainsKey(key))
            actionList[key] = new List<Action>();

        actionList[key].Add(action);
    }

    public static void Subscribe<T>(string key, Action<T> action)
    {
        if (!actionTList.ContainsKey(key))
            actionTList[key] = new List<Action<object>>();

        Action<object> actionWrapper = obj => action((T)obj);
        actionTList[key].Add(actionWrapper);
    }

    public static void UnSubscribe(string key, Action action)
    {
        if (actionList.ContainsKey(key))
        {
            actionList[key].Remove(action);
            if (actionList[key].Count == 0)
            {
                actionList.Remove(key);
            }
        }

    }

    public static void UnSubscribe<T>(string key, Action<T> action)
    {
        if (actionTList.ContainsKey(key))
        {
            Action<object> actionWrapper = obj => action((T)obj);
            actionTList[key].Remove(actionWrapper);
            if (actionTList[key].Count == 0)
            {
                actionTList.Remove(key);
            }
        }
    }

    public static void Publish(string key)
    {
        if (!actionList.ContainsKey(key))
            return;

        foreach (var action in actionList[key])
        {
            action();
        }
       
    }

    public static void Publish<T>(string key, T eventMessage)
    {
        if (actionTList.ContainsKey(key))
        {
            foreach (var action in actionTList[key])
            {
                action(eventMessage);
            }
        }
    }
}
