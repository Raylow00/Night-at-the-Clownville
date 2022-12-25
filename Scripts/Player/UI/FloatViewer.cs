using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatViewer : MonoBehaviour
{
    [Header("References to text object")]
    [SerializeField] private TextMeshProUGUI floatField;

    public void DisplayFloat(float number)
    {
        floatField.text = Mathf.RoundToInt(number).ToString();
    }
}
