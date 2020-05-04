using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    MainController mc;


    [SerializeField]
    Text inputPatientName;
    [SerializeField]
    Text inputPatientLastName;
    [SerializeField]
    Text inputPatientDNI;


    public void SavePatientInfo()
    {
        mc.SaveStardardInfo(inputPatientName.text + " " + inputPatientLastName.text, inputPatientDNI.text);
    }

}
