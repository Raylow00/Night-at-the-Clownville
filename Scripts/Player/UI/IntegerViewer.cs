using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntegerViewer : MonoBehaviour
{
    [Header("References to text object")]
    [SerializeField] private TextMeshProUGUI numField;

    public void DisplayNumber(int number)
    {
        numField.text = number.ToString();
    }
}
