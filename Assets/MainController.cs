using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public QRCodeDecodeController qrController;
    public QRCodeEncodeController qrCreator;

    [SerializeField]
    UserInfo uinfo;
    [SerializeField]
    UIController uiC;

    [Space]
    [SerializeField]
    readType read_Type;
    void Start()
    {
        qrController.onQRScanFinished += SaveScanInfo;
        uiC.LoadUserInfo();

        CheckIsRegisteredStart();
    }

    public UserInfo GetUserScriptable()
    {
        return uinfo;
    }

    void CheckIsRegisteredStart()
    {
        if (uinfo.CheckIsRegistered())
        {
            if (uinfo.userT == userType.Patient)
            {
                uiC.FillPatientInfo();
                uiC.PatientAlreadyRegistered();
            }
            else
            {
                
            }
            
        }
    }

    public void SetUserType(int user)
    {
        switch (user)
        {
            case 0:
                uinfo.userT = userType.Patient;
                break;
            case 1:
                uinfo.userT = userType.Doctor;
                break;
            default:
                break;
        }
    }

    public void SetReadType(int read)
    {
        switch (read)
        {
            case 0:
                read_Type = readType.QR;
                break;
            case 1:
                read_Type = readType.BarCode;
                break;
            default:
                break;
        }
    }





    public void SaveScanInfo(string scan)
    {
        switch (uinfo.userT)
        {
            case userType.Patient:
                uinfo.PatientPharser(scan);
                uiC.EndQRScan();
                break;
            case userType.Doctor:
                switch (read_Type)
                {
                    case readType.QR:

                        break;
                    case readType.BarCode:
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void SaveStardardInfo(string n, string dni)
    {
        uinfo.AddStandardInfo(n, dni);
    }

    public enum userType
    {
        Patient,
        Doctor
    }

    public enum readType
    {
        QR,
        BarCode
    }
}
