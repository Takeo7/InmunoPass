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
    bool isRegistered;
    [SerializeField]
    public string userName;
    [SerializeField]
    public string userDNI;

    [Space]
    [SerializeField]
    bool isTested;

    public PatientInfo patient_Info;
    public List<DoctorInfo> doctor_Info = new List<DoctorInfo>();

    DoctorInfo doc_temp = new DoctorInfo();

    public void AddStandardInfo(string n, string id)
    {
        userName = n;
        userDNI = id;
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

    public void AddPatientInfo(string d, string t, string dat, string r)
    {
        PatientInfo temp = new PatientInfo();

        temp.Doctor = d;
        temp.Test = t;
        temp.Date = dat;
        temp.Result = r;

        patient_Info = temp;

        isTested = true;
    }

    public void PatientPharser(string t)
    {
        string[] sliced = t.Split('/');

        AddPatientInfo(sliced[0], sliced[1], sliced[2], sliced[3]);
    }

    public void DoctorPatientPharser(string t)
    {
        string[] sliced = t.Split('/');

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

    public void AddDocResult(string r)
    {
        doc_temp.Result = r;

        doctor_Info.Add(doc_temp);
        doc_temp = new DoctorInfo();
    }



   

}

public class PatientInfo
{
    public string Doctor;
    public string Test;
    public string Date;
    public string Result;   
}

public class DoctorInfo
{
    public string PatientName;
    public string PatientDNI;
    public string Test;
    public string Date;
    public string Result;
}
