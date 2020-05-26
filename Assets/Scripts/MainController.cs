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
        //string path = Application.persistentDataPath + "/userData.pass";
        //Debug.Log("Path: " + path);
        LoadUser();

        qrController.onQRScanFinished += SaveScanInfo;
        


    }


    public void SaveUser()
    {
        SaveSystemController.SaveUser(uinfo);
    }

    public void LoadUser()
    {

        Debug.Log("Load User");

        UserData data = SaveSystemController.LoadUser();

        if (data != null)
        {
            uinfo.isRegistered = data.isRegistered;
            if (data.isDoctor)
            {
                uinfo.userT = userType.Doctor;
            }
            else
            {
                uinfo.userT = userType.Patient;
            }

            uinfo.userName = data.Name;
            uinfo.userDNI = data.Dni;
            uinfo.docCode = data.DocId;

            uinfo.isTested = data.isTested;

            uinfo.patient_Info.Test = data.test;
            uinfo.patient_Info.Doctor = data.doctor;
            uinfo.patient_Info.Date = data.date;
            uinfo.patient_Info.Result_igm = data.resultIgm;
            uinfo.patient_Info.Result_igg = data.resultIgg;
        }

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
        SaveUser();
    }

    public void SaveStardardInfo(string n, string dni)
    {
        uinfo.AddStandardInfo(n, dni);
    }
    public void SaveDocStandardInfo(string n, string dni, string docId)
    {
        uinfo.AddStandardInfo(n, dni, docId);
    }

    public void SetResultInfo_igm(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.doc_temp.Result_igm = "+";
                break;
            case false:
                uinfo.doc_temp.Result_igm = "-";
                break;
        }
    }
    public void SetResultInfo_igg(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.doc_temp.Result_igg = "+";
                break;
            case false:
                uinfo.doc_temp.Result_igg = "-";
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
