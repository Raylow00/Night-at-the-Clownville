using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatSlider : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider slider;
    [SerializeField] private float maxFloat = 100f;

    [Header("Image")]
    [SerializeField] private Image sliderImage;
    
    [Header("Color")]
    [SerializeField] private Color zeroColor;
    [SerializeField] private Color fullColor;

    public void DisplaySliderValue(float number)
    {
        slider.value = number;
    }

    public void DisplaySliderValue_Percentage_Color(float number)
    {
        float currentNumberPercentage = CalculateNumberPercentage(number, maxFloat);
        slider.value = (int)currentNumberPercentage;
        sliderImage.color = Color.Lerp(zeroColor, fullColor, currentNumberPercentage / maxFloat);
    }

    private float CalculateNumberPercentage(float currentNumber, float maxNumber)
    {
        return ((currentNumber / maxNumber) * 100f);
    }
}
