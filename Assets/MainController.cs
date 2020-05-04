using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public QRCodeDecodeController qrController;

    [SerializeField]
    UserInfo uinfo;

    [SerializeField]
    userType user_Type;
    [SerializeField]
    readType read_Type;
    void Start()
    {
        qrController.onQRScanFinished += SaveScanInfo;
    }

    public void SetUserType(int user)
    {
        switch (user)
        {
            case 0:
                user_Type = userType.Patient;
                break;
            case 1:
                user_Type = userType.Doctor;
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
        switch (user_Type)
        {
            case userType.Patient:
                uinfo.PatientPharser(scan);
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
