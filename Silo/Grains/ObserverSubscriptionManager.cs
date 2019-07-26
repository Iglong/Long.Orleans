using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grains
{
    /// <summary>
    /// 消息订阅管理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObserverSubscriptionManager<T> where T : IGrainObserver
    {
        public ObserverSubscriptionManager()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        private List<ObserverInstance<T>> _instance;
        public void Subscribe(ObserverInstance<T> observer) => _instance.Add(observer);
        public void Unsubscribe(ObserverInstance<T> observer) => _instance.Remove(observer);
        public void Notify(Action<ObserverInstance<T>> action, string key, Type type) => action(_instance.FirstOrDefault(m => m.Type == type && m.Key == key));

    }

    public class ObserverInstance<T> where T : IGrainObserver
    {
        public string Key { get; set; }
        public Type Type { get; set; }
        public T Instance { get; set; }
    }
}