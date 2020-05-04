using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

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
    Color Pass;
    [SerializeField]
    Color NotPass;

    [Space]
    [SerializeField]
    RawImage qrImage;

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


    [Space]
    [Header("Patient Info Visualization")]
    [SerializeField]
    Text PatientName;
    [SerializeField]
    Text PatientDNI;
    [SerializeField]
    Text PatientTestLot;
    [SerializeField]
    Text PatientTestDate;
    [SerializeField]
    Text PatientDocTester;
    [SerializeField]
    Text PatientTestResult;

    [Space]
    [Header("Doctor Info Visualization")]
    [SerializeField]
    Text DocName;
    [SerializeField]
    Text DocDNI;
    


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

    private void Start()
    {
        mc.qrCreator.onQREncodeFinished += PatientQRLoad;
    }

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
            DeviceCamera.SetActive(false);
            ScanLayer.SetActive(false);


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
                PatientTestLot.text = uInfo.patient_Info.Test;
                PatientTestDate.text = uInfo.patient_Info.Date;
                PatientDocTester.text = uInfo.patient_Info.Doctor;
                PatientTestResult.text = uInfo.patient_Info.Result;
            }
        }
        else
        {
            Debug.LogError("No info to fill");
        }
    }
    public void PatientGenerateQR()
    {
        string textToEncode = uInfo.userName + "/" + uInfo.userDNI;
        
        mc.qrCreator.Encode(textToEncode);
        
    }

    public void PatientQRLoad(Texture2D t)
    {
        PatientInfo.SetActive(false);
        ShowQR.SetActive(true);

        qrImage.texture = t;
    }

    #endregion

    public void SaveDoctorInfo()
    {
        mc.SaveStardardInfo("Dr." + inputDocName.text + " " + inputDocLastName.text, inputDocDNI.text);
    }
    public void DocAlreadyRegistered()
    {
        DoctorInfo.SetActive(true);
        UserSelection.SetActive(false);
    }

}
