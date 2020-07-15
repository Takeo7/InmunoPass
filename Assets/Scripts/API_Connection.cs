using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API_Connection : MonoBehaviour
{

    [SerializeField]
    MainController mc;
    [SerializeField]
    UIController uic;

    [Space]
    [SerializeField]
    string url;

    [SerializeField]
    private string token;

    [SerializeField]
    string partner;


    public void StartAPIConnection()
    {
        //RequestToken();
        SaveInfo_API();
    }


    void RequestToken()
    {
        StartCoroutine("TokenRequestAPI");
    }

    IEnumerator TokenRequestAPI()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("usr", "immunitypass"));
        formData.Add(new MultipartFormDataSection("pwd", "s.lp{{?CG|3>Yj8v"));

        UnityWebRequest www = UnityWebRequest.Post("https://app.immunitypass.es/token", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            TokenStructure response = TokenStructure.CreateFromJSON(www.downloadHandler.text);
            token = response.payload.token;

            Debug.Log("Token Form upload complete!");
            
        }
    }


    void SaveInfo_API()
    {
        StartCoroutine("SaveInfoLabAPI");
    }


    IEnumerator SaveInfoLabAPI()
    {
        if (mc.uinfo.patient_temp.tests[0].valid)
        {
            yield return StartCoroutine("TokenRequestAPI");

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("token", token));
            formData.Add(new MultipartFormDataSection("partner", partner));//

            Debug.Log("paciente[nombre]: " + mc.uinfo.patient_temp.PatientName +
                      "\npaciente[nif]: " + mc.uinfo.patient_temp.PatientDNI +
                      "\nmedico[nombre]: " + mc.uinfo.userName +
                      "\nmedico[referencia]: " + mc.uinfo.docCode +
                      "\ntest[igm]: " + mc.uinfo.patient_temp.tests[0].Result_igm +
                      "\ntest[igg]: " + mc.uinfo.patient_temp.tests[0].Result_igg);
            formData.Add(new MultipartFormDataSection("paciente[nombre]", mc.uinfo.patient_temp.PatientName + "DEMO"));
            formData.Add(new MultipartFormDataSection("paciente[nif]", mc.uinfo.patient_temp.PatientDNI + "DEMO"));

            formData.Add(new MultipartFormDataSection("medico[nombre]", mc.uinfo.userName + "DEMO"));
            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.LabNICA + "DEMO"));
                    break;
                case MainController.userType.Doctor:
                    formData.Add(new MultipartFormDataSection("medico[nif]", mc.uinfo.userDNI + "DEMO"));
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.docCode + "DEMO"));
                    break;
                default:
                    break;
            }

            //formData.Add(new MultipartFormDataSection("test[fecha]", DateTime.Today.ToShortDateString()));//
            formData.Add(new MultipartFormDataSection("test[tipo]", "RAPIDO"));//Posibles valores: PCR, RAPIDO, ELISA

            formData.Add(new MultipartFormDataSection("test[igm]", mc.uinfo.patient_temp.tests[0].Result_igm));
            formData.Add(new MultipartFormDataSection("test[igg]", mc.uinfo.patient_temp.tests[0].Result_igg));



            UnityWebRequest www = UnityWebRequest.Post("https://app.immunitypass.es/save", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                SaveStructure response = SaveStructure.CreateFromJSON(www.downloadHandler.text);
                if (response.success == "false")
                {
                    Debug.Log("Error API: " + response.error.message);
                }
                url = response.payload.url;
                Debug.Log("Save Form upload complete!");

                uic.GenerateQR(url);
            }
        }
        if (mc.uinfo.patient_temp.tests[1].valid)
        {
            yield return StartCoroutine("TokenRequestAPI");

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("token", token));
            formData.Add(new MultipartFormDataSection("partner", partner));//

            formData.Add(new MultipartFormDataSection("paciente[nombre]", mc.uinfo.patient_temp.PatientName + "DEMO"));
            formData.Add(new MultipartFormDataSection("paciente[nif]", mc.uinfo.patient_temp.PatientDNI + "DEMO"));

            formData.Add(new MultipartFormDataSection("medico[nombre]", mc.uinfo.userName + "DEMO"));
            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.LabNICA + "DEMO"));
                    break;
                case MainController.userType.Doctor:
                    formData.Add(new MultipartFormDataSection("medico[nif]", mc.uinfo.userDNI + "DEMO"));
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.docCode + "DEMO"));
                    break;
                default:
                    break;
            }
            

            //formData.Add(new MultipartFormDataSection("test[fecha]", DateTime.Today.ToShortDateString()));//
            formData.Add(new MultipartFormDataSection("test[tipo]", "PCR"));//Posibles valores: PCR, RAPIDO, ELISA
            formData.Add(new MultipartFormDataSection("test[resultado]", mc.uinfo.patient_temp.tests[1].testResult));



            UnityWebRequest www = UnityWebRequest.Post("https://app.immunitypass.es/save", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                SaveStructure response = SaveStructure.CreateFromJSON(www.downloadHandler.text);
                url = response.payload.url;
                Debug.Log("Save Form upload complete!");

                uic.GenerateQR(url);
            }
        }
        if (mc.uinfo.patient_temp.tests[2].valid)
        {
            yield return StartCoroutine("TokenRequestAPI");

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();           
            formData.Add(new MultipartFormDataSection("token", token));
            formData.Add(new MultipartFormDataSection("partner", partner));//

            formData.Add(new MultipartFormDataSection("paciente[nombre]", mc.uinfo.patient_temp.PatientName + "DEMO"));
            formData.Add(new MultipartFormDataSection("paciente[nif]", mc.uinfo.patient_temp.PatientDNI + "DEMO"));

            formData.Add(new MultipartFormDataSection("medico[nombre]", mc.uinfo.userName + "DEMO"));
            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.LabNICA + "DEMO"));
                    break;
                case MainController.userType.Doctor:
                    formData.Add(new MultipartFormDataSection("medico[nif]", mc.uinfo.userDNI + "DEMO"));
                    formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.docCode + "DEMO"));
                    break;
                default:
                    break;
            }

            //formData.Add(new MultipartFormDataSection("test[fecha]", DateTime.Today.ToShortDateString()));//
            formData.Add(new MultipartFormDataSection("test[tipo]", "ELISA"));//Posibles valores: PCR, RAPIDO, ELISA

            formData.Add(new MultipartFormDataSection("test[igm]", mc.uinfo.patient_temp.tests[2].Result_igm));
            formData.Add(new MultipartFormDataSection("test[igg]", mc.uinfo.patient_temp.tests[2].Result_igg));

            formData.Add(new MultipartFormDataSection("test[valor_igm]", mc.uinfo.patient_temp.tests[2].Valor_igm));
            formData.Add(new MultipartFormDataSection("test[valor_igg]", mc.uinfo.patient_temp.tests[2].Valor_igg));



            UnityWebRequest www = UnityWebRequest.Post("https://app.immunitypass.es/save", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                SaveStructure response = SaveStructure.CreateFromJSON(www.downloadHandler.text);
                url = response.payload.url;
                Debug.Log("Save Form upload complete!");

                uic.GenerateQR(url);
            }
        }

    }
}





[Serializable]
public class TokenStructure
{
    public string success;
    public TokenPayloadStructure payload;

    public static TokenStructure CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<TokenStructure>(jsonString);
    }
}
[Serializable]
public class TokenPayloadStructure
{
    public string token;
    public string expires;
}



[Serializable]
public class SaveStructure
{
    public string success;
    public SavePayloadStructure payload;
    public SaveErrorStructure error;

    public static SaveStructure CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SaveStructure>(jsonString);
    }
}

[Serializable]
public class SavePayloadStructure
{
    public string id_test;
    public string serial;
    public string url;
}
[Serializable]
public class SaveErrorStructure
{
    public string code;
    public string message;
}
