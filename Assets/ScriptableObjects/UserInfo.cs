using System;
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

    [Space]
    [SerializeField]
    public string hospitalName;
    [SerializeField]
    public string DSACode;


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
    public void AddDSAInfo(string n, string dsa, string hospital)
    {
        userName = n;
        hospitalName = hospital;
        DSACode = dsa;

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
        patient_temp.tests = new Test[6];
        for (int i = 0; i < patient_temp.tests.Length; i++)
        {
            patient_temp.tests[i] = new Test();
        }
    }

    public void AddPatient(string pn, string pid)
    {
        patient_temp.PatientName = pn;
        patient_temp.PatientDNI = pid;
    }

    public void AddPatientSintomatic(bool b)
    {   
        patient_temp.Sintomatic = b.ToString();
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

    #region Inmuno
    public void AddInmunoResult_IGM(string igm)
    {
        Debug.Log(patient_temp.tests.Length);
        patient_temp.tests[4].testType = "INMUNOCROMA";
        patient_temp.tests[4].Result_igm = igm;

        if (patient_temp.tests[4].Result_igg != "")
        {
            patient_temp.tests[4].valid = true;
            MainController.instance.uiC.ShowGenerateQR();
        }


        patient_Info = patient_temp;

    }

    public void AddInmunoResult_IGG(string igg)
    {
        patient_temp.tests[4].testType = "INMUNOCROMA";
        patient_temp.tests[4].Result_igg = igg;

        if (patient_temp.tests[4].Result_igm != "")
        {
            patient_temp.tests[4].valid = true;
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

    #region AntigenosQuick
    public void AddAntigenosQResult(string result)
    {
        patient_temp.tests[3].testType = "ANTIGENOS";
        patient_temp.tests[3].testResult = result;
        patient_temp.tests[3].valid = true;
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

    #region DSA
    public void AddDSAResult(string result)
    {
        patient_temp.tests[5].testType = "SALIVA";
        patient_temp.tests[5].testResult = result;
        patient_temp.tests[5].valid = true;
        MainController.instance.uiC.ShowGenerateQR();

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
            case MainController.userType.Hospital:
                patient_Info = new PatientInfo();
                patient_temp = new PatientInfo();
                
                isRegistered = false;

                userName = "";
                userDNI = "";
                DSACode = "";
                hospitalName = "";

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
    public string PatientCountry;
    public string PatientEmail;
    public string PatientPhone;

    public string Sintomatic;

    public Test[] tests = new Test[6];
    
}

public class Test
{
    public bool valid;

    public string Date;
    public string testType;
    public string testResult;

    public string Result_igm;
    public string Result_igg;

    public string Valor_igm;
    public string Valor_igg;
}
