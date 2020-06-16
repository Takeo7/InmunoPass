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



    public DoctorPatientInfo doctor_Info = new DoctorPatientInfo();
    public DoctorPatientInfo doc_temp = new DoctorPatientInfo();

    public LabPatientInfo lab_Info = new LabPatientInfo();
    public LabPatientInfo lab_temp = new LabPatientInfo();

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



    public void AddDocPatient(string pn, string pid)
    {
        doc_temp.PatientName = pn;
        doc_temp.PatientDNI = pid;
    }

    public void AddLabPatient(string pn, string pid)
    {
        lab_temp.PatientName = pn;
        lab_temp.PatientDNI = pid;
    }

    public void AddDocTest(string t)
    {
        doc_temp.Test = t;
        doc_temp.Date = DateTime.Today.ToString();
    }

    public void AddLabTest()
    {
        lab_temp.Date = DateTime.Today.ToString();
    }

    public void AddDocResult(string igm, string igg)
    {
        doc_temp.Result_igm = igm;
        doc_temp.Result_igg = igg;

        doctor_Info = doc_temp;
        
    }

    public void AddLabResult(string igm, string igg)
    {
        lab_temp.Result_igm = igm;
        lab_temp.Result_igg = igg;

        lab_Info = lab_temp;

    }


    public void ResetInfo()
    {
        switch (userT)
        {
            case MainController.userType.Lab:
                lab_Info = new LabPatientInfo();
                lab_temp = new LabPatientInfo();
                isRegistered = false;
                userName = "";
                LabNICA = "";
                SaveSystemController.SaveUser(new UserInfo());
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case MainController.userType.Doctor:
                doctor_Info = new DoctorPatientInfo();
                doc_temp = new DoctorPatientInfo();
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

public class DoctorPatientInfo
{
    public string PatientName;
    public string PatientDNI;
    public string Test;
    public string Date;
    public string Result_igm;
    public string Result_igg;
}

public class LabPatientInfo
{
    public string PatientName;
    public string PatientDNI;
    public string Date;
    public string Result_igm;
    public string Result_igg;
}
