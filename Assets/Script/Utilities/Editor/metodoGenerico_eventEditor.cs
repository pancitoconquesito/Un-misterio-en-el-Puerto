using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[UnityEditor.CustomEditor(typeof(metodoGenerico_event))]

public class metodoGenerico_eventEditor : Editor
{
    private metodoGenerico_event currentTarget;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        currentTarget = (metodoGenerico_event)target;

        if (GUILayout.Button("Start Method!"))
            currentTarget.startEvent();
    }
}
