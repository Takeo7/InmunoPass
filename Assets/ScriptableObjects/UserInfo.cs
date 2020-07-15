﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableInfo/user Info")]
public class UserInfo : ScriptableObject
{
    [SerializeField]
    public MainController.userType userT;
    [SerializeField]
    public bool isRegistered = false;


    [SerializeField]
    public string userName;
    [SerializeField]
    public string userDNI;
    [SerializeField]
    public string docCode;

    [Space]
    [SerializeField]
    public string LabNICA;


    public PatientInfo patient_Info = new PatientInfo();
    public PatientInfo patient_temp = new PatientInfo();

    public void AddStandardInfo(string n, string id)
    {
        //LAB
        userName = n;
        LabNICA = id;
        //userT = MainController.userType.Lab;
        isRegistered = true;
    }
    public void AddStandardInfo(string n, string id, string dId)
    {
        //DOCTOR
        userName = n;
        userDNI = id;
        docCode = dId;
        //userT = MainController.userType.Doctor;
        isRegistered = true;
    }

    public bool CheckIsRegistered()
    {
        return isRegistered;
    }

    public void InitializeNewPatient()
    {
        patient_Info = new PatientInfo();
        patient_temp = new PatientInfo();
        patient_temp.tests = new Test[3];
        for (int i = 0; i < 3; i++)
        {
            patient_temp.tests[i] = new Test();
        }
    }

    public void AddPatient(string pn, string pid)
    {
        patient_temp.PatientName = pn;
        patient_temp.PatientDNI = pid;
    }

    #region Rapido
    public void AddRAPIDOResult_IGM(string igm)
    {
        Debug.Log(patient_temp.tests.Length);
        patient_temp.tests[0].testType = "RAPIDO";
        patient_temp.tests[0].Result_igm = igm;

        if (patient_temp.tests[0].Result_igg != "")
        {
            patient_temp.tests[0].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }
        

        patient_Info = patient_temp;

    }

    public void AddRAPIDOResult_IGG(string igg)
    {
        patient_temp.tests[0].testType = "RAPIDO";
        patient_temp.tests[0].Result_igg = igg;

        if (patient_temp.tests[0].Result_igm != "")
        {
            patient_temp.tests[0].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }

        patient_Info = patient_temp;
    }
    #endregion

    #region PCR
    public void AddPCRResult(string result)
    {
        patient_temp.tests[1].testType = "PCR";
        patient_temp.tests[1].testResult = result;
        patient_temp.tests[1].valid = true;
        MainController.instance.uiC.ShowGenerateQR();

        patient_Info = patient_temp;
    }
    #endregion

    #region Elisa
    public void AddELISAResult_igm(string igm)
    {
        patient_temp.tests[2].testType = "ELISA";
        patient_temp.tests[2].Result_igm = igm;

        if (patient_temp.tests[2].Result_igg != "" && patient_temp.tests[2].Valor_igg != "" && patient_temp.tests[2].Valor_igm != "")
        {
            patient_temp.tests[2].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }

        patient_Info = patient_temp;
    }

    public void AddELISAResult_igg(string igg)
    {
        patient_temp.tests[2].testType = "ELISA";
        patient_temp.tests[2].Result_igg = igg;

        if (patient_temp.tests[2].Result_igm != "" && patient_temp.tests[2].Valor_igg != "" && patient_temp.tests[2].Valor_igm != "")
        {
            patient_temp.tests[2].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }

        patient_Info = patient_temp;
    }

    public void AddELISAResult_Valorigm(string v_igm)
    {
        patient_temp.tests[2].testType = "ELISA";
        patient_temp.tests[2].Valor_igm = v_igm;

        if (patient_temp.tests[2].Result_igg != "" && patient_temp.tests[2].Result_igm != "" && patient_temp.tests[2].Valor_igg != "")
        {
            patient_temp.tests[2].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }

        patient_Info = patient_temp;
    }

    public void AddELISAResult_Valorigg(string v_igg)
    {
        patient_temp.tests[2].testType = "ELISA";
        patient_temp.tests[2].Valor_igg = v_igg;

        if (patient_temp.tests[2].Result_igg != "" && patient_temp.tests[2].Result_igm != "" && patient_temp.tests[2].Valor_igm != "")
        {
            patient_temp.tests[2].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }

        patient_Info = patient_temp;
    }

    #endregion


    public void ResetInfo()
    {
        switch (userT)
        {
            case MainController.userType.Lab:
                patient_Info = new PatientInfo();
                patient_temp = new PatientInfo();
                isRegistered = false;
                userName = "";
                LabNICA = "";
                SaveSystemController.SaveUser(new UserInfo());
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case MainController.userType.Doctor:
                patient_Info = new PatientInfo();
                patient_temp = new PatientInfo();
                isRegistered = false;
                userName = "";
                userDNI = "";
                docCode = "";
                SaveSystemController.SaveUser(new UserInfo());
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            default:
                break;
        }
        
    }



   

}

public class PatientInfo
{
    public string PatientName;
    public string PatientDNI;

    public Test[] tests = new Test[3];
    
}

public class Test
{
    public bool valid;

    public string Date;
    public string testType;
    public string testResult;//PCR, ELISA, RAPIDO

    public string Result_igm;
    public string Result_igg;

    public string Valor_igm;
    public string Valor_igg;
}
