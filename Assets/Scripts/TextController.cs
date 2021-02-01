using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField]
    UIController uic;

    Text t;

    TextMeshProUGUI tm;

    [SerializeField]
    Idiomas.texto texto;

    void Start()
    {
        t = GetComponent<Text>();

        if(t == null)
        {
            tm = GetComponent<TextMeshProUGUI>();
        }

        uic.delegateChangeLang += SetText;
        SetText();
    }

    public void SetText()
    {
        if (t != null)
        {
            t.text = uic.GetText(texto); 
        }
        else if(tm != null)
        {
            tm.text = uic.GetText(texto);
        }
    }
}
