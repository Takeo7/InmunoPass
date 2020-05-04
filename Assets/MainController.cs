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
    public readType read_Type;
    void Start()
    {

        qrController.onQRScanFinished += SaveScanInfo;
        uiC.LoadUserInfo();

        CheckIsRegisteredStart();
    }

    public void ResetInfo()
    {
        uinfo.ResetInfo();
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
                uiC.FillDoctorInfo();
                uiC.DoctorAlreadyRegistered();
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
        Debug.Log(scan);
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
                        uinfo.DoctorPatientPharser(scan);
                        uiC.EndQRScan();
                        break;
                    case readType.BarCode:
                        uinfo.AddDocTest(scan);
                        uiC.EndQRScan();
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        qrController.Reset();
    }

    public void SaveStardardInfo(string n, string dni)
    {
        uinfo.AddStandardInfo(n, dni);
    }

    public void SetResultInfo(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.doc_temp.Result = "PASS";
                break;
            case false:
                uinfo.doc_temp.Result = "FAIL";
                break;
        }
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
