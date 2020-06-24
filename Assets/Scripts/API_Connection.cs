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


    public void StartAPIConnection()
    {
        RequestToken();
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

            switch (mc.uinfo.userT)
            {
                case MainController.userType.Lab:
                    PostLabInfo();
                    break;
                case MainController.userType.Doctor:
                    PostDoctorInfo();
                    break;
                default:
                    break;
            }
            
        }
    }


    void PostLabInfo()
    {
        StartCoroutine("SaveInfoLabAPI");
    }


    IEnumerator SaveInfoLabAPI()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("token", token));
        Debug.Log("Info: " + mc.uinfo.lab_temp.PatientName);
        formData.Add(new MultipartFormDataSection("paciente[nombre]", mc.uinfo.lab_temp.PatientName + "DEMO"));
        formData.Add(new MultipartFormDataSection("paciente[nif]", mc.uinfo.lab_temp.PatientDNI + "DEMO"));
        formData.Add(new MultipartFormDataSection("medico[nombre]", mc.uinfo.userName + "DEMO"));
        formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.LabNICA + "DEMO"));
        formData.Add(new MultipartFormDataSection("test[igm]", mc.uinfo.lab_temp.Result_igm));
        formData.Add(new MultipartFormDataSection("test[igg]", mc.uinfo.lab_temp.Result_igg));



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

            uic.LabGenerateQR(url);
        }
    }


    void PostDoctorInfo()
    {
        StartCoroutine("SaveInfoDoctorAPI");
    }


    IEnumerator SaveInfoDoctorAPI()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("token", token));



        formData.Add(new MultipartFormDataSection("paciente[nombre]", mc.uinfo.doc_temp.PatientName + "DEMO"));
        formData.Add(new MultipartFormDataSection("paciente[nif]", mc.uinfo.doc_temp.PatientDNI + "DEMO"));

        formData.Add(new MultipartFormDataSection("medico[nombre]", mc.uinfo.userName + "DEMO"));
        formData.Add(new MultipartFormDataSection("medico[nif]", mc.uinfo.userDNI + "DEMO"));
        formData.Add(new MultipartFormDataSection("medico[referencia]", mc.uinfo.docCode + "DEMO"));
        formData.Add(new MultipartFormDataSection("test[lote]", mc.uinfo.doc_temp.Test + "DEMO"));

        formData.Add(new MultipartFormDataSection("test[igm]", mc.uinfo.doc_temp.Result_igm));
        formData.Add(new MultipartFormDataSection("test[igg]", mc.uinfo.doc_temp.Result_igg));



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

            uic.DoctorGenerateQR(url);
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
