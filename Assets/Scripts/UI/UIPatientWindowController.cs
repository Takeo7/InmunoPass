using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPatientWindowController : MonoBehaviour
{
    [SerializeField]
    private Text m_PatientName;

    [SerializeField]
    private Text m_PatientLastName;

    [SerializeField]
    private Text m_PatientDNI;

    [SerializeField]
    private Text m_PatientPhone;

    [SerializeField]
    private Text m_PatientEmail;

    public void FillNewPatientInfo()
    {
        var patient = new PatientInfo()
        {
            PatientName = m_PatientName.text + " " + m_PatientLastName.text,
            PatientDNI = m_PatientDNI.text,
            PatientPhone = m_PatientPhone.text,
            PatientEmail = m_PatientEmail.text
        };

        UIController.Instance.FillNewPatientInfo(patient);
    }
}
