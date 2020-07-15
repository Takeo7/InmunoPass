﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static MainController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public QRCodeDecodeController qrController;
    public QRCodeEncodeController qrCreator;

    [SerializeField]
    public UserInfo uinfo;
    [SerializeField]
    public UIController uiC;

    [Space]
    [SerializeField]
    public readType read_Type;


    void Start()
    {
        //string path = Application.persistentDataPath + "/userData.pass";
        //Debug.Log("Path: " + path);
        LoadUser();

        //qrController.onQRScanFinished += SaveScanInfo;
        


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
                //uinfo.userT = userType.Doctor;
                uinfo.userDNI = data.Dni;
                uinfo.docCode = data.DocId;
            }
            else
            {
                //uinfo.userT = userType.Lab;
                uinfo.LabNICA = data.LabNica;
            }

            uinfo.userName = data.Name;
            
        }


        uiC.StartUI();

    }

    public void ResetInfo()
    {
        uinfo.ResetInfo();
        SaveSystemController.DeleteInfo();
    }

    public UserInfo GetUserScriptable()
    {
        return uinfo;
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





    /*public void SaveScanInfo(string scan)
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
                        //uinfo.AddDocTest(scan);
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
    }*/

    public void SaveLabStardardInfo(string n, string nica)
    {
        uinfo.AddStandardInfo(n, nica);
    }
    public void SaveDocStandardInfo(string n, string dni, string docId)
    {
        uinfo.AddStandardInfo(n, dni, docId);
    }

    public void NewPatient()
    {
        uinfo.InitializeNewPatient();
    }

    #region ResultRapido
    public void SetResultInfo_RAPIDO_igm(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddRAPIDOResult_IGM("+");
                break;
            case false:
                uinfo.AddRAPIDOResult_IGM("-");
                break;
        }
    }
    public void SetResultInfo_RAPIDO_igg(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddRAPIDOResult_IGG("+");
                break;
            case false:
                uinfo.AddRAPIDOResult_IGG("-");
                break;
        }
    }
    #endregion

    #region ResultPCR
    public void SetResultInfo_PCR(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddPCRResult("+");
                break;
            case false:
                uinfo.AddPCRResult("-");
                break;
        }
    }
    #endregion

    #region ResultElisa
    public void SetResultInfo_ELISA_igm(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddELISAResult_igm("+");
                break;
            case false:
                uinfo.AddELISAResult_igm("-");
                break;
        }
    }

    public void SetResultInfo_ELISA_igg(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddELISAResult_igg("+");
                break;
            case false:
                uinfo.AddELISAResult_igg("-");
                break;
        }
    }

    public void SetResultInfo_ELISA_V_igm(Text v_igm)
    {
        uinfo.AddELISAResult_Valorigm(v_igm.text);
    }

    public void SetResultInfo_ELISA_V_igg(Text v_igg)
    {
        uinfo.AddELISAResult_Valorigg(v_igg.text);
    }
#endregion


    public void ResetScene()
    {
        SceneManager.LoadScene(0);
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
