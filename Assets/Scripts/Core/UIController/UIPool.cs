using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LightCore
{

    public class UIPool
    {
        private static Dictionary<string, GameObject> pool = new Dictionary<string, GameObject>();
        private static RectTransform UIPage;
        private static RectTransform UIWindow;
        private static RectTransform PageBuffer;
        private static RectTransform WindowBuffer;
        public static void Init(RectTransform canvasTfm)
        {
            UIPage = canvasTfm.Find("UIPage") as RectTransform;
            UIWindow = canvasTfm.Find("UIPopWindow") as RectTransform;
            PageBuffer = canvasTfm.Find("PageBuffer") as RectTransform;
            WindowBuffer = canvasTfm.Find("WindowBuffer") as RectTransform;
        }
        public static void Recycle(GameObject obj, UIBaseType baseType = UIBaseType.Page)
        {
            string name = obj.name;
            RectTransform parent = baseType == UIBaseType.Page ? PageBuffer : WindowBuffer;
            obj.transform.SetParent(parent);
            pool[name] = obj;
        }
        public static GameObject Load(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            string[] arr = path.Split('/');
            GameObject obj = null;
            if (arr.Length > 0)
            {
                string name = arr[arr.Length - 1];
                if (!pool.TryGetValue(name, out obj))
                {
                    GameObject temp = Resources.Load<GameObject>(path);
                    obj = GameObject.Instantiate(temp);
                    obj.name = name;
                    return obj;
                }
            }
            return obj;
        }
    }
}

