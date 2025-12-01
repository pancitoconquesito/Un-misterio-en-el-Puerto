using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOverlay : MonoBehaviour
{
    public int tamanioLetra = 24;
    public Color colorLetra = Color.green;
    private static readonly List<string> logs = new List<string>();
    private static Vector2 scrollPosition;
    private GUIStyle textStyle;

    public static void Log(string tag, string message)
    {
        //if (logs.Contains(($"[{tag}]-{message}")))
        //{
        //    int counter = 0;
        //    if(logs!=null && logs.Count > 0)
        //    {
        //        for (int i = logs.Count - 1; i >= 0; i--)
        //        {
        //            if (logs[i].Contains(tag))
        //            {
        //                logs.RemoveAt(i);
        //            }
        //        }

        //    }
        //}
        //logs.Add($"[{tag}]-{message}");
        //if (logs.Count > 5) // Máximo de mensajes
        //    logs.RemoveAt(0);
    }

    void Start()
    {
        textStyle = new GUIStyle();
        textStyle.fontSize = tamanioLetra; // Tamaño de letra al doble
        textStyle.normal.textColor = colorLetra; 
    }
    Texture2D backgroundTexture = null;
    void OnGUI()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        
        
        
        scrollPosition = GUI.BeginScrollView(new Rect(10, 10, Screen.width - 20, Screen.height / 2), scrollPosition, new Rect(0, 0, Screen.width - 40, logs.Count * (textStyle.fontSize + 4)));
        //MakeBackgroundTexture(new Color(0, 0, 0, 0.6f)); // negro con 60% de opacidad

        if (backgroundTexture == null)
            backgroundTexture = MakeBackgroundTexture(Color.black);

        // En tu loop
        for (int i = 0; i < logs.Count; i++)
        {
            GUIStyle backgroundStyle = new GUIStyle(textStyle);
            backgroundStyle.normal.background = backgroundTexture;

            Rect labelRect = new Rect(0, i * (textStyle.fontSize + 4), Screen.width - 40, textStyle.fontSize + 4);
            GUI.Label(labelRect, logs[i], backgroundStyle);
        }


        //GUIStyle backgroundStyle = new GUIStyle(textStyle);
        //backgroundStyle.normal.background = Texture2D.blackTexture;

        //Rect labelRect = new Rect(0, i * (textStyle.fontSize + 4), Screen.width - 40, textStyle.fontSize + 4);
        //GUI.Label(labelRect, logs[i], backgroundStyle);

        //GUI.Label(new Rect(0, i * (textStyle.fontSize + 4), Screen.width - 40, textStyle.fontSize + 4), logs[i], textStyle);

        GUI.EndScrollView();

        Texture2D MakeBackgroundTexture(Color color)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }



#endif
    }
}
