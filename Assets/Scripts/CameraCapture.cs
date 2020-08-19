using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;
using System.Xml.XPath;

public class CameraCapture : MonoBehaviour
{

    [SerializeField]
    API_Connection _api;

    [Space]
    [SerializeField]
    RawImage showCamera;
    [SerializeField]
    RawImage showSnap;

    [Space]
    [SerializeField]
    string s;

    WebCamTexture camTexture;

    [Space]
    public Text nombre;
    public Text n_identificacion;
    public GameObject cCaptureGO;

    private void Start()
    {
        camTexture = new WebCamTexture();

        showCamera.texture = camTexture;

        camTexture.Play();
    }

    public void StartSnapshoot()
    {
        string photo = TakeSnapshoot();
        s = photo;
        Debug.Log(photo);
        //System.IO.File.WriteAllText("C:\\Users\\arthu\\Documents\\yourtextfile.txt", photo);

        _api.ImageRecognition(s);
    }

    public string TakeSnapshoot()
    {
        Texture2D snap = new Texture2D(camTexture.width, camTexture.height);
        snap.SetPixels(camTexture.GetPixels());
        snap.Apply();

        showSnap.texture = snap;

        byte[] bytes;

        bytes = snap.EncodeToPNG();
        

        string enc = Convert.ToBase64String(bytes);

        return enc;
    }

    // Take a "screenshot" of a camera's Render Texture.
    Texture2D RTImage()
    {
        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camTexture.width, camTexture.height);
        
        image.Apply();
        camTexture.Stop();
        

        return showCamera.texture as Texture2D;
    }
}
