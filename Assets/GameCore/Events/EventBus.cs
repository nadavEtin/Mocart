﻿using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public enum TypeOfEvent
    {
        ItemListServerResponse,
        ItemDetailsSaved,
        ItemDetailsUpdated,
        Error
    }

    public class EventBus
    {
        public static readonly EventBus Instance = new EventBus();
        private readonly Dictionary<TypeOfEvent, List<Action<BaseEventParams>>> _subscription = new();

        public void Subscribe(TypeOfEvent eventType, Action<BaseEventParams> handler)
        {
            if (_subscription.ContainsKey(eventType) == false)
                _subscription.Add(eventType, new List<Action<BaseEventParams>>());

            var handlerList = _subscription[eventType];
            if (handlerList.Contains(handler) == false)
                handlerList.Add(handler);
        }

        public void Unsubscribe(TypeOfEvent eventType, Action<BaseEventParams> handler)
        {
            if (_subscription.ContainsKey(eventType))
                _subscription[eventType]?.Remove(handler);
        }

        public void Publish(TypeOfEvent eventType, BaseEventParams eventParams)
        {
            if (_subscription.ContainsKey(eventType) == false)
                return;

            var handlerList = _subscription[eventType];
            foreach (var handler in handlerList)
                handler?.Invoke(eventParams);
        }
    }
}
