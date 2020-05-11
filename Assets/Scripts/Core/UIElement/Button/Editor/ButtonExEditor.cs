using UnityEditor;
using UnityEditor.UI;

namespace LightCore
{
    [CustomEditor(typeof(ButtonEx),true)]

    public class ButtonExEditor: SelectableEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }

    }
}

