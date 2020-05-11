using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LightCore
{
    public enum UIBaseType
    {
        Page,
        PopWindow
    }
    public class UIBase 
    {
        static int point;
        static UIBase[] buff = new UIBase[1024];
        public static T GetUI<T>()where T :UIBase
        {
            for (int i = 0; i < point; i++)
            {
                if (buff[i] is T)
                    return buff[i] as T;
            }
            return null;
        }
        public static List<T> GetUIs<T>() where T : UIBase
        {
            List<T> tmp = new List<T>();
            for (int i = 0; i < point; i++)
                if (buff[i] is T)
                    tmp.Add(buff[i] as T);
            return tmp;
        }
        public static void ClearUI()
        {
            for (int i = 0; i < point; i++)
                buff[i] = null;
            point = 0;
        }
        
        int index;
        public UIBase()
        {
            index = point;
            buff[point] = this;
            point++;
        }
        public object DataContext;
        public Transform Parent { get; protected set; }
        public GameObject Model { get; protected set; }
        public T LoadUI<T>(string asset,string name)where T :class,new()
        {
            return null;
        }
        public virtual void Initial(Transform parent,object obj=null)
        {
            DataContext = obj;
            Parent = parent;
            if(parent!=null)
            {
                if (Model != null)
                    Model.transform.SetParent(parent);
            }
            
        }
        public virtual void Update()
        {

        }
        public virtual void Cmd(int cmd,object dat)
        {

        }
        public virtual void Save()
        {

        }
        public virtual void Recovery()
        {

        }
        public virtual object CollectData()
        {
            return null;
        }
        public virtual void ReSize()
        {

        }
        public virtual void Dispose()
        {
            if (Model != null)
            {
                UIPool.Recycle(Model);
            }
            point--;
            if (buff[point] != null)
                buff[point].index = index;
            buff[index] = buff[point];
            buff[point] = null;
        }
    }
}

