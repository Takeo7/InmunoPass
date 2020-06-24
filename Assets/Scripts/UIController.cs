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
    [SerializeField]
    RectTransform layoutVDoc;

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
    [SerializeField]
    RectTransform layoutVLab;



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
        if (mc.uinfo.userT == MainController.userType.Lab)
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


    public void StartUI()
    {
        if (!mc.uinfo.isRegistered)
        {
            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    LabInput.SetActive(true);
                    break;
                case MainController.userType.Doctor:
                    DoctorInput.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    LabAlreadyRegistered();
                    break;
                case MainController.userType.Doctor:
                    DoctorAlreadyRegistered();
                    break;
                default:
                    break;
            }

        }
    }


   

    public void FillDoctorInfo()
    {
        if (mc.uinfo.CheckIsRegistered())
        {
            DocName.text = mc.uinfo.userName;
            DocDNI.text = mc.uinfo.userDNI;
            DocID.text = mc.uinfo.docCode;
        }
    }

    public void FillLabInfo()
    {
        if (mc.uinfo.CheckIsRegistered())
        {
            LabName.text = mc.uinfo.userName;
            LabNica.text = mc.uinfo.LabNICA;
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

        mc.uinfo.AddDocPatient(newPatientDocName.text, newPatientDocDNI.text);

        newTestDocScan.SetActive(true);
    }

    public void FillLabNewPatientInfo()
    {
         mc.uinfo.lab_temp.PatientName = newPatientLabName.text;
         mc.uinfo.lab_temp.PatientDNI = newPatientLabDNI.text;
    }



    public void FillDocNewTestInfo()
    {
        
        newPatientDocTest.text = mc.uinfo.doc_temp.Test;
        newPatientDocDate.text = DateTime.Today.ToShortDateString();

        newDocResult.SetActive(true);
    }

    public void DoctorGenerateQR(string url)
    {
        string textToEncode = url;

        mc.qrCreator.Encode(textToEncode);
    }

    public void LabGenerateQR(string url)
    {
        string textToEncode = url;

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

    public void BackScannReader()
    {
        switch (mc.uinfo.userT)
        {
            case MainController.userType.Lab:
                LabNewPatient.SetActive(true);
                break;
            case MainController.userType.Doctor:
                DocNewPatient.SetActive(true);
                break;
            default:
                break;
        }
    }


    public void GeneralTrigger(GameObject _go)
    {
        _go.SetActive(!_go.activeSelf);
        UpdateLayouts();
    }

    public void UpdateLayouts()
    {
        switch (mc.uinfo.userT)
        {
            case MainController.userType.Lab:
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutVLab);
                break;
            case MainController.userType.Doctor:
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutVDoc);
                break;
            default:
                break;
        }
    }

}
