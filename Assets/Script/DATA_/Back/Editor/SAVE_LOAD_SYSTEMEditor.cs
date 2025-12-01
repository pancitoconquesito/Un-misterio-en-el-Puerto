using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(SAVE_LOAD_SYSTEM))]
public class SAVE_LOAD_SYSTEMEditor : Editor
{
    private SAVE_LOAD_SYSTEM currentTarget;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        currentTarget = (SAVE_LOAD_SYSTEM)target;

        //_style = new GUIStyle(GUI.skin.button, );

        if (GUILayout.Button("Save"))
            currentTarget.save_();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Erase"))
            currentTarget.erase_();
        if (GUILayout.Button("Load"))
            currentTarget.load_();
        EditorGUILayout.EndHorizontal();

    }
}
