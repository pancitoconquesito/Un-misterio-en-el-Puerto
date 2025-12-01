using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CsvReader //: MonoBehaviour
{
    static readonly int id = 0;
    static readonly int index_codigo = 1;
    static readonly int isTalkingPJ = 2;
    static readonly int PJ_Emocion_img = 3;
    static readonly int NPC_Emocion_img = 4;
    static readonly int sp_globo = 5;
    //nombres
    static readonly int nombreNPC_ES = 6;
    static readonly int nombreNPC_EN = 7;
    //text
    static readonly int Text_ES = 8;
    static readonly int Text_EN = 9;

    public enum FilesCSV
    {
        TEST,
    }

    //public FilesCSV filesCSV;
    //public string codigo = "TEST";

    //void Start()
    //{
    //    MostrarFilaPorId(codigo, filesCSV, GLOBAL_TYPE.IDIOMA.espanol);
    //}
    public static List<SO_parrafo> MostrarFilaPorId(string _codigo, FilesCSV _filesCSV, GLOBAL_TYPE.IDIOMA idioma)
    {
        string ruta = $"CSV/{_filesCSV.ToString()}";
        TextAsset csvFile = Resources.Load<TextAsset>(ruta);
        string[] lineas = csvFile.text.Split('\n');
        List<SO_parrafo> parrafos = new List<SO_parrafo>();
        bool encontrado=false;
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] columnas = lineas[i].Split(';');

            if (columnas!=null && columnas.Length>1 && !String.IsNullOrEmpty(columnas[index_codigo]) && columnas[index_codigo] == _codigo)
            {
                encontrado = true;
                string curr_index_codigo = columnas[index_codigo];
                string curr_isTalkingPJ = columnas[isTalkingPJ];
                string curr_PJ_Emocion_img = columnas[PJ_Emocion_img];
                string curr_NPC_Emocion_img = columnas[NPC_Emocion_img];
                string curr_sp_globo = columnas[sp_globo];
                string curr_nombreNPC_ES = columnas[nombreNPC_ES];
                string curr_nombreNPC_EN = columnas[nombreNPC_EN];
                string curr_Text_ES = columnas[Text_ES];
                string curr_Text_EN = columnas[Text_EN];

                if (!string.IsNullOrEmpty(curr_isTalkingPJ))
                {
                    //crear nuevo parrafo
                    SO_parrafo newParrafo = new SO_parrafo();
                    newParrafo.isTalkingPJ = true;
                    newParrafo.PJ_Emocion_img = (GLOBAL_TYPE.anim_sp_EMOCIONES)Enum.Parse(typeof(GLOBAL_TYPE.anim_sp_EMOCIONES), curr_PJ_Emocion_img);
                    newParrafo.NPC_Emocion_img = (GLOBAL_TYPE.anim_sp_EMOCIONES)Enum.Parse(typeof(GLOBAL_TYPE.anim_sp_EMOCIONES), curr_NPC_Emocion_img);
                    newParrafo.sp_globo = (GLOBAL_TYPE.tipo_globo)Enum.Parse(typeof(GLOBAL_TYPE.tipo_globo), curr_sp_globo);

                    string curr_nombreNPC = "";
                    switch (idioma)
                    {
                        case GLOBAL_TYPE.IDIOMA.espanol:
                            {
                                curr_nombreNPC = curr_nombreNPC_ES;
                                break;
                            }
                        case GLOBAL_TYPE.IDIOMA.ingles:
                            {
                                curr_nombreNPC = curr_nombreNPC_EN;
                                break;
                            }
                    }
                    newParrafo.nombreNPC = (GLOBAL_TYPE.nombreNPC)Enum.Parse(typeof(GLOBAL_TYPE.nombreNPC), curr_nombreNPC);

                    newParrafo.texto_ESPANOL = new List<string>();
                    newParrafo.texto_INGLES = new List<string>();
                    newParrafo.texto_ESPANOL.Add(curr_Text_ES);
                    newParrafo.texto_INGLES.Add(curr_Text_EN);
                    //agregarlo
                    parrafos.Add(newParrafo);
                }
                else
                {
                    //tomar ultimo parrado y agregar texto
                    parrafos[parrafos.Count - 1].texto_ESPANOL.Add(curr_Text_ES);
                    parrafos[parrafos.Count - 1].texto_INGLES.Add(curr_Text_EN);
                }
            }
            else
            {
                if (encontrado)
                {
                    break;
                }
            }
        }
        return parrafos;
    }
    //void MostrarFilaPorId(string path, int id)
    //{
    //    if (!File.Exists(path))
    //    {
    //        Debug.LogError($"No se encontró el archivo CSV en: {path}");
    //        return;
    //    }

    //    string[] lineas = File.ReadAllLines(path);

    //    // Asumimos que la primera línea es encabezado
    //    for (int i = 1; i < lineas.Length; i++)
    //    {
    //        string[] columnas = lineas[i].Split(',');

    //        if (columnas.Length > 0 && int.TryParse(columnas[0], out int idActual))
    //        {
    //            if (idActual == id)
    //            {
    //                Debug.Log($"Fila encontrada: {lineas[i]}");
    //                Debug.Log($"Nombre: {columnas[1]}, Edad: {columnas[2]}, Puntaje: {columnas[3]}");
    //                return;
    //            }
    //        }
    //    }

    //    Debug.LogWarning($"No se encontró ninguna fila con id = {id}");
    //}
}
