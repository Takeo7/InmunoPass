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
                uinfo.userDNI = data.Dni;
                uinfo.docCode = data.DocId;
            }
            else
            {
                uinfo.userT = userType.Lab;
                uinfo.LabNICA = data.LabNica;
            }

            uinfo.userName = data.Name;
            
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
            switch (uinfo.userT)
            {
                case userType.Lab:
                    uiC.LabAlreadyRegistered();
                    break;
                case userType.Doctor:
                    uiC.DoctorAlreadyRegistered();
                    break;
                default:
                    break;
            }

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
            case userType.Lab:
                uiC.EndQRScan();
                break;
            case userType.Doctor:
                switch (read_Type)
                {
                    case readType.QR:

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

    public void SaveLabStardardInfo(string n, string nica)
    {
        uinfo.AddStandardInfo(n, nica);
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
        Lab,
        Doctor
    }

    public enum readType
    {
        QR,
        BarCode
    }
}
