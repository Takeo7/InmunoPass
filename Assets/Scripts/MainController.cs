using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Siguente: Añadir Email y Movil a idiomas y seguir con los New Test

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

#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif

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
            switch (data.userType_ud)
            {
                case 0:
                    uinfo.LabNICA = data.LabNica;
                    break;
                case 1:
                    uinfo.userDNI = data.Dni;
                    uinfo.docCode = data.DocId;
                    break;
                case 2:
                    uinfo.hospitalName = data.Hospital;
                    uinfo.DSACode = data.DSACode;
                    break;
                default:
                    break;
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
    public void SaveHospStandardInfo(string n, string dsa, string hospital)
    {
        uinfo.AddDSAInfo(n, dsa, hospital);
    }

    public void NewPatient()
    {
        uinfo.InitializeNewPatient();
    }

    #region Syntomatic
    public void SetSyntomatic(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.patient_temp.Sintomatic = "true";
                break;
            case false:
                uinfo.patient_temp.Sintomatic = "False";
                break;
        }
    }
    #endregion

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

    #region ResultInmuno
    public void SetResultInfo_Inmuno_igm(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddInmunoResult_IGM("+");
                break;
            case false:
                uinfo.AddInmunoResult_IGM("-");
                break;
        }
    }
    public void SetResultInfo_Inmuno_igg(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddInmunoResult_IGG("+");
                break;
            case false:
                uinfo.AddInmunoResult_IGG("-");
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

    #region ResultAntige
    public void SetResultInfo_Antige(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddAntigenosQResult("+");
                break;
            case false:
                uinfo.AddAntigenosQResult("-");
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

    #region DSA
    public void SetResultInfo_DSA(bool b)
    {
        switch (b)
        {
            case true:
                uinfo.AddDSAResult("+");
                break;
            case false:
                uinfo.AddDSAResult("-");
                break;
        }
    }
    #endregion


    public void CountryChanger(int i)
    {
        switch (i)
        {
            case 0:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 1:
                uinfo.patient_temp.PatientCountry = "USA";
                break;
            case 2:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 3:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 4:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 5:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 6:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 7:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 8:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 9:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;
            case 10:
                uinfo.patient_temp.PatientCountry = "ESP";
                break;

      

            default:
                break;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }






    public enum userType
    {
        Lab,
        Doctor,
        Hospital
    }

    public enum readType
    {
        QR,
        BarCode
    }
}
