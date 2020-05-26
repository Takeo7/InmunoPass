using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UserData
{
    public bool isRegistered;
    public bool isDoctor;
    public string Name;
    public string Dni;
    public string DocId;
    public bool isTested;
    public string test;
    public string doctor;
    public string date;
    public string resultIgm;
    public string resultIgg;


    public UserData (UserInfo userI)
    {
        isRegistered = userI.CheckIsRegistered();
        if (userI.userT == MainController.userType.Patient)
        {
            isDoctor = false;
        }
        else
        {
            isDoctor = true;
        }
        Name = userI.userName;
        Dni = userI.userDNI;
        DocId = userI.docCode;
        isTested = userI.CheckIsTested();
        test = userI.patient_Info.Test;
        doctor = userI.patient_Info.Doctor;
        date = userI.patient_Info.Date;
        resultIgm = userI.patient_Info.Result_igm;
        resultIgg = userI.patient_Info.Result_igg;
    }
}
