using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LightCore
{
    public class EventPool
    {
        private static Dictionary<int, Action<object>> pool = new Dictionary<int, Action<object>>();

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public static void DispatchEvent(int name, object data)
        {
            if (pool.ContainsKey(name))
            {
                pool[name](data);
            }
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public static void Register(int name, Action<object> callback)
        {
            if (!pool.ContainsKey(name))
            {
                pool[name] = (o) => { };
            }
            pool[name] += callback;
        }
        /// <summary>
        /// 注销key的某个事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public static void Deregister(int name, Action<object> callback)
        {
            if (pool.ContainsKey(name))
            {
                pool[name] -= callback;
            }
        }
        /// <summary>
        /// 注销key的全部事件
        /// </summary>
        /// <param name="name"></param>
        public static void Deregister(int name)
        {
            pool.Remove(name);
        }
        /// <summary>
        /// 清空事件池
        /// </summary>
        public static void DeregisterAll()
        {
            pool.Clear();
        }

    }
}


