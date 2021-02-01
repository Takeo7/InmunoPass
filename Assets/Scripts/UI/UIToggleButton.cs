using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Content;

    [SerializeField]
    private RectTransform m_Layout;

    public void Toggle()
    {
        m_Content.SetActive(!m_Content.activeSelf);
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_Layout);
    }
}
