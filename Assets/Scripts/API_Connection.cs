using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API_Connection : MonoBehaviour
{

    [SerializeField]
    MainController mc;
    [SerializeField]
    UIController uic;
    [SerializeField]
    CameraCapture ccap;
    
   

    [Space]
    [SerializeField]
    string url;

    [SerializeField]
    private string token;

    [SerializeField]
    string partner;

    [Space]
    [SerializeField]
    Text _imageRec;

    [Space]
    [SerializeField]
    string jsonCOntent;
    [SerializeField]
    string jsonPlayload;
    [SerializeField]
    InputField nameInput;
    [SerializeField]
    InputField lastnameInput;
    [SerializeField]
    InputField idInput;
    [SerializeField]
    GameObject _LoadingScreen;

    [SerializeField]
    Text debugText;


    private void Start()
    {
        RequestToken();
    }

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
            formData.Add(new MultipartFormDataSection("paciente[nombre]", nameInput.text + "DEMO"));
            formData.Add(new MultipartFormDataSection("paciente[nif]", idInput.text + "DEMO"));

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

            if (mc.uinfo.patient_temp.PatientCountry == null || mc.uinfo.patient_temp.PatientCountry == "")
            {
                formData.Add(new MultipartFormDataSection("test[pais]", "USA"));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("test[pais]", mc.uinfo.patient_temp.PatientCountry));  
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

            if (mc.uinfo.patient_temp.PatientCountry == null || mc.uinfo.patient_temp.PatientCountry == "")
            {
                formData.Add(new MultipartFormDataSection("test[pais]", "USA"));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("test[pais]", mc.uinfo.patient_temp.PatientCountry));
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

            if (mc.uinfo.patient_temp.PatientCountry == null || mc.uinfo.patient_temp.PatientCountry == "")
            {
                formData.Add(new MultipartFormDataSection("test[pais]", "USA"));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("test[pais]", mc.uinfo.patient_temp.PatientCountry));
            }

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
        if (mc.uinfo.patient_temp.tests[3].valid)
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

            if (mc.uinfo.patient_temp.PatientCountry == null || mc.uinfo.patient_temp.PatientCountry == "")
            {
                formData.Add(new MultipartFormDataSection("test[pais]", "USA"));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("test[pais]", mc.uinfo.patient_temp.PatientCountry));
            }

            //formData.Add(new MultipartFormDataSection("test[fecha]", DateTime.Today.ToShortDateString()));//
            formData.Add(new MultipartFormDataSection("test[tipo]", "ANTIGENOS"));//Posibles valores: PCR, RAPIDO, ELISA
            formData.Add(new MultipartFormDataSection("test[resultado]", mc.uinfo.patient_temp.tests[3].testResult));



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
        if (mc.uinfo.patient_temp.tests[4].valid)
        {
            yield return StartCoroutine("TokenRequestAPI");

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("token", token));
            formData.Add(new MultipartFormDataSection("partner", partner));//

            Debug.Log("paciente[nombre]: " + mc.uinfo.patient_temp.PatientName +
                      "\npaciente[nif]: " + mc.uinfo.patient_temp.PatientDNI +
                      "\nmedico[nombre]: " + mc.uinfo.userName +
                      "\nmedico[referencia]: " + mc.uinfo.docCode +
                      "\ntest[igm]: " + mc.uinfo.patient_temp.tests[4].Result_igm +
                      "\ntest[igg]: " + mc.uinfo.patient_temp.tests[4].Result_igg);
            formData.Add(new MultipartFormDataSection("paciente[nombre]", nameInput.text + "DEMO"));
            formData.Add(new MultipartFormDataSection("paciente[nif]", idInput.text + "DEMO"));

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


            if (mc.uinfo.patient_temp.PatientCountry == null || mc.uinfo.patient_temp.PatientCountry == "")
            {
                formData.Add(new MultipartFormDataSection("test[pais]", "USA"));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("test[pais]", mc.uinfo.patient_temp.PatientCountry));
            }

            //formData.Add(new MultipartFormDataSection("test[fecha]", DateTime.Today.ToShortDateString()));//
            formData.Add(new MultipartFormDataSection("test[tipo]", "INMUNOCROMA"));//Posibles valores: PCR, RAPIDO, ELISA

            formData.Add(new MultipartFormDataSection("test[igm]", mc.uinfo.patient_temp.tests[4].Result_igm));
            formData.Add(new MultipartFormDataSection("test[igg]", mc.uinfo.patient_temp.tests[4].Result_igg));



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

    }


    public void ImageRecognition(string photo)
    {
        StartCoroutine("TokenImageRecognitionRequestAPI", photo);
    }

    IEnumerator TokenImageRecognitionRequestAPI(string photo)
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

            Debug.Log("Token Form Image Recongnition complete!");

            StartCoroutine("ImageRecognitionRequestAPI", photo);
        }
    }

    IEnumerator ImageRecognitionRequestAPI(string photo)
    {

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("token", token));
        formData.Add(new MultipartFormDataSection("source", "data:image/png;base64," + photo));

        UnityWebRequest www = UnityWebRequest.Post("https://app.immunitypass.es/klippa", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //OCRResponse response = OCRResponse.CreateFromJSON(jsonPlayload);// www.downloadHandler.text

            string[] s = www.downloadHandler.text.Split('{');

            string[] p = s[s.Length - 1].Split('}');

            string jsonContent = "{" + s[s.Length - 2] + "{" + p[0] + "}}";

            OCRResponsePlayload response = OCRResponsePlayload.CreateFromJSON(jsonContent);// www.downloadHandler.text
            //Debug.Log("Image Recognition: " + www.downloadHandler.text);
            Debug.Log(response.content.nombre + response.content.apellidos + "---" + response.content.numero_documento);
            nameInput.text = response.content.nombre;
            lastnameInput.text = response.content.apellidos;
            idInput.text = response.content.numero_documento;
            mc.uinfo.patient_Info.PatientCountry = response.content.pais;
            mc.uinfo.patient_temp.PatientCountry = response.content.pais;

            Debug.Log("Pais: "+mc.uinfo.patient_temp.PatientCountry);

            ccap.cCaptureGO.SetActive(false);
            _LoadingScreen.SetActive(false);

            debugText.text = jsonContent;

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

[Serializable]
public class OCRResponse //Image Recognition: {"success":true,"payload":{"content":{"tipo_documento":"PASSPORT","numero_documento":null,"pais":null,"nombre":null,"apellidos":null}}}
{
    public string success;
    public OCRResponsePlayload playload;

    public static OCRResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<OCRResponse>(jsonString);
    }
}

[Serializable]
public class OCRResponsePlayload
{
    public OCRResponseContent content;

    public static OCRResponsePlayload CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<OCRResponsePlayload>(jsonString);
    }

}

[Serializable]
public class OCRResponseContent
{
    public string tipo_documento;
    public string numero_documento;
    public string pais;
    public string nombre;
    public string apellidos;
}
