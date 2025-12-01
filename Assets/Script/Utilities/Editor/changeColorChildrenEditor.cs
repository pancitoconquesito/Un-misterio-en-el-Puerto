using UnityEngine;
using UnityEditor;
[UnityEditor.CustomEditor(typeof(changeColorChildren))]
public class changeColorChildrenEditor : Editor
{
    private changeColorChildren currentTarget;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        currentTarget = (changeColorChildren)target;

        if (GUILayout.Button("Change Color"))
            currentTarget.changeColor();
    }
}
