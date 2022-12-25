using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteViewer : MonoBehaviour
{
    [Header("References to Image component")]
    [SerializeField] private Image image;

    public void ChangeSpriteImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
