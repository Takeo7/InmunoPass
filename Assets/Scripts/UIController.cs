using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    public Text DEBUG;

    [SerializeField]
    MainController mc;
    [SerializeField]
    UserInfo uInfo;
    [Space]
    [SerializeField]
    GameObject DeviceCamera;
    [SerializeField]
    GameObject ScanLayer;

    [Space]
    [SerializeField]
    Color negative;
    [SerializeField]
    Color positive;

    [Space]
    [SerializeField]
    RawImage qrImage;

    [Space]
    [SerializeField]
    Idiomas lang;


    [Space]
    [Header("Doctor Input")]
    [SerializeField]
    Text inputDocName;
    [SerializeField]
    Text inputDocLastName;
    [SerializeField]
    Text inputDocDNI;
    [SerializeField]
    Text inputDocID;

    [Space]
    [Header("Lab Input")]
    [SerializeField]
    Text inputLabName;
    [SerializeField]
    Text inputNica;
  

    [Space]
    [Header("Doctor Info Visualization")]
    [SerializeField]
    Text DocName;
    [SerializeField]
    Text DocDNI;
    [SerializeField]
    Text DocID;


    [Space]
    [Header("Lab Info Visualization")]
    [SerializeField]
    Text LabName;
    [SerializeField]
    Text LabNica;

    [Space]
    [Header("New Test Doc")]
    [SerializeField]
    Text newPatientDocName;
    [SerializeField]
    Text newPatientDocDNI;
    [SerializeField]
    Text newPatientDocTest;
    [SerializeField]
    Text newPatientDocDate;
    [SerializeField]
    GameObject newTestDocScan;
    [SerializeField]
    GameObject newDocResult;
    [SerializeField]
    GameObject newPatientDocGenerateQR;

    [Space]
    [Header("New Test Lab")]
    [SerializeField]
    Text newPatientLabName;
    [SerializeField]
    Text newPatientLabDNI;
    [SerializeField]
    Text newPatientLabDate;
    [SerializeField]
    GameObject newLabResult;
    [SerializeField]
    GameObject newPatientLabGenerateQR;



    [Space]
    [Header("Canvas Windows")]
    [SerializeField]
    GameObject UserSelection;
    [SerializeField]
    GameObject ShowQR;



    [Space]
    [SerializeField]
    GameObject DoctorInput;
    [SerializeField]
    GameObject DoctorInfo;
    [SerializeField]
    GameObject DocNewPatient;


    [Space]
    [SerializeField]
    GameObject LabInput;
    [SerializeField]
    GameObject LabInfo;
    [SerializeField]
    GameObject LabNewPatient;


    private void Start()
    {
        mc.qrCreator.onQREncodeFinished += QRLoad;

        if (PlayerPrefs.GetInt("LanguageSet") == 0)
        {
            SetStartLanguage();
        }

        if (!uInfo.isRegistered)
        {
            DoctorInput.SetActive(true);
            uInfo.userT = MainController.userType.Doctor;
        }
        else
        {
            DoctorAlreadyRegistered();
        }
    }

    #region Language

    public delegate void changelang();
    public changelang delegateChangeLang;

    void SetStartLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Catalan:
                lang.ChangeLang(Idiomas.idiomasEnum.Catalan);
                delegateChangeLang();
                break;
            case SystemLanguage.English:
                lang.ChangeLang(Idiomas.idiomasEnum.Ingles);
                delegateChangeLang();
                break;
            case SystemLanguage.Spanish:
                lang.ChangeLang(Idiomas.idiomasEnum.Español);
                delegateChangeLang();
                break;
            default:
                lang.ChangeLang(Idiomas.idiomasEnum.Español);
                delegateChangeLang();
                break;
        }
        PlayerPrefs.SetInt("LanguageSet", 1);
    }

    public void ChangeLanguage(int i)
    {
        lang.ChangeLang((Idiomas.idiomasEnum)i);
        delegateChangeLang();
    }

    public string GetText(Idiomas.texto t)
    {
       return  lang.GetText(t);
    }

    #endregion

    public void EndQRScan()
    {        
        if (uInfo.userT == MainController.userType.Lab)
        {
            DeviceCamera.SetActive(false);
            ScanLayer.SetActive(false);


            
        }
        else
        {
            switch (mc.read_Type)
            {
                case MainController.readType.QR:
                    DeviceCamera.SetActive(false);
                    ScanLayer.SetActive(false);

                    DocNewPatient.SetActive(true);
                    FillDocNewPatientInfo();
                    break;
                case MainController.readType.BarCode:
                    DeviceCamera.SetActive(false);
                    ScanLayer.SetActive(false);

                    DocNewPatient.SetActive(true);
                    FillDocNewTestInfo();
                    break;
                default:
                    break;
            }
            

        }
    }

    public void LoadUserInfo()
    {
       uInfo = mc.GetUserScriptable();
    }

   

    public void FillDoctorInfo()
    {
        if (uInfo.CheckIsRegistered())
        {
            DocName.text = uInfo.userName;
            DocDNI.text = uInfo.userDNI;
            DocID.text = uInfo.docCode;
        }
    }

    public void FillLabInfo()
    {
        if (uInfo.CheckIsRegistered())
        {
            LabName.text = uInfo.userName;
            LabNica.text = uInfo.LabNICA;
        }
    }

    public void DoctorAlreadyRegistered()
    {
        DoctorInfo.SetActive(true);
        FillDoctorInfo();    
    }
    public void LabAlreadyRegistered()
    {
        LabInfo.SetActive(true);
        FillLabInfo();
    }

    public void SaveDoctorInfo()
    {
        mc.SaveDocStandardInfo("Dr. " + inputDocName.text + " " + inputDocLastName.text, inputDocDNI.text, inputDocID.text);
    }

    public void SaveLabInfo()
    {
        mc.SaveLabStardardInfo(inputLabName.text, inputNica.text);
    }
    
    public void FillDocNewPatientInfo()
    {
        newPatientDocName.text = uInfo.doc_temp.PatientName;
        newPatientDocDNI.text = uInfo.doc_temp.PatientDNI;

        newTestDocScan.SetActive(true);
    }

    public void FillLabNewPatientInfo()
    {
        newPatientLabName.text = uInfo.lab_temp.PatientName;
        newPatientLabDNI.text = uInfo.lab_temp.PatientDNI;

        newLabResult.SetActive(true);
    }



    public void FillDocNewTestInfo()
    {
        newPatientDocTest.text = uInfo.doc_temp.Test;
        newPatientDocDate.text = DateTime.Today.ToString();

        newDocResult.SetActive(true);
    }

    public void DoctorGenerateQR()
    {
        string textToEncode = "";

        mc.qrCreator.Encode(textToEncode);
    }

    public void LabGenerateQR()
    {
        string textToEncode = "";

        mc.qrCreator.Encode(textToEncode);
    }

    public void DoctorGenerateQRDEMO()
    {
        string textToEncode = "https://app.immunitypass.es/demo?" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

        mc.qrCreator.Encode(textToEncode);

    }

    public void QRLoad(Texture2D t)
    {
        ShowQR.SetActive(true);

        qrImage.texture = t;
    }

}
