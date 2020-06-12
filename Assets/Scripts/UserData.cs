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
    public string LabNica;


    public UserData (UserInfo userI)
    {
        isRegistered = userI.CheckIsRegistered();
        if (userI.userT == MainController.userType.Lab)
        {
            isDoctor = false;
            LabNica = userI.LabNICA;
        }
        else
        {
            
            isDoctor = true;
            Dni = userI.userDNI;
            DocId = userI.docCode;
        }
        Name = userI.userName;
        

    }
}
