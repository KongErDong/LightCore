using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LightCore
{
    public struct ResSprite
    {
        public Sprite standard;
        public Sprite background;
        public Sprite inputField;
        public Sprite knob;
        public Sprite checkmark;
        public Sprite dropdown;
        public Sprite mask;
    }
    public static  class DefaultControlsEx 
    {
        private const float kWidth = 160f;
        private const float kThickHeight = 35f;
        private const float kThinHeight = 28f;
        private static Vector2 s_ThickElementSize = new Vector2(kWidth, kThickHeight);
        private static Vector2 s_ThinElementSize = new Vector2(kWidth, kThinHeight);
        private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);
        private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

        private  static GameObject CreateUIRoot(string name,Vector2 size)
        {
            GameObject obj = new GameObject(name);
            RectTransform rect=obj.AddComponent<RectTransform>();
            rect.sizeDelta = size;
            return obj;
        }
        private static GameObject CreateUIObject(string name,GameObject parent)
        {
            GameObject child = new GameObject(name);
            child.AddComponent<RectTransform>();
            SetParentAndAlign(child,parent);
            return child;
        }
        private static void SetDefaultTextValues(Text text)
        {
            text.color = s_TextColor;
        }
        /// <summary>
        /// 设置过渡值的默认颜色
        /// </summary>
        /// <param name="s"></param>
        private static void SetDefaultColorTransitionValues(Selectable s)
        {
            ColorBlock block = s.colors;
            block.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            block.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            block.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }
        private static void SetParentAndAlign(GameObject child,GameObject parent)
        {
            if (parent == null)
                return;
            child.transform.SetParent(parent.transform,false);
            SetLayer(child,parent.layer);
        }
        private static void SetLayer(GameObject go,int layer)
        {
            go.layer = layer;
            Transform tfm = go.transform;
            int count = tfm.childCount;
            for (int i = 0; i <count ; i++)
            {
                SetLayer(tfm.GetChild(i).gameObject,layer);
            }
        }
        public static GameObject CreateButton(ResSprite res)
        {
            GameObject root = CreateUIRoot("Button", s_ThickElementSize);
            Image image = root.AddComponent<Image>();
            image.sprite = res.standard;
            image.type = Image.Type.Sliced;
            image.color = s_DefaultSelectableColor;

            ButtonEx btn = root.AddComponent<ButtonEx>();
            SetDefaultColorTransitionValues(btn);

            GameObject text = CreateUIObject("Text",root);
            RectTransform textRec=text.GetComponent<RectTransform>();
            textRec.anchorMin = Vector2.zero;
            textRec.anchorMax = Vector2.one;
            textRec.sizeDelta = Vector2.zero;

            Text t=text.AddComponent<Text>();
            t.text = "Button";
            t.alignment = TextAnchor.MiddleCenter;
            SetDefaultTextValues(t);
            t.supportRichText = false;
            t.raycastTarget = false;
            t.fontSize = 16;

            root.transform.localPosition = Vector3.zero;
            return root;

        }


    }
}

