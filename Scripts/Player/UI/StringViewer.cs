using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StringViewer : MonoBehaviour
{
    [Header("References to text object")]
    [SerializeField] private TextMeshProUGUI stringField;

    public void DisplayString(string str)
    {
        stringField.text = str;
    }

    public void HideString(string str)
    {
        stringField.text = str;
    }
}
