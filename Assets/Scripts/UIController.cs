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
    [Header("Patient Input")]
    [SerializeField]
    Text inputPatientName;
    [SerializeField]
    Text inputPatientLastName;
    [SerializeField]
    Text inputPatientDNI;

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
    [Header("Patient Info Visualization")]
    [SerializeField]
    Text PatientName;
    [SerializeField]
    Text PatientDNI;

    [SerializeField]
    GameObject TestInfo;
    [SerializeField]
    Text PatientTestLot;
    [SerializeField]
    Text PatientTestDate;
    [SerializeField]
    Text PatientDocTester;
    [SerializeField]
    Text PatientTestResult_igm;
    [SerializeField]
    Text PatientTestResult_igg;

    [Space]
    [Header("Doctor Info Visualization")]
    [SerializeField]
    Text DocName;
    [SerializeField]
    Text DocDNI;
    [SerializeField]
    Text DocID;

    [Space]
    [SerializeField]
    Text newPatientName;
    [SerializeField]
    Text newPatientDNI;
    [SerializeField]
    Text newPatientTest;
    [SerializeField]
    Text newPatientDate;
    [SerializeField]
    GameObject newTestScan;
    [SerializeField]
    GameObject newResult;
    [SerializeField]
    GameObject newPatientGenerateQR;
    


    [Space]
    [Header("Canvas Windows")]
    [SerializeField]
    GameObject UserSelection;
    [SerializeField]
    GameObject ShowQR;

    [Space]
    [SerializeField]
    GameObject PatientInput;
    [SerializeField]
    GameObject PatientInfo;

    [Space]
    [SerializeField]
    GameObject DoctorInput;
    [SerializeField]
    GameObject DoctorInfo;
    [SerializeField]
    GameObject NewPatient;

    private void Start()
    {
        mc.qrCreator.onQREncodeFinished += PatientQRLoad;
        mc.qrCreator.onQREncodeFinished += DoctorQRLoad;

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
        if (uInfo.userT == MainController.userType.Patient)
        {
            DeviceCamera.SetActive(false);
            ScanLayer.SetActive(false);

            PatientInfo.SetActive(true);
            FillPatientInfo();
        }
        else
        {
            switch (mc.read_Type)
            {
                case MainController.readType.QR:
                    DeviceCamera.SetActive(false);
                    ScanLayer.SetActive(false);

                    NewPatient.SetActive(true);
                    FillDocNewPatientInfo();
                    break;
                case MainController.readType.BarCode:
                    DeviceCamera.SetActive(false);
                    ScanLayer.SetActive(false);

                    NewPatient.SetActive(true);
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

    #region Patient
    public void SavePatientInfo()
    {
        mc.SaveStardardInfo(inputPatientName.text + " " + inputPatientLastName.text, inputPatientDNI.text);
    }
    public void PatientAlreadyRegistered()
    {
        PatientInfo.SetActive(true);
        FillPatientInfo();

        UserSelection.SetActive(false);
    }
    public void FillPatientInfo()
    {
        if (uInfo.CheckIsRegistered())
        {
            PatientName.text = uInfo.userName;
            PatientDNI.text = uInfo.userDNI;

            if (uInfo.CheckIsTested())
            {
                TestInfo.SetActive(true);
                PatientTestLot.text = uInfo.patient_Info.Test;
                PatientTestDate.text = uInfo.patient_Info.Date;
                PatientDocTester.text = uInfo.patient_Info.Doctor;
                PatientTestResult_igm.text = uInfo.patient_Info.Result_igm;
                if (PatientTestResult_igm.text == "-")
                {
                    PatientTestResult_igm.color = negative;
                }
                else
                {
                    PatientTestResult_igm.color = positive;
                }
                PatientTestResult_igg.text = uInfo.patient_Info.Result_igg;
                if (PatientTestResult_igg.text == "-")
                {
                    PatientTestResult_igg.color = negative;
                }
                else
                {
                    PatientTestResult_igg.color = positive;
                }
            }
            else
            {
                TestInfo.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("No info to fill");
        }
    }
    public void PatientGenerateQR()
    {
        string textToEncode = uInfo.userName + "%" + uInfo.userDNI;
        
        mc.qrCreator.Encode(textToEncode);
        
    }

    public void PatientQRLoad(Texture2D t)
    {
        ShowQR.SetActive(true);

        qrImage.texture = t;
    }

    #endregion

    public void FillDoctorInfo()
    {
        if (uInfo.CheckIsRegistered())
        {
            DocName.text = uInfo.userName;
            DocDNI.text = uInfo.userDNI;
            DocID.text = uInfo.docCode;
        }
    }

    public void DoctorAlreadyRegistered()
    {
        DoctorInfo.SetActive(true);
        FillDoctorInfo();

        UserSelection.SetActive(false);
    }

    public void SaveDoctorInfo()
    {
        mc.SaveDocStandardInfo("Dr. " + inputDocName.text + " " + inputDocLastName.text, inputDocDNI.text, inputDocID.text);
    }
    
    public void FillDocNewPatientInfo()
    {
        newPatientName.text = uInfo.doc_temp.PatientName;
        newPatientDNI.text = uInfo.doc_temp.PatientDNI;

        newTestScan.SetActive(true);
    }

    public void FillDocNewTestInfo()
    {
        newPatientTest.text = uInfo.doc_temp.Test;
        newPatientDate.text = DateTime.Today.ToString();

        newResult.SetActive(true);
    }

    public void DoctorGenerateQR()
    {
        string textToEncode = uInfo.userName + "%" + uInfo.doc_temp.Test + "%" + uInfo.doc_temp.Date + "%" + uInfo.doc_temp.Result_igm + "%" + uInfo.doc_temp.Result_igg;

        mc.qrCreator.Encode(textToEncode);

    }

    public void DoctorQRLoad(Texture2D t)
    {
        ShowQR.SetActive(true);

        qrImage.texture = t;
    }

}
