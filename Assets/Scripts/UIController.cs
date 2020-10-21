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
    GameObject[] qrShow;
    [SerializeField]
    RawImage[] qrImage;

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
    [Header("Hospital Input")]
    [SerializeField]
    Text inputHName;
    [SerializeField]
    Text inputHLastName;
    [SerializeField]
    Text inputHospital;
    [SerializeField]
    Text inputDSACode;
  

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
    [Header("Hospital Info Visualization")]
    [SerializeField]
    Text HospitalDocName;
    [SerializeField]
    Text HospitalName;
    [SerializeField]
    Text DSA_code;

    [Space]
    [Header("New Test")]
    [SerializeField]
    Text newPatientLabName;
    [SerializeField]
    Text newPatientLabLastName;
    [SerializeField]
    Text newPatientLabDNI;
    [SerializeField]
    Text newPatientEmail;
    [SerializeField]
    Text newPatientPhone;
    [SerializeField]
    Text newPatientLabDate;
    [SerializeField]
    GameObject newLabResult;
    [SerializeField]
    GameObject newPatientLabGenerateQR;
    [SerializeField]
    RectTransform layoutVLab;

    [Space]
    [Header("TestTypesResult")]
    [SerializeField]
    GameObject[] testTypesResultGO;

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

    [Space]
    [SerializeField]
    GameObject HospitalInput;
    [SerializeField]
    GameObject HospitalInfo;

    [Space]
    [SerializeField]
    GameObject NewTest;

    [Space]
    [SerializeField]
    GameObject GenerateQRGO;
    [SerializeField]
    public Texture2D[] qrTextures;


    private void Start()
    {
        mc.qrCreator.onQREncodeFinished += QRLoad;

        if (PlayerPrefs.GetInt("LanguageSet") == 0)
        {
            SetStartLanguage();
        }
        else
        {
            ChangeLanguage(PlayerPrefs.GetInt("Idioma"));
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
            case SystemLanguage.Arabic:
                lang.ChangeLang(Idiomas.idiomasEnum.Arabe);
                delegateChangeLang();
                break;
            case SystemLanguage.German:
                lang.ChangeLang(Idiomas.idiomasEnum.Aleman);
                delegateChangeLang();
                break;
            case SystemLanguage.Italian:
                lang.ChangeLang(Idiomas.idiomasEnum.Italiano);
                delegateChangeLang();
                break;
            case SystemLanguage.French:
                lang.ChangeLang(Idiomas.idiomasEnum.Frances);
                delegateChangeLang();
                break;
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                lang.ChangeLang(Idiomas.idiomasEnum.Chino);
                delegateChangeLang();
                break;
            case SystemLanguage.Portuguese:
                lang.ChangeLang(Idiomas.idiomasEnum.Portugues);
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
        PlayerPrefs.SetInt("Idioma", i);
        PlayerPrefs.SetInt("LanguageSet", 1);
        delegateChangeLang();
        
    }

    public string GetText(Idiomas.texto t)
    {
       return  lang.GetText(t);
    }

    #endregion




    //public void EndQRScan()
    //{        
    //    if (mc.uinfo.userT == MainController.userType.Lab)
    //    {
    //        DeviceCamera.SetActive(false);
    //        ScanLayer.SetActive(false);


            
    //    }
    //    else
    //    {
    //        switch (mc.read_Type)
    //        {
    //            case MainController.readType.QR:
    //                DeviceCamera.SetActive(false);
    //                ScanLayer.SetActive(false);

    //                DocNewPatient.SetActive(true);
    //                FillDocNewPatientInfo();
    //                break;
    //            case MainController.readType.BarCode:
    //                DeviceCamera.SetActive(false);
    //                ScanLayer.SetActive(false);

    //                DocNewPatient.SetActive(true);
    //                FillDocNewTestInfo();
    //                break;
    //            default:
    //                break;
    //        }
            

    //    }
    //}


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
                case MainController.userType.Hospital:
                    HospitalInput.SetActive(true);
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
                case MainController.userType.Hospital:
                    HospitalAlreadyRegistered();
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

    public void FillDSAInfo()
    {
        if (mc.uinfo.CheckIsRegistered())
        {
            HospitalDocName.text = mc.uinfo.userName;

            HospitalName.text = mc.uinfo.hospitalName;
            DSA_code.text = mc.uinfo.DSACode;
            
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

    public void HospitalAlreadyRegistered()
    {
        HospitalInfo.SetActive(true);
        FillDSAInfo();
    }

    public void SaveDoctorInfo()
    {
        mc.SaveDocStandardInfo("Dr. " + inputDocName.text + " " + inputDocLastName.text, inputDocDNI.text, inputDocID.text);
    }

    public void SaveLabInfo()
    {
        mc.SaveLabStardardInfo(inputLabName.text, inputNica.text);
    }

    public void SaveDSAInfo()
    {
        mc.SaveHospStandardInfo("Dr. " + inputHName.text + " " + inputHLastName.text, inputDSACode.text, inputHospital.text);
    }
   

    public void FillNewPatientInfo()
    {
        mc.uinfo.patient_temp.PatientName = newPatientLabName.text + "" + newPatientLabLastName.text;
        mc.uinfo.patient_temp.PatientDNI = newPatientLabDNI.text;

        mc.uinfo.patient_temp.PatientEmail = newPatientEmail.text;
        mc.uinfo.patient_temp.PatientPhone = newPatientPhone.text;
    }

    public void GenerateQR(string url)
    {
        Debug.Log(url);
        string textToEncode = url;

        mc.qrCreator.Encode(textToEncode);
    }

    public void QRLoad(Texture2D t)
    {
        ShowQR.SetActive(true);

        int length = qrShow.Length;
        for (int i = 0; i < length; i++)
        {
            if (qrTextures[i] == null)
            {
                qrTextures[i] = t;
                qrShow[i].SetActive(true);
                qrImage[i].texture = qrTextures[i];
                break;
            }

        }
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

    public void ShowGenerateQR()
    {
        GenerateQRGO.SetActive(true);
    }


    public void GeneralTrigger(GameObject _go)
    {
        _go.SetActive(!_go.activeSelf);
        UpdateLayouts();
    }

    public void UpdateLayouts()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutVLab);
    }

    public void UpdateResultTestsVisual(int test)
    {
        int length = testTypesResultGO.Length;
        for (int i = 0; i < length; i++)
        {
            if (i != test)
            {
                testTypesResultGO[i].SetActive(false);
            }       
        }
    }

}
