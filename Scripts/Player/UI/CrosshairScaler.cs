using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScaler : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float restingScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float scaleSpeed;
    private Vector2 horizontalInput;
    private float currentScale;
    

    void Start()
    {
        currentScale = restingScale;
    }

    void Update()
    {
        if(horizontalInput != Vector2.zero)
        {
            currentScale = Mathf.Lerp(currentScale, maxScale, Time.deltaTime * scaleSpeed);
        }
        else
        {
            currentScale = Mathf.Lerp(currentScale, restingScale, Time.deltaTime * scaleSpeed);
        }
        
        rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    public void ReceiveHorizontalInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
}
