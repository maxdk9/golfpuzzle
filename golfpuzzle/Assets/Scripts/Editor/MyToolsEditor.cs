using Assets.SimpleLocalization;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(ChangeFontTextMeshPro))]
    public class MyToolsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var component = (ChangeFontTextMeshPro) target;

            if (GUILayout.Button("ChangeFonts"))
            {
                component.ChangeFonts();
            }
        }
    }
}