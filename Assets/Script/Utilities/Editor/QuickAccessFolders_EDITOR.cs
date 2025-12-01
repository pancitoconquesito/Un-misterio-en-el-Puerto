using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

public class QuickAccessFolders_EDITOR : EditorWindow
{
    [MenuItem("Tools/Quick Access Folders - Editor")]
    public static void ShowWindow()
    {
        GetWindow<QuickAccessFolders_EDITOR>("Quick Access Folders");
    }

    // Define las rutas de las carpetas que quieres tener como acceso rápido
    private string[] folderPaths = new string[]
    {
        "Assets/Script/Utilities/Editor;Editor",
        "Assets/PREFABS/Interactables;Interactibles Prefabs",
        "Assets/Scenes/SO_;SCENES",
        "Assets/SRC/Resources/DATA;DATA Hardcodeada",
        "Assets/Script/DATA_/Back; ObjPersistentes",
    };

   
    private void OnGUI()
    {
        GUILayout.Label("Quick Access Folders", EditorStyles.boldLabel);

        // Crear botones dinámicos para cada carpeta
        foreach (var Nodepath in folderPaths)
        {
            string path = Nodepath.Split(';')[0];
            string name = Nodepath.Split(';')[1];
            if (GUILayout.Button($"{name}"))
            {
                OpenFolderInProject(path);
            }
        }

        // Botón para refrescar el contenido
        GUILayout.Space(10); // Espacio para separar los botones
        if (GUILayout.Button("Refresh Folders"))
        {
            RefreshProjectWindow();
        }
    }

    private void OpenFolderInProject(string path)
    {
        // Cargar la carpeta como un objeto de Unity
        Object folder = AssetDatabase.LoadAssetAtPath<Object>(path);
        if (folder != null)
        {
            // Abre la carpeta automáticamente en la ventana del Proyecto
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);

            // Abre la carpeta automáticamente
            AssetDatabase.OpenAsset(folder);
        }
        else
        {
            Debug.LogError($"Folder not found at path: {path}");
        }
    }

    private void RefreshProjectWindow()
    {
        // Refresca la base de datos de assets y la ventana del proyecto
        AssetDatabase.Refresh();
        Debug.Log("Project window refreshed!");
    }
}
