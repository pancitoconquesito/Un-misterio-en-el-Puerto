using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
public class QuickAccessFolders_PC : EditorWindow
{
    [MenuItem("Tools/Quick Access Folders - PC")]
    public static void ShowWindow()
    {
        GetWindow<QuickAccessFolders_PC>("Quick Access Folders");
    }

    // Aquí defines las rutas a las carpetas que deseas abrir rápidamente
    private string[] folderPaths = new string[]
    {
        "Assets/PREFABS/Interactables",
        "Assets/Scenes/SO_",
        "Assets/SRC/Resources/DATA"
    };

    private void OnGUI()
    {
        GUILayout.Label("Quick Access Folders", EditorStyles.boldLabel);

        // Crear botones dinámicamente para cada carpeta
        foreach (var path in folderPaths)
        {
            if (GUILayout.Button($"Open {path}"))
            {
                OpenFolder(path);
            }
        }
    }

    private void OpenFolder(string path)
    {
        var fullPath = Application.dataPath.Replace("Assets", "") + path;
        EditorUtility.RevealInFinder(fullPath);
    }
}
