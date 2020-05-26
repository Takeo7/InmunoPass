using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField]
    UIController uic;

    [SerializeField]
    Text t;

    [SerializeField]
    Idiomas.texto texto;

    void Start()
    {
        uic.delegateChangeLang += SetText;
        SetText();
    }

    public void SetText()
    {
        t.text = uic.GetText(texto);
    }
}
