using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Scriptable Object")]
    [SerializeField] private CrosshairScriptableObject crosshairSO;

    [Header("Crosshair Properties")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image crosshairImage;

    [Header("Layers to detect")]
    [SerializeField] private LayerMask detectableLayers;
    [SerializeField] private int enemyLayer;
    [SerializeField] private int breakableLayer;

    [Header("Events")]
    [SerializeField] private VoidEvent onInteractableDetectEvent;
    [SerializeField] private VoidEvent onInteractableExitEvent;
    #endregion

    #region Private Fields
    private Vector3 currentScale;
    private float currentIndividualScale;
    private float initScale;
    private float maxScale;
    private float scaleSpeed;
    private Vector2 horizontalMovement;
    #endregion

    void Start()
    {
        InitCrosshair();    
    }

    void Update()
    {
        // Process crosshair scale according to movement
        if (horizontalMovement != Vector2.zero)
        {
            currentIndividualScale = Mathf.Lerp(currentIndividualScale, maxScale, Time.deltaTime * scaleSpeed);
        }
        else
        {
            currentIndividualScale = Mathf.Lerp(currentIndividualScale, initScale, Time.deltaTime * scaleSpeed);
        }

        currentScale.x = currentIndividualScale;
        currentScale.y = currentIndividualScale;
        currentScale.z = currentIndividualScale;
        rectTransform.localScale = currentScale;
    }

    #region Public Methods
    /// <summary>
    ///     Changes color of the crosshair based on the input color
    /// </summary>
    /// <param name="arg_color"></param>
    public void ChangeCrosshairColor(Color arg_color)
    {
        crosshairImage.color = arg_color;
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///      Initializes the crosshair properties like currentscale, init scale, max scale and scale speed
    /// </summary>
    private void InitCrosshair()
    {
        currentScale        = new Vector3(crosshairSO.initScale, crosshairSO.initScale, crosshairSO.initScale);
        initScale           = crosshairSO.initScale;
        maxScale            = crosshairSO.maxScale;
        scaleSpeed          = crosshairSO.scaleSpeed;
    }
    #endregion
}
