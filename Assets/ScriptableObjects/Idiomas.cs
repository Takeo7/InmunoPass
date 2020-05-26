using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableInfo/Idiomas")]
public class Idiomas : ScriptableObject
{
    [SerializeField]
    idiomasEnum currentLang;

    [SerializeField]
    language[] languages;


    public void ChangeLang(idiomasEnum i)
    {
        currentLang = i;
    }

    public string GetText(texto t)
    {
       return languages[(int)currentLang].textos[(int)t];  
    }

    public enum idiomasEnum
    {
        Español,
        Ingles,
        Catalan,
        Mallorqui,
        Gallego,
        Valenciano
    }
    public enum texto
    {
        Paciente,
        Doctor,
        Nombre,
        Apellido,
        Reiniciar,
        Generar,
        Escanear,
        Guardar,
        Fecha,
        Negativo,
        Positivo,
        Ingresar,
        Registrar,
        Usuario,
        Contraseña,
        NuevoTest,
        Atras
    }
}

[Serializable]
public class language
{
    public string[] textos;
}
