using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LightCore
{
    public class UIPage : UIBase
    {
        class PageInfo
        {
            public Type Pagetype;
            public object DataContext;
            public Type PopType;
            public object PopData;
        }
        static Stack<PageInfo> pages = new Stack<PageInfo>();
        public static Type typePop = typeof(PopWindow);
        public static void ClearStack()
        {
            pages.Clear();
        }
        public static Transform Root { get; set; }
        public static UIPage CurrentPage { get; private set; }
        public static void LoadPage<T>(object dat = null) where T : UIPage, new()
        {
            if (CurrentPage is T)
            {
                CurrentPage.Show(dat);
                return;
            }
            if (CurrentPage != null)
            {
                CurrentPage.Save();
                CurrentPage.Dispose();
            }
            var t = new T();
            t.Initial(Root, dat);
            t.ReSize();
            CurrentPage = t;
        }
        List<PopWindow> pops;
        public PopWindow currentPop { get; private set; }
        public UIPage()
        {
            pops = new List<PopWindow>();
        }

        public virtual void Initial(Transform parent, object dat = null)
        {
            Parent = parent;
            DataContext = dat;
            if (Model != null)
            {
                var trans = Model.transform;
                trans.SetParent(Parent);
                trans.localPosition = Vector3.zero;
                trans.localScale = Vector3.one;
                trans.localRotation = Quaternion.identity;
            }
                
        }
        public virtual void Show(object dat)
        {

        }
        public override void ReSize() { base.ReSize(); if (currentPop != null) currentPop.ReSize(); }
        public override void Dispose()
        {
            if (pops != null)
                for (int i = 0; i < pops.Count; i++)
                    pops[i].Dispose();
            pops.Clear();
            currentPop = null;
            if (Model != null)
            {
                Model.transform.SetParent(null);
                UIPool.Recycle(Model);
            }
            ClearUI();
        }

    }
}

