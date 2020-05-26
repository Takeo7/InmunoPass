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
    string userSec;
    [SerializeField]
    string passSec;
    [SerializeField]
    public string userName;
    [SerializeField]
    public string userDNI;
    [SerializeField]
    public string docCode;

    [Space]
    [SerializeField]
    public bool isTested;

    public PatientInfo patient_Info = new PatientInfo();
    public DoctorInfo doctor_Info = new DoctorInfo();

    public DoctorInfo doc_temp = new DoctorInfo();

    public void AddStandardInfo(string n, string id)
    {
        userName = n;
        userDNI = id;
        isRegistered = true;
    }
    public void AddStandardInfo(string n, string id, string dId)
    {
        userName = n;
        userDNI = id;
        docCode = dId;
        isRegistered = true;
    }

    public bool CheckIsRegistered()
    {
        return isRegistered;
    }
    public bool CheckIsTested()
    {
        return isTested;
    }

    public void AddPatientInfo(string d, string t, string dat, string igm, string igg)
    {
        PatientInfo temp = new PatientInfo();

        temp.Doctor = d;
        temp.Test = t;
        temp.Date = dat;
        temp.Result_igm = igm;
        temp.Result_igg = igg;

        patient_Info = temp;

        isTested = true;
    }

    public void PatientPharser(string t)
    {
        string[] sliced = t.Split('%');

        AddPatientInfo(sliced[0], sliced[1], sliced[2], sliced[3], sliced[4]);
    }

    public void DoctorPatientPharser(string t)
    {
        doc_temp = new DoctorInfo();

        string[] sliced = t.Split('%');

        AddDocPatient(sliced[0], sliced[1]);
    }



    public void AddDocPatient(string pn, string pid)
    {
        doc_temp.PatientName = pn;
        doc_temp.PatientDNI = pid;
    }

    public void AddDocTest(string t)
    {
        doc_temp.Test = t;
        doc_temp.Date = DateTime.Today.ToString();
    }

    public void AddDocResult(string igm, string igg)
    {
        doc_temp.Result_igm = igm;
        doc_temp.Result_igg = igg;

        doctor_Info = doc_temp;
        
    }


    public void ResetInfo()
    {
        doctor_Info = new DoctorInfo();
        doc_temp = new DoctorInfo();
        isRegistered = false;
        isTested = false;
        userName = "";
        userDNI = "";
        docCode = "";
        patient_Info = new PatientInfo();
        SaveSystemController.SaveUser(new UserInfo());
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }



   

}

public class PatientInfo
{
    public string Doctor;
    public string Test;
    public string Date;
    public string Result_igm;
    public string Result_igg;
}

public class DoctorInfo
{
    public string PatientName;
    public string PatientDNI;
    public string Test;
    public string Date;
    public string Result_igm;
    public string Result_igg;
}
