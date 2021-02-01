using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Gallego,
        Aleman,
        Frances,
        Italiano,
        Portugues,
        Arabe,
        Chino,
        Brasileño
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
        Atras,
        Laboratorio,
        TestRapido,
        TestPCR,
        TestElisa,       
        Idioma,
        DNI,
        IDFacultativo,
        ID,
        TestAntigenos,
        Inmuno,
        PrivacidadToogle,
        Privacidad,
        Hospital,
        DSA,
        DSABreathPass,
        Email,
        Phone,
        Next,
        Sintomático,
        Asintomático,
        SALIVA,
        DatosPaciente,
        IntroduceTusDatos,
        NuevaVacuna,
        EligeVacuna,
        RNA,
        VectorViral,
        VirusInactivado,
        SiguienteDosis,
        Vacuna,
        DosisPrevias,
        ConfirmarDosis,
        Confirmar,
        VacunaAnyadida
    }
}

[Serializable]
public class language
{
    public string[] textos;
}
