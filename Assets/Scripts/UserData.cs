using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UserData
{
    public bool isRegistered;
    public int userType_ud;
    public string Name;
    public string Dni;
    public string DocId;
    public string LabNica;
    public string Hospital;
    public string DSACode;


    public UserData (UserInfo userI)
    {
        isRegistered = userI.CheckIsRegistered();
        switch (userI.userT)
        {
            case MainController.userType.Lab:
                userType_ud = 0;
                LabNica = userI.LabNICA;
                break;
            case MainController.userType.Doctor:
                userType_ud = 1;
                Dni = userI.userDNI;
                DocId = userI.docCode;
                break;
            case MainController.userType.Hospital:
                userType_ud = 2;
                Hospital = userI.hospitalName;
                DSACode = userI.DSACode;
                break;
            default:
                break;
        }
        Name = userI.userName;
        

    }
}
