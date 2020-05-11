using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightCore
{
    public class ButtonEx : Selectable,IPointerClickHandler
    {
        public Action<ButtonEx, object> OnClick;
        public Action<ButtonEx, object> PointerClick;
        public Action<ButtonEx, object> PointerEnter;
        public Action<ButtonEx, object> PointerExit;
        public object DataContex;
        private RectTransform rectTransform; 
        public RectTransform RectTransform
        {
            get {
                if(rectTransform==null)
                {
                    rectTransform = transform as RectTransform;
                }
                return rectTransform;
            }
        }
        public Image GetImage
        {
            get
            {
                if (image == null)
                    image = RectTransform.GetComponent<Image>();
                return image;
            }
        }
        private Text text;
        public Text GetText
        {
            get
            {
                if(text==null)
                {
                    if(transform.childCount>0)
                    {
                        text = RectTransform.GetChild(0).GetComponent<Text>();
                        return text;
                    }
                    return null;
                }
                return text;
            }
        }

       
        public void OnPointerClick(PointerEventData eventData)
        {
            
            if(PointerClick!=null)
            {
                PointerClick(this,DataContex);
            }
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (PointerEnter != null)
            {
                PointerEnter(this,DataContex);
            }
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if(PointerExit!=null)
            {
                PointerExit(this,DataContex);
            }
        }
    }
}

