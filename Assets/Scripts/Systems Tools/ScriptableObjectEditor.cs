using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//found here https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/#:~:text=If%20you%20want%20to%20edit%20SO%20inside%20SO,using%20UnityEngine%3B%20using%20UnityEditor%3B%20%5B%20CustomPropertyDrawer%20%28typeof%28ScriptableObject%29%2C%20true%29%5D
[CanEditMultipleObjects]
[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectEditor : PropertyDrawer
{
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);

        // Draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            EditorGUI.indentLevel++;

            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

            // Draw object properties
            if (editor) // catches empty property
                editor.OnInspectorGUI();

            // Set indent back to what it was
            EditorGUI.indentLevel--;
        }
    }
}